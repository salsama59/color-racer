using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LapCollider : MonoBehaviour
{

    public static event Action OnLapFinished;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.PLAYER_TAG))
        {
            if (OnLapFinished != null && GameManager.isRaceAlreadyStarted)
            {
                OnLapFinished.Invoke();
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
