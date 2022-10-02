using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStatisticsHandler : MonoBehaviour
{
    private CarStatus carStatus;
    private DriverStatus driverStatus;

    // Start is called before the first frame update
    private void Awake()
    {
        string carDataJsonString = JsonUtils.LoadJsonFile(FileConstants.CAR_STATUS_JSON_FILE_PATH);
        this.CarStatus = JsonUtils.FromJsonToObject<CarStatus>(carDataJsonString);
        string driverDataJsonString = JsonUtils.LoadJsonFile(FileConstants.DRIVER_STATUS_JSON_FILE_PATH);
        this.DriverStatus = JsonUtils.FromJsonToObject<DriverStatus>(driverDataJsonString);
    }

    public CarStatus CarStatus { get => carStatus; set => carStatus = value; }
    public DriverStatus DriverStatus { get => driverStatus; set => driverStatus = value; }
}
