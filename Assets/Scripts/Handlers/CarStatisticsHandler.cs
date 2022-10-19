using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStatisticsHandler : MonoBehaviour
{
    private CarStatus carStatus;
    private DriverStatus driverStatus;
    private CarEquipments carEquipments;

    // Start is called before the first frame update
    private void Awake()
    {
        string carDataJsonString = JsonUtils.LoadJsonFile(FileConstants.CAR_STATUS_JSON_FILE_PATH);
        this.CarStatus = JsonUtils.FromJsonToObject<CarStatus>(carDataJsonString);
        string driverDataJsonString = JsonUtils.LoadJsonFile(FileConstants.DRIVER_STATUS_JSON_FILE_PATH);
        this.DriverStatus = JsonUtils.FromJsonToObject<DriverStatus>(driverDataJsonString);

        string wheelDataJsonString = JsonUtils.LoadJsonFile(FileConstants.CAR_DEFAULT_EQUIPMENT_WHEEL_JSON_FILE_PATH);
        string bodyDataJsonString = JsonUtils.LoadJsonFile(FileConstants.CAR_DEFAULT_EQUIPMENT_BODY_JSON_FILE_PATH);
        string brakesDataJsonString = JsonUtils.LoadJsonFile(FileConstants.CAR_DEFAULT_EQUIPMENT_BRAKES_JSON_FILE_PATH);
        string engineDataJsonString = JsonUtils.LoadJsonFile(FileConstants.CAR_DEFAULT_EQUIPMENT_ENGINE_JSON_FILE_PATH);
        string steeringDataJsonString = JsonUtils.LoadJsonFile(FileConstants.CAR_DEFAULT_EQUIPMENT_STEERING_JSON_FILE_PATH);
        string fuelTankDataJsonString = JsonUtils.LoadJsonFile(FileConstants.CAR_DEFAULT_EQUIPMENT_FUEL_TANK_JSON_FILE_PATH);
        this.carEquipments = new CarEquipments();
        this.carEquipments.WheelEquipment = JsonUtils.FromJsonToObject<WheelEquipment>(wheelDataJsonString);
        this.OnCarequipmentChanged(this.carEquipments.WheelEquipment, null);
        this.carEquipments.BodyEquipment = JsonUtils.FromJsonToObject<BodyEquipment>(bodyDataJsonString);
        this.OnCarequipmentChanged(this.carEquipments.BodyEquipment, null);
        this.carEquipments.BrakesEquipment = JsonUtils.FromJsonToObject<BrakesEquipment>(brakesDataJsonString);
        this.OnCarequipmentChanged(this.carEquipments.BrakesEquipment, null);
        this.carEquipments.EngineEquipment = JsonUtils.FromJsonToObject<EngineEquipment>(engineDataJsonString);
        this.OnCarequipmentChanged(this.carEquipments.EngineEquipment, null);
        this.carEquipments.SteeringEquipment = JsonUtils.FromJsonToObject<SteeringEquipment>(steeringDataJsonString);
        this.OnCarequipmentChanged(this.carEquipments.SteeringEquipment, null);
        this.carEquipments.FuelTankEquipment = JsonUtils.FromJsonToObject<FuelTankEquipment>(fuelTankDataJsonString);
        this.OnCarequipmentChanged(this.carEquipments.FuelTankEquipment, null);
    }

    public void OnCarequipmentChanged(CarEquipment newEquipment, CarEquipment oldEquipment)
    {
        if(newEquipment != null)
        {
            switch (newEquipment.EquipmentType)
            {
                case EquipmentTypeEnum.WHEEL:
                    WheelEquipment wheelEquipment = newEquipment as WheelEquipment;
                    this.CarStatus.CarRotationSpeed.AddModifier(wheelEquipment.ManoeuvrabilityBonus);
                    this.CarStatus.DriftFactor.AddModifier(wheelEquipment.DriftFactorBonus);
                    this.CarStatus.CarTranslationSpeed.AddModifier(wheelEquipment.SpeedBonus);
                    break;
                case EquipmentTypeEnum.ENGINE:
                    EngineEquipment engineEquipment = (EngineEquipment)newEquipment;
                    this.CarStatus.CarTranslationSpeed.AddModifier(engineEquipment.SpeedBonus);
                    break;
                case EquipmentTypeEnum.BRAKES:
                    break;
                case EquipmentTypeEnum.BODY:
                    BodyEquipment bodyEquipment = (BodyEquipment)newEquipment;
                    this.CarStatus.MaxSpeed.AddModifier(bodyEquipment.MaxSpeedBonus);
                    this.CarStatus.MaxDamage.AddModifier(bodyEquipment.MaxDamageBonus);
                    break;
                case EquipmentTypeEnum.FUEL_TANK:
                    FuelTankEquipment fuelTankEquipment = (FuelTankEquipment)newEquipment;
                    this.CarStatus.MaxFuel.AddModifier(fuelTankEquipment.MaxFuelBonus);
                    break;
                case EquipmentTypeEnum.STEERING_SYSTEM:
                    SteeringEquipment steeringEquipment = (SteeringEquipment)newEquipment;
                    this.CarStatus.CarRotationSpeed.AddModifier(steeringEquipment.ManoeuvrabilityBonus);
                    break;
                default:
                    break;
            }
        }

        if(oldEquipment != null)
        {
            switch (oldEquipment.EquipmentType)
            {
                case EquipmentTypeEnum.WHEEL:
                    WheelEquipment wheelEquipment = (WheelEquipment)oldEquipment;
                    this.CarStatus.CarRotationSpeed.RemoveModifier(wheelEquipment.ManoeuvrabilityBonus);
                    this.CarStatus.DriftFactor.RemoveModifier(wheelEquipment.DriftFactorBonus);
                    this.CarStatus.CarTranslationSpeed.RemoveModifier(wheelEquipment.SpeedBonus);
                    break;
                case EquipmentTypeEnum.ENGINE:
                    EngineEquipment engineEquipment = (EngineEquipment)oldEquipment;
                    this.CarStatus.CarTranslationSpeed.RemoveModifier(engineEquipment.SpeedBonus);
                    break;
                case EquipmentTypeEnum.BRAKES:
                    break;
                case EquipmentTypeEnum.BODY:
                    BodyEquipment bodyEquipment = (BodyEquipment)oldEquipment;
                    this.CarStatus.MaxSpeed.RemoveModifier(bodyEquipment.MaxSpeedBonus);
                    this.CarStatus.MaxDamage.RemoveModifier(bodyEquipment.MaxDamageBonus);
                    break;
                case EquipmentTypeEnum.FUEL_TANK:
                    FuelTankEquipment fuelTankEquipment = (FuelTankEquipment)oldEquipment;
                    this.CarStatus.MaxFuel.RemoveModifier(fuelTankEquipment.MaxFuelBonus);
                    break;
                case EquipmentTypeEnum.STEERING_SYSTEM:
                    SteeringEquipment steeringEquipment = (SteeringEquipment)oldEquipment;
                    this.CarStatus.CarRotationSpeed.RemoveModifier(steeringEquipment.ManoeuvrabilityBonus);
                    break;
                default:
                    break;
            }
        }
    }

    public CarStatus CarStatus { get => carStatus; set => carStatus = value; }
    public DriverStatus DriverStatus { get => driverStatus; set => driverStatus = value; }
    public CarEquipments CarEquipments { get => carEquipments; set => carEquipments = value; }
}
