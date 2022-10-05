using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class FuelManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI fuelText;
    private static float FUEL_CONSUMPTION_PERCENTAGE = 0.25f;
    private static float FUEL_UPDATE_RATE_IN_SECONDS = 1f;

    private float timeElapsed = 0;

    public static event Action<GameOverReasonEnum> OnFuelShortage;
    private CarStatus carStatus;
    private CarEquipments carEquipments;

    private void Start()
    {
        GameObject playerCarGameObject =  GameObject.FindGameObjectWithTag(TagsConstants.PLAYER_TAG);
        this.carStatus = CarUtils.GetCarStatus(playerCarGameObject);
        this.carEquipments = CarUtils.GetCarEquipments(playerCarGameObject);
    }

    private void OnEnable()
    {
        GhostCollider.OnPlayerFuelDamage += SubstractFuel;
        CarMovementController.OnCarFuelBonusAttribution += AllowsFuelRegeneration;
        LapCollider.OnFuelRegenerationBonusEnd += ModifyFuelRegenerationRequirement;
    }

    private void OnDisable()
    {
        GhostCollider.OnPlayerFuelDamage -= SubstractFuel;
        CarMovementController.OnCarFuelBonusAttribution -= AllowsFuelRegeneration;
        LapCollider.OnFuelRegenerationBonusEnd -= ModifyFuelRegenerationRequirement;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameInPause && GameManager.isRacePreparationDone)
        {

            this.timeElapsed += Time.deltaTime;

            if(this.timeElapsed >= FUEL_UPDATE_RATE_IN_SECONDS)
            {
                if (!this.carStatus.IsFuelRegenerationAllowed && this.carStatus.IsEngineOn)
                {
                    this.SubstractFuel(FUEL_CONSUMPTION_PERCENTAGE);
                }
                else if(this.carStatus.IsFuelRegenerationAllowed)
                {
                    this.AddFuel(this.carStatus.AmountOfFuelRegenerated);
                }

                this.timeElapsed = 0f;

                if(this.carStatus.Fuel == 0f && OnFuelShortage != null)
                {
                    OnFuelShortage.Invoke(GameOverReasonEnum.FUEL);
                }
            }
            
        }
    }

    public void UpdateFuelDisplay()
    {
        this.fuelText.text = $"Fuel : {this.carStatus.Fuel}%";
    }

    public void AddFuel(float amountInpercentage)
    {
        this.carStatus.Fuel += amountInpercentage * this.CalculateEnhancedMaxFuelOfCar() / 100;
        this.carStatus.Fuel = Mathf.Clamp(this.carStatus.Fuel, 0f, this.CalculateEnhancedMaxFuelOfCar());
        this.UpdateFuelDisplay();
    }

    public void SubstractFuel(float amountInpercentage)
    {
        this.carStatus.Fuel -= amountInpercentage * this.CalculateEnhancedMaxFuelOfCar() / 100;
        this.carStatus.Fuel = Mathf.Clamp(this.carStatus.Fuel, 0f, this.CalculateEnhancedMaxFuelOfCar());
        this.UpdateFuelDisplay();
    }

    public void AllowsFuelRegeneration(float amountOfFuelRegenerated)
    {
        this.carStatus.AmountOfFuelRegenerated = amountOfFuelRegenerated;
        this.ModifyFuelRegenerationRequirement(true);
    }

    public void ModifyFuelRegenerationRequirement(bool isRegenerationAllowed)
    {
        this.carStatus.IsFuelRegenerationAllowed = isRegenerationAllowed;
    }

    private float CalculateEnhancedMaxFuelOfCar()
    {
        return this.carStatus.MaxFuel + this.carEquipments.FuelTankEquipment.MaxFuelBonus;
    }
}
