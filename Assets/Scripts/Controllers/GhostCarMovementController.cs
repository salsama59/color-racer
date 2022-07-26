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

    public static event Action<Transform, List<PointInTime>, int> OnGhostCarMovementRefresh;

    void Update()
    {
        if(OnGhostCarMovementRefresh != null && !GameManager.isGameInPause)
        {
            OnGhostCarMovementRefresh.Invoke(this.transform, GhostCarMovementRecords, currentRecordIndex);
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
