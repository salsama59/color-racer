using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GhostCollider : MonoBehaviour
{
    public static event Action<float> OnPlayerFuelDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(OnPlayerFuelDamage != null)
        {
            OnPlayerFuelDamage.Invoke(GhostConstants.GHOST_FUEL_DAMAGE_PERCENTAGE);
        }
    }

}
