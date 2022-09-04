using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CarMovementController : MonoBehaviour
{

    
    private float carTranslationSpeed = 8f;
    private float carRotationSpeed = 3f;
    public static event Action<PointInTime> OnCarMovementInput;
    public static event Action<float> OnCarFuelBonusAttribution;
    public static event Action<float> OnCarDamageRepairBonusAttribution;
    private CarSpriteManager carSpriteManager;
    [SerializeField]
    private Rigidbody2D carRigidBody2D;
    private float rotationAngle = 0f;
    private float driftFactor = 0.80f;
    private float maxSpeed = 20f;

    [SerializeField]
    private CarInputHandler carInputHandler;
    private float velocityDotProduct;

    private Vector2 carVelocitySaved;
    private float carAngularVelocitySaved;
    private float carDragSaved;
    private float carAngularDragSaved;
    private Animator carAnimator;



    private void Start()
    {
        this.carSpriteManager = this.gameObject.GetComponent<CarSpriteManager>();
        this.carAnimator = this.gameObject.GetComponent<Animator>();
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
        if (velocityDotProduct > maxSpeed && this.carInputHandler.AccelerationInput > 0)
        {
            return;
        }
        
        //Limit so we cannot go faster than the 50% of max speed in the "reverse" direction
        if (velocityDotProduct < -maxSpeed * 0.5f && this.carInputHandler.AccelerationInput < 0)
        {
            return;
        }
           
        //Limit so we cannot go faster in any direction while accelerating
        if (this.carRigidBody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && this.carInputHandler.AccelerationInput > 0)
        {
            return;
        }

        //Create a force for the engine
        engineForceVector = transform.up * this.carInputHandler.AccelerationInput * this.carTranslationSpeed;

        //Apply force and pushes the car forward
        this.carRigidBody2D.AddForce(engineForceVector, ForceMode2D.Force);

        pointInTime.IsForceApplied = true;
        pointInTime.ForceVector = new Vector2(engineForceVector.x, engineForceVector.y);
    }

    private void RotateCar(PointInTime pointInTime)
    {
        //Limit the cars ability to turn when moving slowly
        float minimumSpeedBeforeAllowRotation = (this.carRigidBody2D.velocity.magnitude / 2);
        minimumSpeedBeforeAllowRotation = Mathf.Clamp01(minimumSpeedBeforeAllowRotation);

        //Update the rotation angle based on input
        rotationAngle -= this.carInputHandler.RotationInput * this.carRotationSpeed * minimumSpeedBeforeAllowRotation;

        //Apply steering by rotating the car object
        this.carRigidBody2D.MoveRotation(rotationAngle);

        pointInTime.RotationAngle = rotationAngle;
    }

    void RemoveOrthogonalVelocity(PointInTime pointInTime)
    {
        //Get forward and right velocity of the car
        Vector2 forwardVelocity = transform.up * Vector2.Dot(this.carRigidBody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(this.carRigidBody2D.velocity, transform.right);

        //Kill the orthogonal velocity (side velocity) based on how much the car should drift. 
        this.carRigidBody2D.velocity = forwardVelocity + rightVelocity * driftFactor;

        pointInTime.VelocityVector = new Vector2(this.carRigidBody2D.velocity.x, this.carRigidBody2D.velocity.y);
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
            case BonusEnum.DAMAGE_REPAIR:
                if (OnCarDamageRepairBonusAttribution != null)
                {
                    OnCarDamageRepairBonusAttribution.Invoke(bonusAmountInPercentage);
                }
                break;
            default:
                break;
        }
        this.carSpriteManager.UpdateCarRunningAnimation(bonusType, this.carAnimator);
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
