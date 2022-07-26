using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DamageManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI damageText;
    private float damage = 0f;
    private float maxDamage = 100f;
    private float timeElapsed;
    private bool isDamageRepairActive;
    public static event Action<GameOverReasonEnum> OnDamageBeyondRepair;

    private float amountOfDamageRepaired = 0f;
    private static float DAMAGE_UPDATE_RATE_IN_SECONDS = 1f;

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
            if (this.isDamageRepairActive)
            {
                this.timeElapsed += Time.deltaTime;
                if (this.timeElapsed >= DAMAGE_UPDATE_RATE_IN_SECONDS)
                {
                    this.RepairDamage(this.amountOfDamageRepaired);
                    this.timeElapsed = 0f;
                }

            } else
            {
                this.timeElapsed = 0f;
            }

            if (this.damage == this.maxDamage && OnDamageBeyondRepair != null)
            {
                OnDamageBeyondRepair.Invoke(GameOverReasonEnum.DAMAGE);
            }
        }
    }

    public void AddDamage(float damageAmountInPercentageToAdd)
    {
        this.damage += this.maxDamage * damageAmountInPercentageToAdd / 100f;
        this.damage = Mathf.Clamp(this.damage, 0f, this.maxDamage);
        this.UpdateDamageDisplay();
    }

    public void RepairDamage(float damageAmountInPercentageToRemove)
    {
        this.damage -= this.maxDamage * damageAmountInPercentageToRemove / 100f;
        this.damage = Mathf.Clamp(this.damage, 0f, this.maxDamage);
        this.UpdateDamageDisplay();
    }

    public void UpdateDamageDisplay()
    {
        this.damageText.text = $"Damages : {this.damage}/{this.maxDamage}";
    }

    public void AllowsDamageRepair(float amountOfDamageRepaired)
    {
        this.amountOfDamageRepaired = amountOfDamageRepaired;
        this.ModifyDamageRepairRequirement(true);
    }

    public void ModifyDamageRepairRequirement(bool isDamageRepairActive)
    {
        this.isDamageRepairActive = isDamageRepairActive;
    }
}
