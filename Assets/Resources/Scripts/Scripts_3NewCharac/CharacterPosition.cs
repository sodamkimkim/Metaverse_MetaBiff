using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPosition : MonoBehaviour
{
    Camera mainCam = null;
    private void Awake()
    {
        mainCam = Camera.main;
    }
    private void Update()
    {
        LookAtCamera();
      
    }
    private void LookAtCamera()
    {
        Vector3 targetPos;
        targetPos = new Vector3(mainCam.transform.position.x, transform.position.y, mainCam.transform.position.z);
        transform.LookAt(targetPos);
    }
} // end of class
