using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleCollider : MonoBehaviour
{

    public static event Action<float> OnPlayerCarSlowingObstacleHit;
    public static event Action<float> OnPlayerCarSlowingObstacleExit;
    public static event Action<float> OnPlayerCarDamagingObstacleHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isPlayerCollided = this.hasPlayerCollided(collision);

        if (isPlayerCollided)
        {
            if (this.gameObject.CompareTag(TagsConstants.SLOWING_OBSTACLE_TAG))
            {
                this.ManageSlowingObstacleHit();
            }
            else if (this.gameObject.CompareTag(TagsConstants.DAMAGING_OBSTACLE_TAG))
            {
                this.ManageDamagingObstacleHit();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bool isPlayerCollided = this.hasPlayerCollided(collision);

        if (isPlayerCollided)
        {
            if (this.gameObject.CompareTag(TagsConstants.SLOWING_OBSTACLE_TAG))
            {
                this.ManageSlowingObstacleExit();
            }
            
        }
    }

    private bool hasPlayerCollided(Collider2D collision)
    {
        return collision.gameObject.CompareTag(TagsConstants.PLAYER_TAG);
    }

    private void ManageSlowingObstacleHit()
    {
        if(OnPlayerCarSlowingObstacleHit != null)
        {
            OnPlayerCarSlowingObstacleHit.Invoke(ObstacleConstants.SPEED_REDUCE_PERCENTAGE);
        }
    }

    private void ManageSlowingObstacleExit()
    {
        if (OnPlayerCarSlowingObstacleExit != null)
        {
            //We add back the same value we reduced
            OnPlayerCarSlowingObstacleExit.Invoke(ObstacleConstants.SPEED_REDUCE_PERCENTAGE);
        }
    }

    private void ManageDamagingObstacleHit()
    {
        if (OnPlayerCarDamagingObstacleHit != null)
        {
            OnPlayerCarDamagingObstacleHit.Invoke(ObstacleConstants.HEALTH_DAMAGE_PERCENTAGE);
        }
    }
}
