using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class CarStatus
{
    [SerializeField]
    private CarStatistic carTranslationSpeed;
    [SerializeField]
    private CarStatistic carRotationSpeed;
    [SerializeField]
    private CarStatistic driftFactor;
    [SerializeField]
    private CarStatistic maxSpeed;
    [SerializeField]
    private CarStatistic fuel;
    [SerializeField]
    private CarStatistic maxFuel;
    [SerializeField]
    private CarStatistic amountOfFuelRegenerated;
    [SerializeField]
    private CarStatistic damage;
    [SerializeField]
    private CarStatistic maxDamage;
    [SerializeField]
    private CarStatistic amountOfDamageRepaired;
    [SerializeField]
    private bool isEngineOn = false;
    [SerializeField]
    private bool isDamageRepairActive;
    [SerializeField]
    private bool isFuelRegenerationAllowed = false;

    public CarStatistic CarTranslationSpeed { get => carTranslationSpeed; set => carTranslationSpeed = value; }
    public CarStatistic CarRotationSpeed { get => carRotationSpeed; set => carRotationSpeed = value; }
    public CarStatistic DriftFactor { get => driftFactor; set => driftFactor = value; }
    public CarStatistic MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public CarStatistic Fuel { get => fuel; set => fuel = value; }
    public CarStatistic MaxFuel { get => maxFuel; set => maxFuel = value; }
    public CarStatistic AmountOfFuelRegenerated { get => amountOfFuelRegenerated; set => amountOfFuelRegenerated = value; }
    public CarStatistic Damage { get => damage; set => damage = value; }
    public CarStatistic MaxDamage { get => maxDamage; set => maxDamage = value; }
    public CarStatistic AmountOfDamageRepaired { get => amountOfDamageRepaired; set => amountOfDamageRepaired = value; }
    public bool IsEngineOn { get => isEngineOn; set => isEngineOn = value; }
    public bool IsDamageRepairActive { get => isDamageRepairActive; set => isDamageRepairActive = value; }
    public bool IsFuelRegenerationAllowed { get => isFuelRegenerationAllowed; set => isFuelRegenerationAllowed = value; }
}

