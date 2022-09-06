using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GhostCarSpawnerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ghostCarModel;
    public static event Func<string> OnghostSpawned;
    public static event Action OnPlayerCarReferenceSettingsUpdate;
    public static event Action<Animator, string> OnCarAnimationChange;

    private void OnEnable()
    {
        LapCollider.OnLapFinished += SpawnGhost;
    }

    private void OnDisable()
    {
        LapCollider.OnLapFinished -= SpawnGhost;
    }

    public void SpawnGhost()
    {
        GameObject carMovementRecordManagerObject = GameObject.FindGameObjectWithTag(TagsConstants.CAR_MOVEMENT_RECORD_MANAGER_TAG);
        CarMovementRecordManager carMovementRecordManagerScript = carMovementRecordManagerObject.GetComponent<CarMovementRecordManager>();

        GameObject ghostCar = Instantiate(ghostCarModel, carMovementRecordManagerScript.PlayerCarReferencePosition, carMovementRecordManagerScript.PlayerCarReferenceRotation);
        GhostCarMovementController ghostCarMovementControllerScript = ghostCar.AddComponent<GhostCarMovementController>();
        ghostCarMovementControllerScript.GhostInitialPosition = ghostCar.transform.position;
        ghostCarMovementControllerScript.GhostInitialRotation = ghostCar.transform.rotation;

        ghostCarMovementControllerScript.GhostCarMovementRecords = new List<PointInTime>(carMovementRecordManagerScript.PointsInTime);
        carMovementRecordManagerScript.PointsInTime.Clear();
        if(OnghostSpawned != null && OnCarAnimationChange != null)
        {
            Animator ghostCarAnimator = ghostCar.GetComponent<Animator>();
            string animationName = OnghostSpawned.Invoke();

            if(animationName == null)
            {
                ghostCarAnimator.SetBool("IsDefaultCar", true);
                ghostCarAnimator.SetBool("IsCarRunning", true);
            } else
            {
                ghostCarAnimator.SetBool("IsDefaultCar", true);
                ghostCarAnimator.SetBool("IsCarRunning", true);
                OnCarAnimationChange.Invoke(ghostCarAnimator, animationName);
            }
        }

        if(OnPlayerCarReferenceSettingsUpdate != null)
        {
            OnPlayerCarReferenceSettingsUpdate.Invoke();
        }
    }
}
