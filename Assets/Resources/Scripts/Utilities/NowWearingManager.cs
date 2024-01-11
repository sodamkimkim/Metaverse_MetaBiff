using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Biff.BackgroundInfo;
using UnityEngine.UI;
using Unity.VisualScripting;

public class NowWearingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject nowWearingUIGo_ = null;
    public static GameObject PositionChangingItem = null;
    private InventoryManager inventoryManager = null;
    [SerializeField]
    private NowWearingDB nowWearingDB = null;
    private GameObject[] itemsArr = null;

    [SerializeField]
    private GameObject clothesUI = null;
    [SerializeField]
    private GameObject handsUI = null;
    [SerializeField]
    private GameObject headUI = null;
    [SerializeField]
    private GameObject bagUI = null;
    [SerializeField]
    private GameObject petUI = null;

    /// <summary>
    /// 켜고 끄는 대상이 되는 GameObject 반환
    /// </summary>
    /// <returns></returns>
    public GameObject GetUIGo()
    {
        return nowWearingUIGo_;
    }

    private void Awake()
    {
        nowWearingUIGo_.SetActive(false);
        itemsArr = Resources.LoadAll<GameObject>("Items");
        //Debug.Log(nowWearingUI.gameObject.name);

        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
    }
    private void Start()
    {
    }
    private void Update()
    {
        SetItemPosFixed();
    }
    /// <summary>
    /// nowwearing 아이템은 slot밖으로 움직일 수없게 고정해주는 함수
    /// </summary>
    private void SetItemPosFixed()
    {
        if (clothesUI.GetComponentInChildren<DragItem>() != null)
        {
            DragItem dragItem = clothesUI.GetComponentInChildren<DragItem>();
            dragItem.transform.localPosition = Vector3.zero;
        }
        if (handsUI.GetComponentInChildren<DragItem>() != null)
        {
            DragItem dragItem = handsUI.GetComponentInChildren<DragItem>();
            dragItem.transform.localPosition = Vector3.zero;
        }
        if (headUI.GetComponentInChildren<DragItem>() != null)
        {
            DragItem dragItem = headUI.GetComponentInChildren<DragItem>();
            dragItem.transform.localPosition = Vector3.zero;
        }
        if (bagUI.GetComponentInChildren<DragItem>() != null)
        {
            DragItem dragItem = bagUI.GetComponentInChildren<DragItem>();
            dragItem.transform.localPosition = Vector3.zero;
        }
        if (petUI.GetComponentInChildren<DragItem>() != null)
        {
            DragItem dragItem = petUI.GetComponentInChildren<DragItem>();
            dragItem.transform.localPosition = Vector3.zero;
        }
    }
    public void UpdateNowWearingInfo(string _nick)
    {
        nowWearingDB.UpdateNowWearingInfo(_nick);
    }
    public void OpenNowWearingUI()
    {
        nowWearingUIGo_.SetActive(true);
    }
    public void CloseNowWearingUI()
    {
        nowWearingDB.UpdateNowWearingInfo(PlayerInfoManager.GetNickname());
        nowWearingUIGo_.SetActive(false);
    }
    /// <summary>
    /// nowwearing slot에 아이템 생성해주는 함수
    /// </summary>
    public void InstsantiateNowWearingItem()
    {
        for (int i = 0; i < (int)ItemInfo.EItemName.Len; i++)
        {
            //  Debug.Log("#######MyNowWearingInfo.clothes : " + MyNowWearingInfo.clothes);
            if (PlayerInfoManager.GetNW_Clothes().Equals(((ItemInfo.EItemName)i).ToString()))
            {
                GameObject go = Instantiate(itemsArr[i], clothesUI.transform);
                go.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
            if (PlayerInfoManager.GetNW_Head().Equals(((ItemInfo.EItemName)i).ToString()))
            {
                GameObject go = Instantiate(itemsArr[i], headUI.transform);
                go.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
            if (PlayerInfoManager.GetNW_Hands().Equals(((ItemInfo.EItemName)i).ToString()))
            {
                GameObject go = Instantiate(itemsArr[i], handsUI.transform);
                go.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
            if (PlayerInfoManager.GetNW_Bag().Equals(((ItemInfo.EItemName)i).ToString()))
            {
                GameObject go = Instantiate(itemsArr[i], bagUI.transform);
                go.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
            if (PlayerInfoManager.GetNW_Pet().Equals(((ItemInfo.EItemName)i).ToString()))
            {
                GameObject go = Instantiate(itemsArr[i], petUI.transform);
                go.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
        }
    }
    /// <summary>
    /// nowearing의 cloth 슬롯에 담긴 아이템 수 카운트
    /// </summary>
    /// <returns></returns>
    public int CountClothesItemSlotChilds()
    {
        DragItem[] childItems = clothesUI.GetComponentsInChildren<DragItem>();
        return childItems.Length;
    }
    /// <summary>
    /// nowearing의 head 슬롯에 담긴 아이템 수 카운트
    /// </summary>
    /// <returns></returns>
    public int CountHeadItemSlotChilds()
    {
        DragItem[] childItems = headUI.GetComponentsInChildren<DragItem>();
        return childItems.Length;
    }
    /// <summary>
    /// nowearing의 hands 슬롯에 담긴 아이템 수 카운트
    /// </summary>
    /// <returns></returns>
    public int CountHandsItemSlotChilds()
    {
        DragItem[] childItems = handsUI.GetComponentsInChildren<DragItem>();
        return childItems.Length;
    }
    /// <summary>
    /// nowearing의 bag 슬롯에 담긴 아이템 수 카운트
    /// </summary>
    /// <returns></returns>
    public int CountBagItemSlotChilds()
    {
        DragItem[] childItems = bagUI.GetComponentsInChildren<DragItem>();
        return childItems.Length;
    }
    /// <summary>
    /// nowearing의 pet 슬롯에 담긴 아이템 수 카운트
    /// </summary>
    /// <returns></returns>
    public int CountPetItemSlotChilds()
    {
        DragItem[] childItems = petUI.GetComponentsInChildren<DragItem>();
        return childItems.Length;
    }
    /// <summary>
    /// wearing slot에 이미 item있으면 old item이랑 new item이랑 교체해주는 함수
    /// </summary>
    public void ChangeWearingItem(string _ItmeTagAndSlotName)
    {
        if (clothesUI.gameObject.name == _ItmeTagAndSlotName)
        {
            DragItem[] dragItems = clothesUI.GetComponentsInChildren<DragItem>();
            Debug.Log("leng: " + dragItems.Length);
            DragItem temp = dragItems[0];
            dragItems[0] = dragItems[1];
            dragItems[1] = temp;

            // inventory로 되돌려 주기
            PositionChangingItem = dragItems[1].gameObject;
            inventoryManager.SetItemParentIntoInventory();
        }

        if (handsUI.gameObject.name == _ItmeTagAndSlotName)
        {
            DragItem[] dragItems = handsUI.GetComponentsInChildren<DragItem>();
            DragItem temp = dragItems[0];
            dragItems[0] = dragItems[1];
            dragItems[1] = temp;

            // inventory로 되돌려 주기
            PositionChangingItem = dragItems[1].gameObject;
            inventoryManager.SetItemParentIntoInventory();
        }
        if (headUI.gameObject.name == _ItmeTagAndSlotName)
        {
            DragItem[] dragItems = headUI.GetComponentsInChildren<DragItem>();
            DragItem temp = dragItems[0];
            dragItems[0] = dragItems[1];
            dragItems[1] = temp;

            // inventory로 되돌려 주기
            PositionChangingItem = dragItems[1].gameObject;
            inventoryManager.SetItemParentIntoInventory();
        }
        if (bagUI.gameObject.name == _ItmeTagAndSlotName)
        {
            DragItem[] dragItems = bagUI.GetComponentsInChildren<DragItem>();
            DragItem temp = dragItems[0];
            dragItems[0] = dragItems[1];
            dragItems[1] = temp;

            // inventory로 되돌려 주기
            PositionChangingItem = dragItems[1].gameObject;
            inventoryManager.SetItemParentIntoInventory();
        }
        if (petUI.gameObject.name == _ItmeTagAndSlotName)
        {
            DragItem[] dragItems = petUI.GetComponentsInChildren<DragItem>();
            DragItem temp = dragItems[0];
            dragItems[0] = dragItems[1];
            dragItems[1] = temp;

            // inventory로 되돌려 주기
            PositionChangingItem = dragItems[1].gameObject;
            inventoryManager.SetItemParentIntoInventory();
        }

    }
} // end of class
