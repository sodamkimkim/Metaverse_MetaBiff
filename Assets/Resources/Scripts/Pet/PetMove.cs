using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMove : MonoBehaviour
{

    private float YMIN = -0.05f;
    private float YMAX = 0.15f;
    private float curruntPosition = 0f;

    float direction = 0.1f;
    private void Start()
    {
        curruntPosition = transform.position.y;
        YMIN = curruntPosition -0.1f;
        YMAX = curruntPosition+ 0.1f;
    }
    private void Update()
    {
        AutoMovingProcess();
    }
    private void AutoMovingProcess()
    {
        curruntPosition += Time.deltaTime * direction;
       // Debug.Log(transform.position);
        if (curruntPosition >= YMAX)
        {
            direction *= -1;
            curruntPosition = YMAX;

        }
        else if (curruntPosition <= YMIN)
        {
            direction *= -1;
            curruntPosition = YMIN;
        }
        transform.position = new Vector3(transform.position.x, curruntPosition, transform.position.z);
       
    }
} // end of class
