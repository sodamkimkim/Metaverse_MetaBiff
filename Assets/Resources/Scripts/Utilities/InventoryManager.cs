using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Biff.BackgroundInfo;
using Unity.VisualScripting;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryUI_ = null;
    [SerializeField]
    private InventoryDB inventoryDB = null;
    private DropSlot[] dropSlotArr = null;

    private bool inventoryIsFull = false;
    private int firstEmptySlotIdx = 0;
    private GameObject[] itemsArr = null;
    int emptySlotCnt = 0;

    private void Awake()
    {
        dropSlotArr = inventoryUI_.GetComponentsInChildren<DropSlot>();
        itemsArr = Resources.LoadAll<GameObject>("Items");
        inventoryUI_.SetActive(false);
    }
    /// <summary>
    /// 켜고 끄는 대상이 되는 GameObject 반환
    /// </summary>
    /// <returns></returns>
    public GameObject GetUIGo()
    {
        return inventoryUI_;
    }
    public void GetInventoryInfo(string _nick)
    {
        inventoryDB.GetInventoryInfo(PlayerInfoManager.GetNickname());
    }
    public void OpenInvenUI()
    {
        inventoryUI_.SetActive(true);
    }
    public void CloseInvenUI()
    {
        inventoryUI_.SetActive(false);
    }
    public void UpdateInventoryInfo()
    {
        inventoryDB.UpdateInventoryInfo();
    }
    /// <summary>
    /// nowwearing info <-> inventory는 아이템을 생성이 아니라 부모를 옮겨주는 것.
    /// 즉, 이 함수는 instantiate안함!
    /// </summary>
    /// <param name="_inputItem"></param>
    public void SetItemWearingObjParentIntoInventory()
    {
        CheckInventoryFull();
        SearchEmptySlot();
        if (CheckInventoryFull() != true)
        {
            RectTransform parentRtr = dropSlotArr[firstEmptySlotIdx].GetComponent<RectTransform>();
            ItemFunction.wearingObj.transform.SetParent(parentRtr);
        }
        else
        { // # inventory == full이라면 아이템을 inventory에 못넣는다.
            return;
        }
    }
    public void SetItemParentIntoInventory()
    {
        SearchEmptySlot();
        if (CheckInventoryFull() != true)
        {
            RectTransform parentRtr = dropSlotArr[firstEmptySlotIdx].GetComponent<RectTransform>();
            NowWearingManager.PositionChangingItem.transform.SetParent(parentRtr);
            NowWearingManager.PositionChangingItem.transform.localPosition = Vector3.zero;
            // NowWearingManager.PositionChangingItem.GetComponent<DragItem>().enabled = true;
        }
    }
    /// <summary>
    /// 마켓에서 아이템 구매 후 인벤토리에 생성해주는 함수
    /// </summary>
    /// <param name="_inputItem"></param>
    public void PushIntoInventoryAfterPurchasing(string _inputItem)
    {   // # inventory full체크해서 full이면 push못하게 해줌(아이템 거래 못하도록)
        // # full 아니면 아이템 프리팹 생성해서 first empty slot 에 넣어줌 
        CheckInventoryFull();
        SearchEmptySlot();
        Debug.Log(" firstEmptySlotIdx : " + firstEmptySlotIdx);
        if (CheckInventoryFull() != true)
        {
            for (int i = 0; i < (int)ItemInfo.EItemName.Len; i++)
            {
                if (_inputItem.Equals(((ItemInfo.EItemName)i).ToString()))
                {
                    Instantiate(itemsArr[i], dropSlotArr[firstEmptySlotIdx].gameObject.transform);
                }
            }
        }
        else
        { // # inventory == full이라면
            return;
        }
        CheckInventoryFull();
        inventoryDB.UpdateInventoryInfo();
    }
    public void SearchEmptySlot()
    { // # 젤 첫 emtpy slot 찾아주는 함수
        for (int i = 0; i < dropSlotArr.Length; i++)
        {
            if (dropSlotArr[i].gameObject.GetComponentInChildren<DragItem>() == null)
            {
                //     Debug.Log("slot" + (i + 1) + " is empty");
                firstEmptySlotIdx = i; // * idx 값은 0 ~ 19 (db에 저장되는 이름이랑 다름 주의)
                inventoryIsFull = false;
                break;
            }
        }
    }
    public bool CheckInventoryFull()
    { // # inventory가 full 인지 체크해주는 함수
        emptySlotCnt = 0;
        for (int i = 0; i < dropSlotArr.Length; i++)
        {
            if (dropSlotArr[i].gameObject.GetComponentInChildren<DragItem>() == null)
            {
                ++emptySlotCnt;
            }
        }
        if (emptySlotCnt == 0)
        {
            inventoryIsFull = true;
            Debug.Log("inventory is full");
        }
        else
        {
            inventoryIsFull = false;
        }
        return inventoryIsFull;
    }
    /// <summary>
    ///  첫 empty slot 을 draggingobj의 부모로 지정해 주는 함수
    /// </summary>
    public void SetDraggingItemParent()
    { // #
        SearchEmptySlot();
        RectTransform parentRtr = dropSlotArr[firstEmptySlotIdx].GetComponent<RectTransform>();
        DragItem.SetDraggingObjPosition(parentRtr.position);
        DragItem.draggingObj.transform.SetParent(parentRtr);
    }

} // end of class
