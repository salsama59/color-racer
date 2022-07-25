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


    private void OnEnable()
    {
        BonusManager.OnBonusChoice += AddBonusToCar;
    }

    private void OnDisable()
    {
        BonusManager.OnBonusChoice -= AddBonusToCar;
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
                //Forward
                translationExecuted = Vector2.up * carTranslationSpeed * Time.deltaTime;
                transform.Translate(translationExecuted);
                //Rotate
                rotationExecuted = this.RotateCar();
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                //Backward
                translationExecuted = Vector2.down * carTranslationSpeed * Time.deltaTime;
                transform.Translate(translationExecuted);
                //Rotate
                rotationExecuted = this.RotateCar();
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

    public void AddBonusToCar(BonusEnum bonusType, float bonusAmountInPercentage)
    {
        switch (bonusType)
        {
            case BonusEnum.SPEED:
                this.carTranslationSpeed += this.carTranslationSpeed * bonusAmountInPercentage / 100f;
                break;
            case BonusEnum.ACCELERATION:
                break;
            case BonusEnum.MANIABILITY:
                this.carRotationSpeed += this.carRotationSpeed * bonusAmountInPercentage / 100f;
                break;
            default:
                break;
        }
    }
}
