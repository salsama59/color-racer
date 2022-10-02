using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DamageManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI damageText;
    private float timeElapsed;
    public static event Action<GameOverReasonEnum> OnDamageBeyondRepair;
    private static float DAMAGE_UPDATE_RATE_IN_SECONDS = 1f;

    private CarStatus carStatus;

    private void Start()
    {
        GameObject playerCarGameObject = GameObject.FindGameObjectWithTag(TagsConstants.PLAYER_TAG);
        this.carStatus = CarUtils.GetCarStatus(playerCarGameObject);
    }

    private void OnEnable()
    {
        ObstacleCollider.OnPlayerCarDamagingObstacleHit += AddDamage;
        CarMovementController.OnCarDamageRepairBonusAttribution += AllowsDamageRepair;
        LapCollider.OnDamageRepairBonusEnd += ModifyDamageRepairRequirement;
    }

    private void OnDisable()
    {
        ObstacleCollider.OnPlayerCarDamagingObstacleHit -= AddDamage;
        CarMovementController.OnCarDamageRepairBonusAttribution -= AllowsDamageRepair;
        LapCollider.OnDamageRepairBonusEnd -= ModifyDamageRepairRequirement;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameInPause)
        {
            if (this.carStatus.IsDamageRepairActive)
            {
                this.timeElapsed += Time.deltaTime;
                if (this.timeElapsed >= DAMAGE_UPDATE_RATE_IN_SECONDS)
                {
                    this.RepairDamage(this.carStatus.AmountOfDamageRepaired);
                    this.timeElapsed = 0f;
                }

            } else
            {
                this.timeElapsed = 0f;
            }

            if (this.carStatus.Damage == this.carStatus.MaxDamage && OnDamageBeyondRepair != null)
            {
                OnDamageBeyondRepair.Invoke(GameOverReasonEnum.DAMAGE);
            }
        }
    }

    public void AddDamage(float damageAmountInPercentageToAdd)
    {
        this.carStatus.Damage += this.carStatus.MaxDamage * damageAmountInPercentageToAdd / 100f;
        this.carStatus.Damage = Mathf.Clamp(this.carStatus.Damage, 0f, this.carStatus.MaxDamage);
        this.UpdateDamageDisplay();
    }

    public void RepairDamage(float damageAmountInPercentageToRemove)
    {
        this.carStatus.Damage -= this.carStatus.MaxDamage * damageAmountInPercentageToRemove / 100f;
        this.carStatus.Damage = Mathf.Clamp(this.carStatus.Damage, 0f, this.carStatus.MaxDamage);
        this.UpdateDamageDisplay();
    }

    public void UpdateDamageDisplay()
    {
        this.damageText.text = $"Damages : {this.carStatus.Damage}/{this.carStatus.MaxDamage}";
    }

    public void AllowsDamageRepair(float amountOfDamageRepaired)
    {
        this.carStatus.AmountOfDamageRepaired = amountOfDamageRepaired;
        this.ModifyDamageRepairRequirement(true);
    }

    public void ModifyDamageRepairRequirement(bool isDamageRepairActive)
    {
        this.carStatus.IsDamageRepairActive = isDamageRepairActive;
    }
}
