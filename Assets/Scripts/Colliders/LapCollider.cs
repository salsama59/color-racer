using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LapCollider : MonoBehaviour
{

    public static event Action OnLapFinished;
    public static event Func<bool> OnLapAuthenticityCheckOk;
    public static event Func<bool> OnTimerRequirementCheckOk;
    public static event Action<GameOverReasonEnum> OnGameOver;
    public static event Action OnBonusChoiceDisplay;
    public static event Action<bool> OnFuelRegenerationBonusEnd;
    public static event Action<bool> OnDamageRepairBonusEnd;
    public static event Action OnRaceBegining;
    public static event Action OnCarsMotionStop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.PLAYER_TAG))
        {
            if (OnLapFinished != null && OnTimerRequirementCheckOk != null && GameManager.isRaceAlreadyStarted)
            {

                if(OnTimerRequirementCheckOk.Invoke())
                {
                    if (OnLapAuthenticityCheckOk != null && OnLapAuthenticityCheckOk.Invoke())
                    {
                        if(OnCarsMotionStop != null)
                        {
                            OnCarsMotionStop.Invoke();
                        }

                        OnLapFinished.Invoke();

                        if (OnFuelRegenerationBonusEnd != null)
                        {
                            OnFuelRegenerationBonusEnd.Invoke(false);
                        }

                        if (OnDamageRepairBonusEnd != null)
                        {
                            OnDamageRepairBonusEnd.Invoke(false);
                        }

                        if (OnBonusChoiceDisplay != null)
                        {
                            OnBonusChoiceDisplay.Invoke();
                        }
                    }
                } else
                {
                    if(OnGameOver != null)
                    {
                        OnGameOver.Invoke(GameOverReasonEnum.TIMER);
                    }
                }
               
            } else if (OnRaceBegining != null && !GameManager.isRaceAlreadyStarted)
            {
                OnRaceBegining.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.PLAYER_TAG))
        {
            GameManager.isRaceAlreadyStarted = true;
        }
    }
}
