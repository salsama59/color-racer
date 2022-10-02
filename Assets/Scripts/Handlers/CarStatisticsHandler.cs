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
        this.CarStatus = new CarStatus();
        this.DriverStatus = new DriverStatus();
    }

    public CarStatus CarStatus { get => carStatus; set => carStatus = value; }
    public DriverStatus DriverStatus { get => driverStatus; set => driverStatus = value; }
}
