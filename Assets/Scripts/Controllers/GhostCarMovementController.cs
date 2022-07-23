using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GhostCarMovementController : MonoBehaviour
{
    private List<PointInTime> ghostCarMovementRecords;
    private int currentRecordIndex = 0;

    public static event Action<Transform, List<PointInTime>, int> OnGhostCarMovementRefresh;

    void Update()
    {
        if(OnGhostCarMovementRefresh != null)
        {
            OnGhostCarMovementRefresh.Invoke(this.transform, GhostCarMovementRecords, currentRecordIndex);
            currentRecordIndex++;

            if (currentRecordIndex == GhostCarMovementRecords.Count)
            {
                currentRecordIndex = 0;
                this.transform.position = Vector2.zero;
                this.transform.rotation = Quaternion.identity;
            }
        }
    }

    public List<PointInTime> GhostCarMovementRecords { get => ghostCarMovementRecords; set => ghostCarMovementRecords = value; }
}
