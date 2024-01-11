using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSlot : MonoBehaviour
{
    private RectTransform rtr = null;
    private readonly float magneticDist = 70f;
    private InventoryManager inventoryManager = null;
    private void Awake()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").gameObject.GetComponent<InventoryManager>();
        rtr = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (DragItem.draggingObj == null)
        {
            return;
        }

        float dist = Vector3.Distance(this.rtr.position, DragItem.GetDraggingObjPosition());
        if (dist < magneticDist)
        {
            DragItem.SetDraggingObjPosition(this.rtr.position);
            DragItem.draggingObj.transform.SetParent(this.rtr);
            DragItem[] childArr = this.gameObject.GetComponentsInChildren<DragItem>();
            if (childArr.Length > 1)
            {
            // 자식에 이미 drag item 이 있으면 가장 가까운 or first empty slot 에 넣어주기
                inventoryManager.SetDraggingItemParent();
            }
        }
    }
} // end of class
