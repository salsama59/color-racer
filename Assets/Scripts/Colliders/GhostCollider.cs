using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GhostCollider : MonoBehaviour
{
    public static event Action<float> OnPlayerFuelDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(TagsConstants.PLAYER_TAG) && OnPlayerFuelDamage != null)
        {
            OnPlayerFuelDamage.Invoke(GhostConstants.GHOST_FUEL_DAMAGE_PERCENTAGE);
        }
    }

}
