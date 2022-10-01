using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementRecordManager : MonoBehaviour
{
    
    private List<PointInTime> pointsInTime;
    private bool isRecordingMustBeStopped = false;
    private GameObject playerCarGameObject;
    private Vector3 playerCarReferencePosition = Vector3.zero;
    private Quaternion playerCarReferenceRotation = Quaternion.identity;

    // Start is called before the first frame update
    private void Awake()
    {
        this.playerCarGameObject = GameObject.FindGameObjectWithTag(TagsConstants.PLAYER_TAG);
        this.PointsInTime = new List<PointInTime>();
    }

    private void OnEnable()
    {
        CarMovementController.OnCarMovementInput += Record;
        GhostCarMovementController.OnGhostCarMovementRefresh += ExecuteRecord;
        GameManager.OnGameOverResultDisplayReady += DisableRecording;
        LapCollider.OnRaceBegining += SaveReferencePlayerSettings;
        GhostCarSpawnerManager.OnPlayerCarReferenceSettingsUpdate += SaveReferencePlayerSettings;
    }

    private void OnDisable()
    {
        CarMovementController.OnCarMovementInput -= Record;
        GhostCarMovementController.OnGhostCarMovementRefresh -= ExecuteRecord;
        GameManager.OnGameOverResultDisplayReady -= DisableRecording;
        LapCollider.OnRaceBegining -= SaveReferencePlayerSettings;
        GhostCarSpawnerManager.OnPlayerCarReferenceSettingsUpdate -= SaveReferencePlayerSettings;
    }

    private void ExecuteRecord(Rigidbody2D rigidBody2D, List<PointInTime> recordsToExecute, int recordIndexToExecute)
    {
        PointInTime currentRecord = recordsToExecute[recordIndexToExecute];

        //Simulate the engine force operations
        rigidBody2D.drag = currentRecord.DragValue;
        if (currentRecord.IsForceApplied)
        {
            rigidBody2D.AddForce(currentRecord.ForceVector, ForceMode2D.Force);
        }

        //Simulate the orthogonnal velocity adjustments
        rigidBody2D.velocity = currentRecord.VelocityVector;

        //Simulate the sterring operations
        rigidBody2D.MoveRotation(currentRecord.RotationAngle);
    }

    public void Record(PointInTime pointInTime)
    {
        if (!this.isRecordingMustBeStopped)
        {
            this.PointsInTime.Add(pointInTime);
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

    public void SaveReferencePlayerSettings()
    {
        this.PlayerCarReferencePosition = this.playerCarGameObject.transform.position;
        this.PlayerCarReferenceRotation = this.playerCarGameObject.transform.rotation;
    }

    public List<PointInTime> PointsInTime { get => pointsInTime; set => pointsInTime = value; }
    public Vector3 PlayerCarReferencePosition { get => playerCarReferencePosition; set => playerCarReferencePosition = value; }
    public Quaternion PlayerCarReferenceRotation { get => playerCarReferenceRotation; set => playerCarReferenceRotation = value; }
}
