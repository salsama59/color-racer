using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DriverStatus
{
    [SerializeField]
    private string driverName;
    [SerializeField]
    private int currentExperience;
    [SerializeField]
    private int experienceToReachNextLevel;
    [SerializeField]
    private int skillPoints;
    [SerializeField]
    private int driverMana;
    [SerializeField]
    private int luck;

    public string DriverName { get => driverName; set => driverName = value; }
    public int CurrentExperience { get => currentExperience; set => currentExperience = value; }
    public int ExperienceToReachNextLevel { get => experienceToReachNextLevel; set => experienceToReachNextLevel = value; }
    public int SkillPoints { get => skillPoints; set => skillPoints = value; }
    public int DriverMana { get => driverMana; set => driverMana = value; }
    public int Luck { get => luck; set => luck = value; }
}
