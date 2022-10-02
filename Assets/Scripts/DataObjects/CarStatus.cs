using System;
using System.Collections.Generic;
using System.Linq;

public class CarStatus
{
    private float carTranslationSpeed = 8f;
    private float carRotationSpeed = 3f;
    private float driftFactor = 0.80f;
    private float maxSpeed = 20f;
    private float fuel = 100f;
    private float maxFuel = 100f;
    private float amountOfFuelRegenerated = 0f;
    private float damage = 0f;
    private float maxDamage = 100f;
    private float amountOfDamageRepaired = 0f;
    private bool isEngineOn = false;
    private bool isDamageRepairActive;
    private bool isFuelRegenerationAllowed = false;

    public float CarTranslationSpeed { get => carTranslationSpeed; set => carTranslationSpeed = value; }
    public float CarRotationSpeed { get => carRotationSpeed; set => carRotationSpeed = value; }
    public float DriftFactor { get => driftFactor; set => driftFactor = value; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float Fuel { get => fuel; set => fuel = value; }
    public float MaxFuel { get => maxFuel; set => maxFuel = value; }
    public float AmountOfFuelRegenerated { get => amountOfFuelRegenerated; set => amountOfFuelRegenerated = value; }
    public float Damage { get => damage; set => damage = value; }
    public float MaxDamage { get => maxDamage; set => maxDamage = value; }
    public float AmountOfDamageRepaired { get => amountOfDamageRepaired; set => amountOfDamageRepaired = value; }
    public bool IsEngineOn { get => isEngineOn; set => isEngineOn = value; }
    public bool IsDamageRepairActive { get => isDamageRepairActive; set => isDamageRepairActive = value; }
    public bool IsFuelRegenerationAllowed { get => isFuelRegenerationAllowed; set => isFuelRegenerationAllowed = value; }
}

