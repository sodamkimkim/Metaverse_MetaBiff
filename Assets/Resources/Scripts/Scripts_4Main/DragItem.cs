using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image img = null;
    private Vector3 offset = Vector3.zero;
    private RectTransform rtr = null;

    static public GameObject draggingObj = null;

    private void Awake()
    {
        rtr = GetComponent<RectTransform>();
        img = GetComponent<Image>();
    }


    public static Vector3 GetDraggingObjPosition()
    {
        return draggingObj.transform.position;
    }
    public static void SetDraggingObjPosition(Vector3 _pos)
    {
        draggingObj.transform.position = _pos;
    }
    public void OnBeginDrag(PointerEventData _eventData)
    {
        offset = (Vector2)rtr.position - _eventData.position;
        img.raycastTarget = false;
        draggingObj = gameObject;

    }

    public void OnDrag(PointerEventData _eventData)
    {
        rtr.position = _eventData.position + (Vector2)offset;
        // transform.SetParent(null);
    }
    public void OnEndDrag(PointerEventData _eventData)
    {
        img.raycastTarget = true;
        draggingObj = null;
    }

} // end of class
