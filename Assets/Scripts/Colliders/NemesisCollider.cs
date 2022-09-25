using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemesisCollider : MonoBehaviour
{
    public static event Action<GameOverReasonEnum> OnNemesisHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.PLAYER_TAG) && OnNemesisHit != null)
        {
            OnNemesisHit.Invoke(GameOverReasonEnum.NEMESIS);
        }
    }
}
