using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BonusManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bonusChoicePanel; 
    public static event Action<BonusEnum, float> OnBonusChoice;
    public static event Action OnCarsMotionResume;

    private void OnEnable()
    {
        LapCollider.OnBonusChoiceDisplay += DisplayBonusUi;
    }

    private void OnDisable()
    {
        LapCollider.OnBonusChoiceDisplay -= DisplayBonusUi;
    }

    public void AddSpeedBonus()
    {
        this.AddBonus(BonusEnum.SPEED, BonusConstants.SPEED_BONUS_AMOUNT);
    }

    public void AddFuelRegenerationBonus()
    {
        this.AddBonus(BonusEnum.FUEL_REGENERATION, BonusConstants.FUEL_REGENERATION_BONUS_AMOUNT);
    }

    public void AddManiabilityBonus()
    {
        this.AddBonus(BonusEnum.MANIABILITY, BonusConstants.MANIABILITY_BONUS_AMOUNT);
    }

    public void AddDamageRepairBonus()
    {
        this.AddBonus(BonusEnum.DAMAGE_REPAIR, BonusConstants.DAMAGE_REPAIR_BONUS_AMOUNT);
    }

    public void DisplayBonusUi()
    {
        GameManager.isGameInPause = true;
        this.bonusChoicePanel.SetActive(true);
    }

    public void HideBonusUi()
    {
        GameManager.isGameInPause = false;
        this.bonusChoicePanel.SetActive(false);
    }

    private void AddBonus(BonusEnum bonusType, float bonusAmountInPercentage)
    {
        if(OnBonusChoice != null)
        {
            OnBonusChoice.Invoke(bonusType, bonusAmountInPercentage);
        }
        
        this.HideBonusUi();

        //The bonus choice resume the game after the lap
        if(OnCarsMotionResume != null)
        {
            OnCarsMotionResume.Invoke();
        }
    }
}
