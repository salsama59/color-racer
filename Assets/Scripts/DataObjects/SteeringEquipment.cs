using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SteeringEquipment : CarEquipment
{
    [SerializeField]
    private float manoeuvrabilityBonus;

    public float ManoeuvrabilityBonus { get => manoeuvrabilityBonus; set => manoeuvrabilityBonus = value; }
}
