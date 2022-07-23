using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnLapFinished;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if(OnLapFinished != null)
            {
                OnLapFinished.Invoke();
            }
            
        }
    }
}
