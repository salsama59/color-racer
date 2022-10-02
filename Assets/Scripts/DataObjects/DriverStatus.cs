using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverStatus
{
    private string driverName;
    private int currentExperience;
    private int experienceToReachNextLevel;
    private int skillPoints;
    private int driverMana;
    private int luck;

    public string DriverName { get => driverName; set => driverName = value; }
    public int CurrentExperience { get => currentExperience; set => currentExperience = value; }
    public int ExperienceToReachNextLevel { get => experienceToReachNextLevel; set => experienceToReachNextLevel = value; }
    public int SkillPoints { get => skillPoints; set => skillPoints = value; }
    public int DriverMana { get => driverMana; set => driverMana = value; }
    public int Luck { get => luck; set => luck = value; }
}
