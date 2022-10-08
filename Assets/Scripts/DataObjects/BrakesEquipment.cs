using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BrakesEquipment : CarEquipment
{
    [SerializeField]
    private float speedBonus;

    public float SpeedBonus { get => speedBonus; set => speedBonus = value; }
}
