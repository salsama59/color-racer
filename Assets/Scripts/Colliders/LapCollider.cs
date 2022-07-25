using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LapCollider : MonoBehaviour
{

    public static event Action OnLapFinished;
    public static event Func<bool> OnTimerRequirementCheckOk;
    public static event Action OnGameOver;
    public static event Action OnBonusChoiceDisplay;
    public static event Action<bool> OnFuelRegenerationBonusEnd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.PLAYER_TAG))
        {
            if (OnLapFinished != null && OnTimerRequirementCheckOk != null && GameManager.isRaceAlreadyStarted)
            {

                if(OnTimerRequirementCheckOk.Invoke())
                {
                    OnLapFinished.Invoke();

                    if(OnFuelRegenerationBonusEnd != null)
                    {
                        OnFuelRegenerationBonusEnd.Invoke(false);
                    }

                    if(OnBonusChoiceDisplay != null)
                    {
                        OnBonusChoiceDisplay.Invoke();
                    }

                } else
                {
                    if(OnGameOver != null)
                    {
                        OnGameOver.Invoke();
                    }
                }
               
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
