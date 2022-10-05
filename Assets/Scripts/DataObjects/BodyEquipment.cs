using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyEquipment : CarEquipment
{
    private float damageResistanceBonus;
    private float maxDamageBonus;
    private float maxSpeedBonus;

    public float DamageResistanceBonus { get => damageResistanceBonus; set => damageResistanceBonus = value; }
    public float MaxDamageBonus { get => maxDamageBonus; set => maxDamageBonus = value; }
    public float MaxSpeedBonus { get => maxSpeedBonus; set => maxSpeedBonus = value; }
}
