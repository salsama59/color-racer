using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GhostCarSpawnerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ghostCarModel;
    [SerializeField]
    private GameObject nemesisGhostModel;
    public static event Func<string> OnghostSpawned;
    public static event Action OnPlayerCarReferenceSettingsUpdate;
    public static event Action<Animator, string> OnCarAnimationChange;
    private float timeElapsedForNemesisSpawn = 0f;
    private bool isNemesisGhostSpawned = false;
    private GameObject nemesisGhostGameObject;
    [SerializeField]
    private Transform nemesisDefaultSpawnPoint;

    private void OnEnable()
    {
        LapCollider.OnLapFinished += SpawnGhost;
        LapCollider.OnLapFinished += DestroyNemesis;
    }

    private void OnDisable()
    {
        LapCollider.OnLapFinished -= SpawnGhost;
        LapCollider.OnLapFinished -= DestroyNemesis;
    }

    private void FixedUpdate()
    {
        if (!GameManager.isGameInPause && GameManager.isRacePreparationDone && !this.isNemesisGhostSpawned)
        {
            this.timeElapsedForNemesisSpawn += Time.fixedDeltaTime;

            if (this.timeElapsedForNemesisSpawn >= GhostConstants.TIME_BEFORE_NEMESIS_GHOST_SPAWN_IN_SECONDS)
            {
                LapManager lapManager = GameObject.FindGameObjectWithTag(TagsConstants.LAP_MANAGER_TAG).GetComponent<LapManager>();

                CheckpointCollider checkpointCollider = GameObject.FindGameObjectsWithTag(TagsConstants.CHECKPOINT_TAG).Select(checkpointGameObject =>
                    {
                        return checkpointGameObject.GetComponent<CheckpointCollider>();
                    }
                ).Where(checkpointCollider =>
                {
                    return checkpointCollider.CheckpontIndex == lapManager.CurrentCheckpointIndex;
                }).FirstOrDefault();

                Vector3 spawnPosition = Vector3.zero;
                Quaternion spawnRotation = Quaternion.identity;

                if (checkpointCollider != null)
                {
                    spawnPosition = checkpointCollider.gameObject.transform.position;
                    spawnRotation = checkpointCollider.gameObject.transform.rotation;
                } else
                {
                    spawnPosition = nemesisDefaultSpawnPoint.position;
                    spawnRotation = nemesisDefaultSpawnPoint.rotation;
                }

                this.nemesisGhostGameObject = Instantiate(this.nemesisGhostModel, spawnPosition, spawnRotation);
                this.isNemesisGhostSpawned = true;
                this.timeElapsedForNemesisSpawn = 0f;
            }
        }
        
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

    public void DestroyNemesis()
    {
        if(this.nemesisGhostGameObject != null)
        {
            GameObject.Destroy(this.nemesisGhostGameObject);
        }

        //Prevent the nemesis future spawning since the first lap is done
        this.isNemesisGhostSpawned = true;
        this.timeElapsedForNemesisSpawn = 0f;

    }
}
