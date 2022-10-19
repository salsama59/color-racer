using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class CarEquipments
{
    [SerializeField]
    private WheelEquipment wheelEquipment;
    [SerializeField]
    private EngineEquipment engineEquipment;
    [SerializeField]
    private BrakesEquipment brakesEquipment;
    [SerializeField]
    private BodyEquipment bodyEquipment;
    [SerializeField]
    private FuelTankEquipment fuelTankEquipment;
    [SerializeField]
    private SteeringEquipment steeringEquipment;

    public WheelEquipment WheelEquipment { get => wheelEquipment; set => wheelEquipment = value; }
    public EngineEquipment EngineEquipment { get => engineEquipment; set => engineEquipment = value; }
    public BrakesEquipment BrakesEquipment { get => brakesEquipment; set => brakesEquipment = value; }
    public BodyEquipment BodyEquipment { get => bodyEquipment; set => bodyEquipment = value; }
    public FuelTankEquipment FuelTankEquipment { get => fuelTankEquipment; set => fuelTankEquipment = value; }
    public SteeringEquipment SteeringEquipment { get => steeringEquipment; set => steeringEquipment = value; }
}

