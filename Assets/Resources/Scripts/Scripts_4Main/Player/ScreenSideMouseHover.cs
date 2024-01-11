using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenSideMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ScreenSideManager screenSideManager_;

    private void Awake()
    {
        screenSideManager_ = GetComponentInParent<ScreenSideManager>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.CompareTag("ScreenSide_Top"))
        {
            screenSideManager_.isScreenSideTop = true; // Debug.Log("isScreenSideTop == true");
        }
        if (this.gameObject.CompareTag("ScreenSide_Bottom"))
        {
            screenSideManager_.isScreenSideBottom = true; // Debug.Log("isScreenSideBottom == true");
        }
        if (this.gameObject.CompareTag("ScreenSide_Left"))
        {
            screenSideManager_.isScreenSideLeft = true; // Debug.Log("isScreenSideLeft == true");
        }
        if (this.gameObject.CompareTag("ScreenSide_Right"))
        {
            screenSideManager_.isScreenSideRight = true; // Debug.Log("isScreenSideRight == true");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.gameObject.CompareTag("ScreenSide_Top"))
        {
            screenSideManager_.isScreenSideTop = false;
        }
        if (this.gameObject.CompareTag("ScreenSide_Bottom"))
        {
            screenSideManager_.isScreenSideBottom = false;
        }
        if (this.gameObject.CompareTag("ScreenSide_Left"))
        {
            screenSideManager_.isScreenSideLeft = false;
        }
        if (this.gameObject.CompareTag("ScreenSide_Right"))
        {
            screenSideManager_.isScreenSideRight = false;
        }
    }
} // end of class
