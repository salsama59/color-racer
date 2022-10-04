using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineEquipment : CarEquipment
{
    private float speedBonus;
    private float fuelConsumptionBonnus;

    public float SpeedBonus { get => speedBonus; set => speedBonus = value; }
    public float FuelConsumptionBonnus { get => fuelConsumptionBonnus; set => fuelConsumptionBonnus = value; }
}
