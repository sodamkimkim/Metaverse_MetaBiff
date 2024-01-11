using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private NowWearingManager nowWearingManager = null;
    private InventoryManager inventoryManager = null;
    private void Awake()
    {
        nowWearingManager = GameObject.FindGameObjectWithTag("NowWearingManager").gameObject.GetComponent<NowWearingManager>();
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").gameObject.GetComponent<InventoryManager>();
    }
}
