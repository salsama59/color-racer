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
    private CarEquipments carEquipments;

    private void Start()
    {
        GameObject playerCarGameObject = GameObject.FindGameObjectWithTag(TagsConstants.PLAYER_TAG);
        this.carStatus = CarUtils.GetCarStatus(playerCarGameObject);
        this.carEquipments = CarUtils.GetCarEquipments(playerCarGameObject);
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
                    this.RepairDamage(this.carStatus.AmountOfDamageRepaired.GetEnhancedValue());
                    this.timeElapsed = 0f;
                }

            } else
            {
                this.timeElapsed = 0f;
            }

            if (this.carStatus.Damage.BaseValue == this.CalculateEnhancedMaxDamageOfCar() && OnDamageBeyondRepair != null)
            {
                OnDamageBeyondRepair.Invoke(GameOverReasonEnum.DAMAGE);
            }
        }
    }

    public void AddDamage(float damageAmountInPercentageToAdd)
    {
        float damageReduced = damageAmountInPercentageToAdd - this.carEquipments.BodyEquipment.DamageResistanceBonus;

        if(damageReduced < 0)
        {
            damageReduced = 0;
        }

        this.carStatus.Damage.BaseValue += this.CalculateEnhancedMaxDamageOfCar() * damageReduced / 100f;
        this.carStatus.Damage.BaseValue = Mathf.Clamp(this.carStatus.Damage.BaseValue, 0f, this.CalculateEnhancedMaxDamageOfCar());
        this.UpdateDamageDisplay();
    }

    public void RepairDamage(float damageAmountInPercentageToRemove)
    {
        this.carStatus.Damage.BaseValue -= this.CalculateEnhancedMaxDamageOfCar() * damageAmountInPercentageToRemove / 100f;
        this.carStatus.Damage.BaseValue = Mathf.Clamp(this.carStatus.Damage.BaseValue, 0f, this.CalculateEnhancedMaxDamageOfCar());
        this.UpdateDamageDisplay();
    }

    public void UpdateDamageDisplay()
    {
        this.damageText.text = $"Damages : {this.carStatus.Damage.BaseValue}/{this.CalculateEnhancedMaxDamageOfCar()}";
    }

    public void AllowsDamageRepair(float amountOfDamageRepaired)
    {
        this.carStatus.AmountOfDamageRepaired.BaseValue = amountOfDamageRepaired;
        this.ModifyDamageRepairRequirement(true);
    }

    public void ModifyDamageRepairRequirement(bool isDamageRepairActive)
    {
        this.carStatus.IsDamageRepairActive = isDamageRepairActive;
    }

    private float CalculateEnhancedMaxDamageOfCar()
    {
        return this.carStatus.MaxDamage.GetEnhancedValue()/*+ this.carEquipments.BodyEquipment.MaxDamageBonus*/;
    }
}
