using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lapCounterText;
    private int lapCounter = 0;
    private int currentCheckpointIndex = 0;
    private int targetCheckpointsCount = 1;

    private void Start()
    {
        GameObject[] checkpointObjects = GameObject.FindGameObjectsWithTag(TagsConstants.CHECKPOINT_TAG);
        this.targetCheckpointsCount = checkpointObjects.Length;
    }

    private void OnEnable()
    {
        LapCollider.OnLapFinished += UpdateLap;
        LapCollider.OnLapFinished += ResetCheckpointIndex;
        LapCollider.OnLapAuthenticityCheckOk += IsAllCheckpointsValidated;
        CheckpointCollider.OnCheckpointEnter += ValidateCheckpoint;
    }

    private void OnDisable()
    {
        LapCollider.OnLapFinished -= UpdateLap;
        LapCollider.OnLapFinished -= ResetCheckpointIndex;
        LapCollider.OnLapAuthenticityCheckOk -= IsAllCheckpointsValidated;
        CheckpointCollider.OnCheckpointEnter -= ValidateCheckpoint;
    }

    public void UpdateLap()
    {
        this.LapCounter++;
        this.lapCounterText.text = $"Laps: {this.LapCounter}";
    }

    public void ValidateCheckpoint(int checkpointIndexToValidate)
    {
        if(this.CurrentCheckpointIndex == checkpointIndexToValidate - 1)
        {
            this.CurrentCheckpointIndex = checkpointIndexToValidate;
        }
    }

    public bool IsAllCheckpointsValidated()
    {
        return CurrentCheckpointIndex == targetCheckpointsCount;
    }

    public void ResetCheckpointIndex()
    {
        this.CurrentCheckpointIndex = 0;
    }

    public int LapCounter { get => lapCounter; set => lapCounter = value; }
    public int CurrentCheckpointIndex { get => currentCheckpointIndex; set => currentCheckpointIndex = value; }
}
