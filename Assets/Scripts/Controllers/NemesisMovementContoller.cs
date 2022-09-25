using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NemesisMovementContoller : MonoBehaviour
{
  
    private Tilemap tilemap;
    private PathFindingManager pathFindingManager;
    private float elapsedTime = 0f;
    private float speed = 12f;
    private GameObject targetCar = null;
    private List<Vector3> locations = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        pathFindingManager = GameObject.FindGameObjectWithTag(TagsConstants.PATH_FINDING_MANAGER_TAG).GetComponent<PathFindingManager>();
        this.targetCar = GameObject.FindGameObjectWithTag(TagsConstants.PLAYER_TAG);
        tilemap = GameObject.FindGameObjectWithTag("BaseTilemap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.isGameInPause)
        {
            this.elapsedTime += Time.fixedDeltaTime;

            Vector3Int currentCellPos;
            Vector3Int target;

            if (locations.Count == 0)
            {
                currentCellPos = tilemap.WorldToCell(transform.position);
                target = tilemap.WorldToCell(this.targetCar.transform.position);
                target.z = 0;
                this.locations = pathFindingManager.GetCalculatedMapPath(new Vector2Int(currentCellPos.x, currentCellPos.y), new Vector2Int(target.x, target.y));
            }
           
            transform.position = Vector3.MoveTowards(transform.position, locations[0], this.speed * Time.fixedDeltaTime);

            Vector3 direction = (locations[0] - transform.position);
            direction.Normalize();
            bool isRotationValid = true;

            if (direction.x == 0f && direction.y == 0f)
            {
                isRotationValid = false;
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (isRotationValid)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }

            if (Vector3.Distance(transform.position, locations[0]) <= 0)
            {
                locations.RemoveAt(0);
            } 

            if (this.elapsedTime >= 5f)
            {
                this.elapsedTime = 0;
                currentCellPos = tilemap.WorldToCell(transform.position);
                target = tilemap.WorldToCell(this.targetCar.transform.position);
                target.z = 0;
                this.locations = pathFindingManager.GetCalculatedMapPath(new Vector2Int(currentCellPos.x, currentCellPos.y), new Vector2Int(target.x, target.y));
            }
        }
        
    }

}
