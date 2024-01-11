using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPosition : MonoBehaviour
{
    private Transform characterTr = null;
    private RectTransform rt = null;
    Camera cam; 
    private void Awake()
    {
        cam = Camera.main;
        rt = GetComponent<RectTransform>();
        characterTr = GameObject.Find(this.gameObject.name).gameObject.transform;
        //this.gameObject.SetActive(true);
    }
    private void Update()
    {
        
        SetUIPosition();
    }
    private void SetUIPosition()
    {
        Vector3 characterScreenPos = Camera.main.WorldToScreenPoint(characterTr.position);
        characterScreenPos.y += 420f; 
        transform.position = characterScreenPos;
    }
} // end of class
