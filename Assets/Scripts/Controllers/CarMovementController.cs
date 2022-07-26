using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CarMovementController : MonoBehaviour
{

    [SerializeField]
    private float carTranslationSpeed = 5;
    [SerializeField]
    private float carRotationSpeed = 100;
    public static event Action<Vector2, Vector3> OnCarMovementInput;
    public static event Action<float> OnCarFuelBonusAttribution;

    private void OnEnable()
    {
        BonusManager.OnBonusChoice += AddBonusToCar;
        ObstacleCollider.OnPlayerCarSlowingObstacleHit += RemoveSpeed;
        ObstacleCollider.OnPlayerCarSlowingObstacleExit += AddSpeed;
    }

    private void OnDisable()
    {
        BonusManager.OnBonusChoice -= AddBonusToCar;
        ObstacleCollider.OnPlayerCarSlowingObstacleHit -= RemoveSpeed;
        ObstacleCollider.OnPlayerCarSlowingObstacleExit -= AddSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameInPause)
        {
            Vector2 translationExecuted = Vector2.zero;
            Vector3 rotationExecuted = Vector3.zero;

            if (Input.GetKey(KeyCode.A))
            {
                FuelManager.isEngineOn = true;
                //Forward
                translationExecuted = Vector2.up * carTranslationSpeed * Time.deltaTime;
                transform.Translate(translationExecuted);
                //Rotate
                rotationExecuted = this.RotateCar();
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                FuelManager.isEngineOn = true;
                //Backward
                translationExecuted = Vector2.down * carTranslationSpeed * Time.deltaTime;
                transform.Translate(translationExecuted);
                //Rotate
                rotationExecuted = this.RotateCar();
            } else
            {
                FuelManager.isEngineOn = false;
            }

            if (OnCarMovementInput != null)
            {
                OnCarMovementInput.Invoke(translationExecuted, rotationExecuted);
            }
        }
    }

    private Vector3 RotateCar()
    {
        Vector3 executedRotation = Vector3.zero;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Turn right
            executedRotation = Vector3.back * carRotationSpeed * Time.deltaTime;
            transform.Rotate(executedRotation);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Turn left
            executedRotation = Vector3.forward * carRotationSpeed * Time.deltaTime;
            transform.Rotate(executedRotation);
        }

        return executedRotation;
    }

    public void AddSpeed(float amountToAddInPercentage)
    {
        this.carTranslationSpeed += this.carTranslationSpeed * amountToAddInPercentage / 100f;
    }

    public void RemoveSpeed(float amountToRemoveInPercentage)
    {
        this.carTranslationSpeed -= this.carTranslationSpeed * amountToRemoveInPercentage / 100f;
    }

    public void AddBonusToCar(BonusEnum bonusType, float bonusAmountInPercentage)
    {
        switch (bonusType)
        {
            case BonusEnum.SPEED:
                this.AddSpeed(bonusAmountInPercentage);
                break;
            case BonusEnum.FUEL_REGENERATION:
                if(OnCarFuelBonusAttribution != null)
                {
                    OnCarFuelBonusAttribution.Invoke(bonusAmountInPercentage);
                }
                break;
            case BonusEnum.MANIABILITY:
                this.carRotationSpeed += this.carRotationSpeed * bonusAmountInPercentage / 100f;
                break;
            default:
                break;
        }
    }
}
