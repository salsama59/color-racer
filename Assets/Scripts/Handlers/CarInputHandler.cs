using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    private float accelerationInput;
    private float rotationInput;

    // Update is called once per frame
    void Update()
    {

        this.AccelerationInput = Input.GetAxis("Vertical");
        this.RotationInput = Input.GetAxis("Horizontal");

        if (this.AccelerationInput > 0 || this.AccelerationInput < 0)
        {
            FuelManager.isEngineOn = true;
        } else
        {
            FuelManager.isEngineOn = false;
        }

    }

    public float AccelerationInput { get => accelerationInput; set => accelerationInput = value; }
    public float RotationInput { get => rotationInput; set => rotationInput = value; }
}
