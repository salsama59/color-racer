using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class FuelManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI fuelText;

    [SerializeField]
    private float fuel = 100f;
    private float maxFuel = 100f;
    private static float FUEL_CONSUMPTION_PERCENTAGE = 0.25f;
    private static float FUEL_UPDATE_RATE_IN_SECONDS = 1f;
    public static bool isEngineOn = false; 

    private bool isFuelRegenerationAllowed = false;
    private float amountOfFuelRegenerated = 0f;

    private float timeElapsed = 0;

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
        if (!GameManager.isGameInPause)
        {

            this.timeElapsed += Time.deltaTime;

            if(this.timeElapsed >= FUEL_UPDATE_RATE_IN_SECONDS)
            {
                if (!this.isFuelRegenerationAllowed && isEngineOn)
                {
                    this.SubstractFuel(FUEL_CONSUMPTION_PERCENTAGE);
                }
                else if(this.isFuelRegenerationAllowed)
                {
                    this.AddFuel(this.amountOfFuelRegenerated);
                }

                this.timeElapsed = 0f;
            }
            
        }
    }

    public void UpdateFuelDisplay()
    {
        this.fuelText.text = $"Fuel : {this.fuel}%";
    }

    public void AddFuel(float amountInpercentage)
    {
        this.fuel += amountInpercentage * this.maxFuel / 100;
        Mathf.Clamp(this.fuel, 0f, this.maxFuel);
        this.UpdateFuelDisplay();
    }

    public void SubstractFuel(float amountInpercentage)
    {
        this.fuel -= amountInpercentage * this.maxFuel / 100;
        Mathf.Clamp(this.fuel, 0f, this.maxFuel);
        this.UpdateFuelDisplay();
    }

    public void AllowsFuelRegeneration(float amountOfFuelRegenerated)
    {
        this.amountOfFuelRegenerated = amountOfFuelRegenerated;
        this.ModifyFuelRegenerationRequirement(true);
    }

    public void ModifyFuelRegenerationRequirement(bool isRegenerationAllowed)
    {
        this.isFuelRegenerationAllowed = isRegenerationAllowed;
    }
}
