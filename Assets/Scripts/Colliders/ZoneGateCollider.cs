using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoneGateCollider : MonoBehaviour
{

    //public static event Action<string> OnZoneGateEnter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagsConstants.PLAYER_TAG))
        {
            SceneManager.LoadScene(SceneConstants.RACE_SCENE_NAME);
        }
    }
}
