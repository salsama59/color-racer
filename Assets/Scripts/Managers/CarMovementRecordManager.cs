using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementRecordManager : MonoBehaviour
{
    [SerializeField]
    private bool isRewinding = false;
    private List<PointInTime> pointsInTime;
    //private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        this.pointsInTime = new List<PointInTime>();
      //  this.rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartRewind();
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            StopRewind();
        }
        
    }

    private void FixedUpdate()
    {
        if (this.isRewinding)
        {
            Rewind();
        } else
        {
            Record();
        }
    }


    private void Rewind()
    {
        if(pointsInTime.Count > 0)
        {
            transform.position = pointsInTime[0].Position;
            transform.rotation = pointsInTime[0].Rotation;
            pointsInTime.RemoveAt(0);
        } else
        {
            StopRewind();
        }
    }

    void Record()
    {
        this.pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    public void StartRewind()
    {
        this.isRewinding = true;
       // this.rigidbody2D
    }

    public void StopRewind()
    {
        this.isRewinding = false;
        // this.rigidbody2D
    }
}
