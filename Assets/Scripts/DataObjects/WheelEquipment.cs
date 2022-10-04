using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelEquipment : CarEquipment
{
    private float speedBonus;
    private float driftFactorBonus;
    private float manoeuvrabilityBonus;

    public float SpeedBonus { get => speedBonus; set => speedBonus = value; }
    public float ManoeuvrabilityBonus { get => manoeuvrabilityBonus; set => manoeuvrabilityBonus = value; }
    public float DriftFactorBonus { get => driftFactorBonus; set => driftFactorBonus = value; }
}
