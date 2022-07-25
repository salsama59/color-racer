using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BonusManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bonusChoicePanel; 
    public static event Action<BonusEnum, float> OnBonusChoice;
    private static float SPEED_BONUS_AMOUNT = 3f;
    private static float FUEL_REGENERATION_BONUS_AMOUNT = 0.25f;
    private static float MANIABILITY_BONUS_AMOUNT = 5f;


    private void OnEnable()
    {
        LapCollider.OnBonusChoiceDisplay += DisplayBonusUi;
    }

    private void OnDisable()
    {
        LapCollider.OnBonusChoiceDisplay -= DisplayBonusUi;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSpeedBonus()
    {
        this.AddBonus(BonusEnum.SPEED, SPEED_BONUS_AMOUNT);
    }

    public void AddFuelRegenerationBonus()
    {
        this.AddBonus(BonusEnum.FUEL_REGENERATION, FUEL_REGENERATION_BONUS_AMOUNT);
    }

    public void AddManiabilityBonus()
    {
        this.AddBonus(BonusEnum.MANIABILITY, MANIABILITY_BONUS_AMOUNT);
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
        OnBonusChoice.Invoke(bonusType, bonusAmountInPercentage);
        this.HideBonusUi();
    }
}
