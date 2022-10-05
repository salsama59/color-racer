using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringEquipment : CarEquipment
{
    private float manoeuvrabilityBonus;

    public float ManoeuvrabilityBonus { get => manoeuvrabilityBonus; set => manoeuvrabilityBonus = value; }
}
