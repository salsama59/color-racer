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

    public List<PointInTime> GhostCarMovementRecords { get => ghostCarMovementRecords; set => ghostCarMovementRecords = value; }
    public Vector3 GhostInitialPosition { get => ghostInitialPosition; set => ghostInitialPosition = value; }
    public Quaternion GhostInitialRotation { get => ghostInitialRotation; set => ghostInitialRotation = value; }
}
