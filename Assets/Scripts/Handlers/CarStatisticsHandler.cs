using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStatisticsHandler : MonoBehaviour
{
    private CarStatus carStatus;

    // Start is called before the first frame update
    private void Awake()
    {
        this.CarStatus = new CarStatus();
    }

    public CarStatus CarStatus { get => carStatus; set => carStatus = value; }
}
