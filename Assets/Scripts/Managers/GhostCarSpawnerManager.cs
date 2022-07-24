using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCarSpawnerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ghostCarModel;
   
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
        GameObject ghostCar = Instantiate(ghostCarModel, Vector2.zero, Quaternion.identity);
        GhostCarMovementController ghostCarMovementControllerScript = ghostCar.AddComponent<GhostCarMovementController>();
        GameObject carMovementRecordManagerObject = GameObject.FindGameObjectWithTag(TagsConstants.CAR_MOVEMENT_RECORD_MANAGER_TAG);
        CarMovementRecordManager carMovementRecordManagerScript = carMovementRecordManagerObject.GetComponent<CarMovementRecordManager>();
        ghostCarMovementControllerScript.GhostCarMovementRecords = new List<PointInTime>(carMovementRecordManagerScript.PointsInTime);
        carMovementRecordManagerScript.PointsInTime.Clear();
    }
}
