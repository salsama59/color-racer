using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EngineEquipment : CarEquipment
{
    [SerializeField]
    private float speedBonus;
    [SerializeField]
    private float fuelConsumptionBonnus;

    public float SpeedBonus { get => speedBonus; set => speedBonus = value; }
    public float FuelConsumptionBonnus { get => fuelConsumptionBonnus; set => fuelConsumptionBonnus = value; }
}
