using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WheelEquipment : CarEquipment
{
    [SerializeField]
    private float speedBonus;
    [SerializeField]
    private float driftFactorBonus;
    [SerializeField]
    private float manoeuvrabilityBonus;

    public float SpeedBonus { get => speedBonus; set => speedBonus = value; }
    public float ManoeuvrabilityBonus { get => manoeuvrabilityBonus; set => manoeuvrabilityBonus = value; }
    public float DriftFactorBonus { get => driftFactorBonus; set => driftFactorBonus = value; }
}
