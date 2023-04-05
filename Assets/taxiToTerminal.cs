using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taxiToTerminal : MonoBehaviour
{

    float speed = 1.25f;
    float turnSpeed = 20f;
    float maxTurnAngle = 90f;
    float currentTurnAngle = 0f;
    float currentTurnAnglePositiveY = 0f;
    float maxTurnAnglePositiveY = 90f;
    bool isTurning = false;
    //float[3] waypoint = [-10f, -12f, -14f];

    //TERMINAL 1
    bool terminal_1_avail = true;
    //TERMINAL 2
    bool terminal_2_avail = false;
    //TERMINAL 3
    bool terminal_3_avail = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 25);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.z > 34.5 && transform.position.x == 0 && transform.position.y == 0)
        {
            isTurning = true;
            
        }
        else if (transform.position.x <= -6.5 && transform.position.z >= 38 && transform.position.y == 0)
        {
            isTurning = true;
            maxTurnAngle = 180f;
            
        }
        else if (( transform.position.x <= -5 && transform.position.z <= 32.42 && transform.position.y == 0 ) &&
                   terminal_1_avail)
        {
            isTurning = true;
            turnSpeed = -turnSpeed; //banking in positive direction need positive speed.
        }

        Debug.Log("isTurning: " + isTurning);

        if (!isTurning)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else
        {
            float angleToRotate = -turnSpeed * Time.deltaTime;
            Debug.Log("angleToRotate: " + angleToRotate);

            if (angleToRotate < 0) //Banking turn in Negative Y rotation
            {
                if (currentTurnAngle + angleToRotate <= -maxTurnAngle)
                {
                    // clamp rotation angle to maxTurnAngle
                    angleToRotate = -maxTurnAngle - currentTurnAngle;
                    isTurning = false;
                }
                currentTurnAngle += angleToRotate;
            }
            else //Banking turn in positive Y rotation
            {
                // Banked turn to right
                if (currentTurnAnglePositiveY + angleToRotate >= maxTurnAnglePositiveY)
                {
                    angleToRotate = maxTurnAnglePositiveY - currentTurnAnglePositiveY;
                    isTurning = false;
                }
                currentTurnAnglePositiveY += angleToRotate;
            }
            
            transform.Rotate(Vector3.up, angleToRotate);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            //Debug
            Debug.Log("maxTurnAngle: " + currentTurnAngle);
            Debug.Log("currentTurnAngle: " + currentTurnAngle);
            //Debug.Log("angleToRotate: " + angleToRotate);
        }
    }
}