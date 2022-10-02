using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarUtils
{
    public static CarStatus GetCarStatus(GameObject carGameObject)
    {
        return carGameObject.GetComponent<CarStatisticsHandler>().CarStatus;
    }

    public static DriverStatus GetDriverStatus(GameObject carGameObject)
    {
        return carGameObject.GetComponent<CarStatisticsHandler>().DriverStatus;
    }
}
