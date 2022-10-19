using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BodyEquipment : CarEquipment
{
    [SerializeField]
    private float damageResistanceBonus;
    [SerializeField]
    private float maxDamageBonus;
    [SerializeField]
    private float maxSpeedBonus;

    public float DamageResistanceBonus { get => damageResistanceBonus; set => damageResistanceBonus = value; }
    public float MaxDamageBonus { get => maxDamageBonus; set => maxDamageBonus = value; }
    public float MaxSpeedBonus { get => maxSpeedBonus; set => maxSpeedBonus = value; }
}
