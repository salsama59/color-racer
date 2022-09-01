using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Linq;

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
    private TextMeshProUGUI gameOverReasonText;
    public static bool isRaceAlreadyStarted = false;
    public static bool isGameInPause = false;

    private bool isGameOver = false;

    public static event Action OnGameOverResultDisplayReady;
    public static event Action OnGameReset;
    public static event Action OnMainMenuReturn;

    public static bool isRacePreparationDone = false;

    private void OnEnable()
    {
        LapCollider.OnGameOver += GameOver;
        FuelManager.OnFuelShortage += GameOver;
        DamageManager.OnDamageBeyondRepair += GameOver;
        LapCollider.OnCarsMotionStop += StopCarsMotion;
        BonusManager.OnCarsMotionResume += ResumeCarsMotion;
    }

    private void OnDisable()
    {
        LapCollider.OnGameOver -= GameOver;
        FuelManager.OnFuelShortage -= GameOver;
        DamageManager.OnDamageBeyondRepair -= GameOver;
        LapCollider.OnCarsMotionStop -= StopCarsMotion;
        BonusManager.OnCarsMotionResume -= ResumeCarsMotion;
    }

    private void Update()
    {
        if (this.isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R) && OnGameReset != null)
            {
                OnGameReset.Invoke();
                this.ResetGameSettings();
            }

            if (Input.GetKeyDown(KeyCode.Escape) && OnMainMenuReturn != null)
            {
                OnMainMenuReturn.Invoke();
                this.ResetGameSettings();
            }
        }
    }

    public void GameOver(GameOverReasonEnum gameOverReason)
    {
        GameObject timerManagerGameObject = GameObject.FindGameObjectWithTag(TagsConstants.TIMER_MANAGER_TAG);
        TimerManager timerManagerScript = timerManagerGameObject.GetComponent<TimerManager>();

        GameObject lapManagerGameObject = GameObject.FindGameObjectWithTag(TagsConstants.LAP_MANAGER_TAG);
        LapManager lapManagerScript = lapManagerGameObject.GetComponent<LapManager>();

        //Because the game over is declared before the last lap count
        lapManagerScript.LapCounter++;

        this.lastLapNumberText.text = $"You did {lapManagerScript.LapCounter} laps!!!";
        this.lastTimerText.text = $"Your time was {timerManagerScript.TimeElapsed} seconds";

        switch (gameOverReason)
        {
            case GameOverReasonEnum.TIMER:
                this.gameOverReasonText.text = $"Your was'nt able to beat the following time : {timerManagerScript.LastTimeElapsed} seconds";
                break;
            case GameOverReasonEnum.FUEL:
                this.gameOverReasonText.text = $"Unfortunatelly your fuel level reached zero";
                break;
            case GameOverReasonEnum.DAMAGE:
                this.gameOverReasonText.text = $"Unfortunatelly your car damage reached 100%";
                break;
            default:
                break;
        }
        
        this.gameOverPanel.SetActive(true);
        this.gameMainUiPanel.SetActive(false);

        if(OnGameOverResultDisplayReady != null)
        {
            OnGameOverResultDisplayReady.Invoke();
        }

        isGameInPause = true;
        this.isGameOver = true;

        this.StopCarsMotion();
    }

    public void StopCarsMotion()
    {
        GameObject playerCarGameObject = GameObject.FindGameObjectWithTag(TagsConstants.PLAYER_TAG);
        CarMovementController carMovementControllerScript = playerCarGameObject.GetComponent<CarMovementController>();
        carMovementControllerScript.StopCarMotion();

        List<GhostCarMovementController> ghostCarMovementControllerScripts = GameObject
            .FindGameObjectsWithTag(TagsConstants.GHOST_CAR_TAG)
            .Select(ghostCarGameObject => {
                return ghostCarGameObject.GetComponent<GhostCarMovementController>();
            }).ToList();

        foreach (GhostCarMovementController ghostCarMovementControllerScript in ghostCarMovementControllerScripts)
        {
            ghostCarMovementControllerScript.StopGhostCarMotion();
        }

    }

    public void ResumeCarsMotion()
    {
        GameObject playerCarGameObject = GameObject.FindGameObjectWithTag(TagsConstants.PLAYER_TAG);
        CarMovementController carMovementControllerScript = playerCarGameObject.GetComponent<CarMovementController>();
        carMovementControllerScript.ResumeCarMotion();

        List<GhostCarMovementController> ghostCarMovementControllerScripts = GameObject
            .FindGameObjectsWithTag(TagsConstants.GHOST_CAR_TAG)
            .Select(ghostCarGameObject => { 
                return ghostCarGameObject.GetComponent<GhostCarMovementController>();
            }).ToList();

        foreach (GhostCarMovementController ghostCarMovementControllerScript in ghostCarMovementControllerScripts)
        {
            ghostCarMovementControllerScript.ResumeGhostCarMotion();
        }
    }

    private void ResetGameSettings()
    {
        isGameOver = false;
        isGameInPause = false;
        isRaceAlreadyStarted = false;
        isRacePreparationDone = false;
    }

}
