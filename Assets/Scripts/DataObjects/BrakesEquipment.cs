using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakesEquipment : CarEquipment
{
    private float speedBonus;

    public float SpeedBonus { get => speedBonus; set => speedBonus = value; }
}
