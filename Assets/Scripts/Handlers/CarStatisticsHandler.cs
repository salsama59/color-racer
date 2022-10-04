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
        this.carEquipments.BodyEquipment = JsonUtils.FromJsonToObject<BodyEquipment>(bodyDataJsonString);
        this.carEquipments.BrakesEquipment = JsonUtils.FromJsonToObject<BrakesEquipment>(brakesDataJsonString);
        this.carEquipments.EngineEquipment = JsonUtils.FromJsonToObject<EngineEquipment>(engineDataJsonString);
        this.carEquipments.SteeringEquipment = JsonUtils.FromJsonToObject<SteeringEquipment>(steeringDataJsonString);
        this.carEquipments.FuelTankEquipment = JsonUtils.FromJsonToObject<FuelTankEquipment>(fuelTankDataJsonString);
    }

    public CarStatus CarStatus { get => carStatus; set => carStatus = value; }
    public DriverStatus DriverStatus { get => driverStatus; set => driverStatus = value; }
    public CarEquipments CarEquipments { get => carEquipments; set => carEquipments = value; }
}
