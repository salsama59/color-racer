using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FuelTankEquipment : CarEquipment
{
    [SerializeField]
    private float maxFuelBonus;

    public float MaxFuelBonus { get => maxFuelBonus; set => maxFuelBonus = value; }
}
