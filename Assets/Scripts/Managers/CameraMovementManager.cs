using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementManager : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private float smoothTime = 0.15f;

    private void LateUpdate()
    {
        if (!GameManager.isGameInPause)
        {
            Vector2 temporaryCalculatedCameraPosition = Vector2.Lerp(transform.position, target.position, smoothTime);
            transform.position = new Vector3(temporaryCalculatedCameraPosition.x, temporaryCalculatedCameraPosition.y, transform.position.z);
        }  
    }
}
