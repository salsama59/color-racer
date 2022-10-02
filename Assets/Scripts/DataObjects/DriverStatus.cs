using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverStatus
{
    private string driverName;
    private int experienceTotal;
    private int experienceToReachNextLevel;
    private int skillPoints;
    private int driverMana;

    public string DriverName { get => driverName; set => driverName = value; }
    public int ExperienceTotal { get => experienceTotal; set => experienceTotal = value; }
    public int ExperienceToReachNextLevel { get => experienceToReachNextLevel; set => experienceToReachNextLevel = value; }
    public int SkillPoints { get => skillPoints; set => skillPoints = value; }
    public int DriverMana { get => driverMana; set => driverMana = value; }
}
