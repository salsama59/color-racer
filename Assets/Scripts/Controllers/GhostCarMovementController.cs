using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GhostCarMovementController : MonoBehaviour
{
    private List<PointInTime> ghostCarMovementRecords;
    private int currentRecordIndex = 0;
    private Vector3 ghostInitialPosition;
    private Quaternion ghostInitialRotation;

    public static event Action<Rigidbody2D, List<PointInTime>, int> OnGhostCarMovementRefresh;
    private Rigidbody2D ghostCarRigidBody;

    private Vector2 carVelocitySaved;
    private float carAngularVelocitySaved;
    private float carDragSaved;
    private float carAngularDragSaved;


    private void Awake()
    {
        this.ghostCarRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(OnGhostCarMovementRefresh != null && !GameManager.isGameInPause)
        {
            OnGhostCarMovementRefresh.Invoke(ghostCarRigidBody, GhostCarMovementRecords, currentRecordIndex);
            currentRecordIndex++;

            if (currentRecordIndex == GhostCarMovementRecords.Count)
            {
                currentRecordIndex = 0;
                this.transform.position = GhostInitialPosition;
                this.transform.rotation = GhostInitialRotation;
            }
        }
    }

    public void StopGhostCarMotion()
    {
        this.carVelocitySaved = new Vector2(this.ghostCarRigidBody.velocity.x, this.ghostCarRigidBody.velocity.y);
        this.carAngularVelocitySaved = this.ghostCarRigidBody.angularVelocity;
        this.carDragSaved = this.ghostCarRigidBody.drag;
        this.carAngularDragSaved = this.ghostCarRigidBody.angularDrag;
        this.ghostCarRigidBody.Sleep();
    }

    public void ResumeGhostCarMotion()
    {
        this.ghostCarRigidBody.WakeUp();
        this.ghostCarRigidBody.velocity = this.carVelocitySaved;
        this.ghostCarRigidBody.angularVelocity = this.carAngularVelocitySaved;
        this.ghostCarRigidBody.drag = this.carDragSaved;
        this.ghostCarRigidBody.angularDrag = this.carAngularDragSaved;
    }

    public List<PointInTime> GhostCarMovementRecords { get => ghostCarMovementRecords; set => ghostCarMovementRecords = value; }
    public Vector3 GhostInitialPosition { get => ghostInitialPosition; set => ghostInitialPosition = value; }
    public Quaternion GhostInitialRotation { get => ghostInitialRotation; set => ghostInitialRotation = value; }
}
