using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementManager : MonoBehaviour
{

    [SerializeField]
    private float carTranslationSpeed = 5;
    [SerializeField]
    private float carRotationSpeed = 100;
    [SerializeField]
    private bool pauseMove = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //Forward
            transform.Translate(Vector2.up * carTranslationSpeed * Time.deltaTime);
            //Rotate
            this.RotateCar();
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            //Backward
            transform.Translate(Vector2.down * carTranslationSpeed *Time.deltaTime);
            //Rotate
            this.RotateCar();
        }
    }


    void RotateCar()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Turn right
            transform.Rotate(Vector3.back * carRotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Turn left
            transform.Rotate(Vector3.forward * carRotationSpeed * Time.deltaTime);
        }
    }
}
