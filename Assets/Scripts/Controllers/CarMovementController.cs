using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CarMovementController : MonoBehaviour
{

    public static event Action<PointInTime> OnCarMovementInput;
    public static event Action<float> OnCarFuelBonusAttribution;
    public static event Action<float> OnCarDamageRepairBonusAttribution;
    private CarAnimationManager carAnimationManager;
    [SerializeField]
    private Rigidbody2D carRigidBody2D;
    private float rotationAngle = 0f;
    [SerializeField]
    private CarInputHandler carInputHandler;
    private float velocityDotProduct;

    private Vector2 carVelocitySaved;
    private float carAngularVelocitySaved;
    private float carDragSaved;
    private float carAngularDragSaved;
    private Animator carAnimator;
    private CarStatisticsHandler carStatisticsHandler;

    private void Awake()
    {
        this.carStatisticsHandler = GetComponent<CarStatisticsHandler>();
        this.carAnimationManager = GetComponent<CarAnimationManager>();
        this.carAnimator = GetComponent<Animator>();
    }

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
    private void FixedUpdate()
    {
        if (!GameManager.isGameInPause && GameManager.isRacePreparationDone)
        {
           PointInTime pointInTime = new PointInTime();
           this.ApplyEngineForce(pointInTime);
           this.RemoveOrthogonalVelocity(pointInTime);
           this.RotateCar(pointInTime);

           if (OnCarMovementInput != null)
           {
                OnCarMovementInput.Invoke(pointInTime);
           }
        }
        this.carAnimator.SetBool("IsCarRunning", GameManager.isRacePreparationDone);
    }

    private void ApplyEngineForce(PointInTime pointInTime)
    {
        pointInTime.IsForceApplied = false;
        Vector2 engineForceVector;
        float enhancedMaximumSpeed = this.carStatisticsHandler.CarStatus.MaxSpeed.GetEnhancedValue();
        float enhancedSpeed = this.CalculateEnhancedSpeedOfCar();

        //Apply drag if there is no accelerationInput so the car stops when the player lets go of the accelerator
        if (this.carInputHandler.AccelerationInput == 0)
        {
            this.carRigidBody2D.drag = Mathf.Lerp(this.carRigidBody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            this.carRigidBody2D.drag = 0;
        }

        pointInTime.DragValue = this.carRigidBody2D.drag;

        //Caculate how much "forward" we are going in terms of the direction of our velocity
        velocityDotProduct = Vector2.Dot(transform.up, this.carRigidBody2D.velocity);


        //Limit so we cannot go faster than the max speed in the "forward" direction
        if (velocityDotProduct > enhancedMaximumSpeed && this.carInputHandler.AccelerationInput > 0)
        {
            return;
        }

        //Limit so we cannot go faster than the 50% of max speed in the "reverse" direction
        if (velocityDotProduct < -enhancedMaximumSpeed * 0.5f && this.carInputHandler.AccelerationInput < 0)
        {
            return;
        }

        //Limit so we cannot go faster in any direction while accelerating
        if (this.carRigidBody2D.velocity.sqrMagnitude > enhancedMaximumSpeed * enhancedMaximumSpeed && this.carInputHandler.AccelerationInput > 0)
        {
            return;
        }

        //Create a force for the engine
        engineForceVector = transform.up * this.carInputHandler.AccelerationInput * enhancedSpeed;

        //Apply force and pushes the car forward
        this.carRigidBody2D.AddForce(engineForceVector, ForceMode2D.Force);

        pointInTime.IsForceApplied = true;
        pointInTime.ForceVector = new Vector2(engineForceVector.x, engineForceVector.y);
    }

    /*private float CalculateEnhancedMaxSpeedOfCar()
    {
       return this.carStatisticsHandler.CarStatus.MaxSpeed 
            + this.carStatisticsHandler.CarEquipments.BodyEquipment.MaxSpeedBonus;
    }*/

    private float CalculateEnhancedSpeedOfCar()
    {
        return this.carStatisticsHandler.CarStatus.CarTranslationSpeed.GetEnhancedValue();
             //+ this.carStatisticsHandler.CarEquipments.EngineEquipment.SpeedBonus
             //+ this.carStatisticsHandler.CarEquipments.WheelEquipment.SpeedBonus;
    }

    private void RotateCar(PointInTime pointInTime)
    {
        float enhancedRotationSpeed = this.CalculateEnhancedRotationSpeedOfCar();
        //Limit the cars ability to turn when moving slowly
        float minimumSpeedBeforeAllowRotation = (this.carRigidBody2D.velocity.magnitude / 2);
        minimumSpeedBeforeAllowRotation = Mathf.Clamp01(minimumSpeedBeforeAllowRotation);

        //Update the rotation angle based on input
        rotationAngle -= this.carInputHandler.RotationInput * enhancedRotationSpeed * minimumSpeedBeforeAllowRotation;

        //Apply steering by rotating the car object
        this.carRigidBody2D.MoveRotation(rotationAngle);

        pointInTime.RotationAngle = rotationAngle;
    }

    private float CalculateEnhancedRotationSpeedOfCar()
    {
        return this.carStatisticsHandler.CarStatus.CarRotationSpeed.GetEnhancedValue();
             //+ this.carStatisticsHandler.CarEquipments.WheelEquipment.ManoeuvrabilityBonus
             //+ this.carStatisticsHandler.CarEquipments.SteeringEquipment.ManoeuvrabilityBonus;
    }

    void RemoveOrthogonalVelocity(PointInTime pointInTime)
    {
        float enhancedDriftFactor = this.carStatisticsHandler.CarStatus.DriftFactor.GetEnhancedValue();
        //Get forward and right velocity of the car
        Vector2 forwardVelocity = transform.up * Vector2.Dot(this.carRigidBody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(this.carRigidBody2D.velocity, transform.right);

        //Kill the orthogonal velocity (side velocity) based on how much the car should drift. 
        this.carRigidBody2D.velocity = forwardVelocity + rightVelocity * enhancedDriftFactor;

        pointInTime.VelocityVector = new Vector2(this.carRigidBody2D.velocity.x, this.carRigidBody2D.velocity.y);
    }

    /*private float CalculateEnhancedDriftFactorOfCar()
    {
        return this.carStatisticsHandler.CarStatus.DriftFactor
             + this.carStatisticsHandler.CarEquipments.WheelEquipment.DriftFactorBonus;
    }*/

    public void AddSpeed(float amountToAddInPercentage)
    {
        this.carStatisticsHandler.CarStatus.CarTranslationSpeed.BaseValue += this.CalculateEnhancedSpeedOfCar() * amountToAddInPercentage / 100f;
    }

    public void RemoveSpeed(float amountToRemoveInPercentage)
    {
        this.carStatisticsHandler.CarStatus.CarTranslationSpeed.BaseValue -= this.CalculateEnhancedSpeedOfCar() * amountToRemoveInPercentage / 100f;
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
            case BonusEnum.MANOEUVRABILITY:
                this.carStatisticsHandler.CarStatus.CarRotationSpeed.Modifiers.Add(this.CalculateEnhancedRotationSpeedOfCar() * bonusAmountInPercentage / 100f);
                break;
            case BonusEnum.DAMAGE_REPAIR:
                if (OnCarDamageRepairBonusAttribution != null)
                {
                    OnCarDamageRepairBonusAttribution.Invoke(bonusAmountInPercentage);
                }
                break;
            default:
                break;
        }
        this.carAnimationManager.UpdateCarRunningAnimation(bonusType, this.carAnimator);
    }

    public void StopCarMotion()
    {
        this.carVelocitySaved = new Vector2(this.carRigidBody2D.velocity.x, this.carRigidBody2D.velocity.y);
        this.carAngularVelocitySaved = this.carRigidBody2D.angularVelocity;
        this.carDragSaved = this.carRigidBody2D.drag;
        this.carAngularDragSaved = this.carRigidBody2D.angularDrag;
        this.carRigidBody2D.Sleep();
    }

    public void ResumeCarMotion()
    {
        this.carRigidBody2D.WakeUp();
        this.carRigidBody2D.velocity = this.carVelocitySaved;
        this.carRigidBody2D.angularVelocity = this.carAngularVelocitySaved;
        this.carRigidBody2D.drag = this.carDragSaved;
        this.carRigidBody2D.angularDrag = this.carAngularDragSaved;
    }
}
