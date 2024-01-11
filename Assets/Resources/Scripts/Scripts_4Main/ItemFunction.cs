using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Biff.BackgroundInfo;
using Unity.VisualScripting;

public class ItemFunction : MonoBehaviour, IPointerClickHandler
{
    static public GameObject wearingObj = null;
    private InventoryManager inventoryManager = null;
    private NowWearingManager nowWearingManager = null;

    private bool isHead = false;
    private bool isClothes = false;
    private bool isHands = false;
    private bool isBag = false;
    private bool isPet = false;
    private bool isFood = false;

    private GameObject nowWearingUI = null;
    private GameObject headUI = null;
    private GameObject clothesUI = null;
    private GameObject handsUI = null;
    private GameObject bagUI = null;
    private GameObject petUI = null;

    private GameObject playerItemGo = null;
    private Transform item_ClothesTr = null;
    private Transform item_HandsTr = null;
    private Transform item_HeadTr = null;
    private Transform item_BagTr = null;
    private Transform item_PetTr = null;
    private void Awake()
    {
        isHead = this.gameObject.CompareTag(ItemInfo.EItemTag.Head.ToString());
        isClothes = this.gameObject.CompareTag(ItemInfo.EItemTag.Clothes.ToString());
        isHands = this.gameObject.CompareTag(ItemInfo.EItemTag.Hands.ToString());
        isBag = this.gameObject.CompareTag(ItemInfo.EItemTag.Bag.ToString());
        isPet = this.gameObject.CompareTag(ItemInfo.EItemTag.Pet.ToString());
        isFood = this.gameObject.CompareTag(ItemInfo.EItemTag.Food.ToString());

        nowWearingUI = GameObject.FindGameObjectWithTag("Canvas_NowWearing").transform.Find("NowWearingUI").gameObject;
        headUI = nowWearingUI.transform.Find("Slots").transform.Find("Head").gameObject;
        clothesUI = nowWearingUI.transform.Find("Slots").transform.Find("Clothes").gameObject;
        handsUI = nowWearingUI.transform.Find("Slots").transform.Find("Hands").gameObject;
        bagUI = nowWearingUI.transform.Find("Slots").transform.Find("Bag").gameObject;
        petUI = nowWearingUI.transform.Find("Slots").transform.Find("Pet").gameObject;
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        nowWearingManager = GameObject.FindGameObjectWithTag("NowWearingManager").GetComponent<NowWearingManager>();
    }
    private void Start()
    {
        playerItemGo = GameObject.FindGameObjectWithTag("PlayerItems");
        item_ClothesTr = playerItemGo.transform.Find("Item_Clothes");
        item_HandsTr = playerItemGo.transform.Find("Item_Hands");
        item_HeadTr = playerItemGo.transform.Find("Item_Head");
        item_BagTr = playerItemGo.transform.Find("Item_Bag");
        item_PetTr = playerItemGo.transform.Find("Item_Pet");
    }
    public void OnPointerClick(PointerEventData _eventData)
    {
        if (_eventData.clickCount == 2)
        {
            Debug.Log(this.gameObject.name + " double clicked!");
            wearingObj = _eventData.selectedObject;
            if (this.gameObject.CompareTag("Clothes") || this.gameObject.CompareTag("Hands") || this.gameObject.CompareTag("Head") ||
                this.gameObject.CompareTag("Bag") || this.gameObject.CompareTag("Pet"))
            {
                OnWearingItemClickCallback();
            }
            else if (this.gameObject.CompareTag("Food"))
            {
                //TODO
            }
        }
    }

