using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class CarStatistic
{
    [SerializeField]
    private float baseValue = 0f;
    [SerializeField]
    private List<float> modifiers;

    public float GetEnhancedValue()
    {
        float modifierSum = 0f;
        foreach(float modifier in Modifiers)
        {
            modifierSum += modifier;
        }
        return BaseValue + modifierSum;
    }

    public void AddModifier(float modifier)
    {
        if(modifier != 0f)
        {
            Modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(float modifier)
    {
        if (modifier != 0f)
        {
            Modifiers.Remove(modifier);
        }
    }

    public float BaseValue { get => baseValue; set => baseValue = value; }
    public List<float> Modifiers { get => modifiers; set => modifiers = value; }
}
