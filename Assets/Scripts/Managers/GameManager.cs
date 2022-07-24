using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject gameMainUiPanel;
    [SerializeField]
    private TextMeshProUGUI lastLapNumberText;
    [SerializeField]
    private TextMeshProUGUI lastTimerText;
    [SerializeField]
    private TextMeshProUGUI timerToBeatText;
    public static bool isRaceAlreadyStarted = false;
    public static bool isGameInPause = false;

    public static event Action OnGameOverResultDisplayReady;

    private void OnEnable()
    {
        LapCollider.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        LapCollider.OnGameOver -= GameOver;
    }

    public void GameOver()
    {
        GameObject timerManagerGameObject = GameObject.FindGameObjectWithTag(TagsConstants.TIMER_MANAGER_TAG);
        TimerManager timerManagerScript = timerManagerGameObject.GetComponent<TimerManager>();

        GameObject lapManagerGameObject = GameObject.FindGameObjectWithTag(TagsConstants.LAP_MANAGER_TAG);
        LapManager lapManagerScript = lapManagerGameObject.GetComponent<LapManager>();

        //Because the game over is declared before the last lap count
        lapManagerScript.LapCounter++;

        this.lastLapNumberText.text = $"You did {lapManagerScript.LapCounter} laps!!!";
        this.lastTimerText.text = $"Your time was {timerManagerScript.TimeElapsed} seconds";
        this.timerToBeatText.text = $"Your was'nt able to beat the following time : {timerManagerScript.LastTimeElapsed} seconds";
        this.gameOverPanel.SetActive(true);
        this.gameMainUiPanel.SetActive(false);

        if(OnGameOverResultDisplayReady != null)
        {
            OnGameOverResultDisplayReady.Invoke();
        }

        isGameInPause = true;
    }
}