    private void OnWearingItemClickCallback()
    {
        if (playerItemGo != null)
        {
            DropSlot checkInInventory = this.transform.parent.gameObject.GetComponent<DropSlot>();
            // if wearing 상태o -> setparent(inventory slot의 transform)
            // if wearing 상태x -> setparent(해당 ui 의 transform)
            if (isClothes)
            { // clothes item
                if (checkInInventory == null)
                { // #  wearing 상태o
                    GetOffItem();
                }
                else
                { // #  wearing 상태x
                  // 1. nowwearingslot이 빈 slot 이면 그냥 넣고
                  // 2. 이미 아이템 있으면 바꿔주고
                    /*
                    # 이미 아이템이 있으면 바꿔준다? 
                     nowWearingslot에서
                    자식 search 해서 2개 이상이면 앞에 들어온 것을 다시 inventory에 push
                    ㄴ 배열[0] 과 [1]의 값을 바꿔서 [1]의 것을 다시 inventory에 push
                    */
                    if (nowWearingManager.CountClothesItemSlotChilds() == 0)
                    { // 1
                        this.transform.SetParent(clothesUI.transform);
                        PlayerInfoManager.SetNW_Clothes(this.gameObject.name.Replace("(Clone)", ""));
                    }
                    else if (nowWearingManager.CountClothesItemSlotChilds() > 0)
                    { // 2 now wearing slot에 아이템 이미 있으면 ChangeWearingItem();
                        this.transform.SetParent(clothesUI.transform);
                        PlayerInfoManager.SetNW_Clothes(this.gameObject.name.Replace("(Clone)", ""));
                        nowWearingManager.ChangeWearingItem(ItemInfo.EItemTag.Clothes.ToString());
                    }
                    GameObject itemPrefab = Resources.Load<GameObject>("itemModels\\" + this.gameObject.name.Replace("(Clone)", ""));
                    if (itemPrefab != null)
                    {
                        GameObject itemGo = Instantiate(itemPrefab, item_ClothesTr);
                        itemGo.gameObject.name = itemGo.gameObject.name.Replace("(Clone)", "");
                    }
                }
                this.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
            else if (isHands)
            {// hands item
                if (checkInInventory == null)
                { // #  wearing 상태o
                    GetOffItem();
                }
                else
                { // #  wearing 상태x
                  // TODO
                    if (nowWearingManager.CountHandsItemSlotChilds() == 0)
                    { // 1
                        this.transform.SetParent(handsUI.transform);
                        PlayerInfoManager.SetNW_Hands(this.gameObject.name.Replace("(Clone)", ""));
                    }
                    else if (nowWearingManager.CountClothesItemSlotChilds() > 0)
                    { // 2 now wearing slot에 아이템 이미 있으면 ChangeWearingItem();
                        this.transform.SetParent(handsUI.transform);
                        PlayerInfoManager.SetNW_Hands(this.gameObject.name.Replace("(Clone)", ""));
                        nowWearingManager.ChangeWearingItem(ItemInfo.EItemTag.Hands.ToString());
                    }
                    GameObject itemPrefab = Resources.Load<GameObject>("itemModels\\" + this.gameObject.name.Replace("(Clone)", ""));
                    if (itemPrefab != null)
                    {
                        GameObject itemGo = Instantiate(itemPrefab, item_HandsTr);
                        itemGo.gameObject.name = itemGo.gameObject.name.Replace("(Clone)", "");
                    }
                }
                this.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
            else if (isHead)
            {// head item
                if (checkInInventory == null)
                { // #  wearing 상태o
                    GetOffItem();
                }
                else
                { // #  wearing 상태x
                  // TODO
                    if (nowWearingManager.CountHeadItemSlotChilds() == 0)
                    { // 1
                        this.transform.SetParent(headUI.transform);
                        PlayerInfoManager.SetNW_Head(this.gameObject.name.Replace("(Clone)", ""));
                    }
                    else if (nowWearingManager.CountHeadItemSlotChilds() > 0)
                    { // 2 now wearing slot에 아이템 이미 있으면 ChangeWearingItem();
                        this.transform.SetParent(headUI.transform);
                        PlayerInfoManager.SetNW_Head(this.gameObject.name.Replace("(Clone)", ""));
                        nowWearingManager.ChangeWearingItem(ItemInfo.EItemTag.Head.ToString());
                    }
                    GameObject itemPrefab = Resources.Load<GameObject>("itemModels\\" + this.gameObject.name.Replace("(Clone)", ""));
                    if (itemPrefab != null)
                    {
                        GameObject itemGo = Instantiate(itemPrefab, item_HeadTr);
                        itemGo.gameObject.name = itemGo.gameObject.name.Replace("(Clone)", "");
                    }
                }
                this.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
            else if (isBag)
            {// bag item
                if (checkInInventory == null)
                { // #  wearing 상태o
                    GetOffItem();
                }
                else
                { // #  wearing 상태x
                  // TODO
                    if (nowWearingManager.CountBagItemSlotChilds() == 0)
                    { // 1
                        this.transform.SetParent(bagUI.transform);
                        PlayerInfoManager.SetNW_Bag(this.gameObject.name.Replace("(Clone)", ""));
                    }
                    else if (nowWearingManager.CountBagItemSlotChilds() > 0)
                    { // 2 now wearing slot에 아이템 이미 있으면 ChangeWearingItem();
                        this.transform.SetParent(bagUI.transform);
                        PlayerInfoManager.SetNW_Bag(this.gameObject.name.Replace("(Clone)", ""));
                        nowWearingManager.ChangeWearingItem(ItemInfo.EItemTag.Bag.ToString());
                    }
                    GameObject itemPrefab = Resources.Load<GameObject>("itemModels\\" + this.gameObject.name.Replace("(Clone)", ""));
                    if (itemPrefab != null)
                    {
                        GameObject itemGo = Instantiate(itemPrefab, item_BagTr);
                        itemGo.gameObject.name = itemGo.gameObject.name.Replace("(Clone)", "");
                    }
                }
                this.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
            else if (isPet)
            {// pet item
                if (checkInInventory == null)
                { // #  wearing 상태o
                    GetOffItem();
                }
                else
                { // #  wearing 상태x
                  // TODO
                    if (nowWearingManager.CountPetItemSlotChilds() == 0)
                    { // 1
                        this.transform.SetParent(petUI.transform);
                        PlayerInfoManager.SetNW_Pet(this.gameObject.name.Replace("(Clone)", ""));
                    }
                    else if (nowWearingManager.CountPetItemSlotChilds() > 0)
                    { // 2 now wearing slot에 아이템 이미 있으면 ChangeWearingItem();
                        this.transform.SetParent(petUI.transform);
                        PlayerInfoManager.SetNW_Pet(this.gameObject.name.Replace("(Clone)", ""));
                        nowWearingManager.ChangeWearingItem(ItemInfo.EItemTag.Pet.ToString());
                    }
                    GameObject itemPrefab = Resources.Load<GameObject>("itemModels\\" + this.gameObject.name.Replace("(Clone)", ""));
                    if (itemPrefab != null)
                    {
                        GameObject itemGo = Instantiate(itemPrefab, item_PetTr);
                        itemGo.gameObject.name = itemGo.gameObject.name.Replace("(Clone)", "");
                    }
                }
                this.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
            else if (isFood)
            {
                // TODO
                Debug.Log("Food Item");
            }
        }

    }
    public void GetOffItem()
    {
        inventoryManager.SetItemWearingObjParentIntoInventory();
        ItemModel itemModel = null;
        if (isClothes)
        {
            PlayerInfoManager.SetNW_Clothes("null");
            itemModel = item_ClothesTr.GetComponentInChildren<ItemModel>();
        }
        else if (isHands)
        {
            PlayerInfoManager.SetNW_Hands("null");
            itemModel = item_HandsTr.GetComponentInChildren<ItemModel>();
        }
        else if (isHead)
        {
            PlayerInfoManager.SetNW_Head("null");
            itemModel = item_HeadTr.GetComponentInChildren<ItemModel>();
        }
        else if (isBag)
        {
            PlayerInfoManager.SetNW_Bag("null");
            itemModel = item_BagTr.GetComponentInChildren<ItemModel>();
        }
        else if (isPet)
        {
            PlayerInfoManager.SetNW_Pet("null");
            itemModel = item_PetTr.GetComponentInChildren<ItemModel>();
        }
        else if (isFood)
        {
            // ?
        }
        Destroy(itemModel.gameObject);
    }

} // end of class
