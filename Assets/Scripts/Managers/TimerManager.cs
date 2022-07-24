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
    }

    private void OnDisable()
    {
        LapCollider.OnLapFinished -= ResetCurrentTimer;
        LapCollider.OnLapFinished -= UpdateTimerToBeat;
    }

    // Update is called once per frame
    void Update()
    {
        this.temporaryTimeCalculation += Time.deltaTime;

        if (this.temporaryTimeCalculation >= 0.5f)
        {
            this.timeElapsed += 0.5f;
            this.temporaryTimeCalculation = 0f;
        }

        timerText.text = $"Time : {this.timeElapsed} seconds";
    }

    public void ResetCurrentTimer()
    {
        this.lastTimeElapsed = this.timeElapsed;
        this.timeElapsed = 0f;
    }

    public void UpdateTimerToBeat()
    {
        lastTimerText.text = $"Time : {this.lastTimeElapsed} seconds";
    }
}
