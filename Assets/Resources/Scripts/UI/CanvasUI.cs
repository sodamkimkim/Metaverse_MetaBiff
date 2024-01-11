using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{
    public bool isUIOpen { get; set; }
    private void Awake()
    {
        isUIOpen = false;
    }
}
