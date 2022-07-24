using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementRecordManager : MonoBehaviour
{
    
    private List<PointInTime> pointsInTime;
    private bool isRecordingMustBeStopped = false;

    // Start is called before the first frame update
    void Start()
    {
        this.PointsInTime = new List<PointInTime>();
    }

    private void OnEnable()
    {
        CarMovementController.OnCarMovementInput += Record;
        GhostCarMovementController.OnGhostCarMovementRefresh += ExecuteRecord;
        GameManager.OnGameOverResultDisplayReady += DisableRecording;
    }

    private void OnDisable()
    {
        CarMovementController.OnCarMovementInput -= Record;
        GhostCarMovementController.OnGhostCarMovementRefresh -= ExecuteRecord;
        GameManager.OnGameOverResultDisplayReady -= DisableRecording;
    }

    private void ExecuteRecord(Transform targetTransform, List<PointInTime> recordsToExecute, int recordIndexToExecute)
    {
        targetTransform.Translate(recordsToExecute[recordIndexToExecute].TranslationVector);
        targetTransform.Rotate(recordsToExecute[recordIndexToExecute].RotationVector);
    }

    public void Record(Vector2 translationVectorTorecord, Vector3 rotationVectorToRecord)
    {
        if (!this.isRecordingMustBeStopped)
        {
            this.PointsInTime.Add(new PointInTime(translationVectorTorecord, rotationVectorToRecord));
        }
    }

    public void EnableRecording()
    {
        this.isRecordingMustBeStopped = false;
    }

    public void DisableRecording()
    {
        this.isRecordingMustBeStopped = true;
    }

    public List<PointInTime> PointsInTime { get => pointsInTime; set => pointsInTime = value; }
}
