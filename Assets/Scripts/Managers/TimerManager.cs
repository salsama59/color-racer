using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI lastTimerText;
    private float timeElapsed = 0;
    private float temporaryTimeCalculation;
    private float lastTimeElapsed = 0;

    private void OnEnable()
    {
        LapCollider.OnLapFinished += ResetCurrentTimer;
        LapCollider.OnLapFinished += UpdateTimerToBeat;
        LapCollider.OnTimerRequirementCheckOk += IsTimerCheckPasses;
    }

    private void OnDisable()
    {
        LapCollider.OnLapFinished -= ResetCurrentTimer;
        LapCollider.OnLapFinished -= UpdateTimerToBeat;
        LapCollider.OnTimerRequirementCheckOk -= IsTimerCheckPasses;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameInPause)
        {
            this.temporaryTimeCalculation += Time.deltaTime;

            if (this.temporaryTimeCalculation >= 0.5f)
            {
                this.TimeElapsed += 0.5f;
                this.temporaryTimeCalculation = 0f;
            }

            timerText.text = $"Current time : {this.TimeElapsed} seconds";
        }
        
    }

    public void ResetCurrentTimer()
    {
        this.LastTimeElapsed = this.TimeElapsed;
        this.TimeElapsed = 0f;
    }

    public void UpdateTimerToBeat()
    {
        lastTimerText.text = $"Time to beat : {this.LastTimeElapsed} seconds";
    }

    public bool IsTimerCheckPasses()
    {
        return this.LastTimeElapsed == 0f || this.TimeElapsed <= this.LastTimeElapsed;
    }

    public float TimeElapsed { get => timeElapsed; set => timeElapsed = value; }
    public float LastTimeElapsed { get => lastTimeElapsed; set => lastTimeElapsed = value; }
}
