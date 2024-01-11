using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using Biff.BackgroundInfo;
public class InventoryDB : MonoBehaviour
{
    [SerializeField]
    private InventoryManager inventoryManager = null;
    [SerializeField]
    private MoneyManager moneyManager = null;

    [SerializeField]
    private GameObject inventoryUI = null;
    private DropSlot[] dropSlotArr = null;
    private GameObject[] itemsArr = null;

    public struct CharacterInventoryInfo
    {
        public string userId;
        public string characterNick;
        public int money;
        public string itemslot1;
        public string itemslot2;
        public string itemslot3;
        public string itemslot4;
        public string itemslot5;
        public string itemslot6;
        public string itemslot7;
        public string itemslot8;
        public string itemslot9;
        public string itemslot10;
        public string itemslot11;
        public string itemslot12;
        public string itemslot13;
        public string itemslot14;
        public string itemslot15;
        public string itemslot16;
        public string itemslot17;
        public string itemslot18;
        public string itemslot19;
        public string itemslot20;

        public override string ToString()
        {
            return
                "userId : " + userId
                + ", characterNick : " + characterNick
                + ", money : " + money
                + ", itemslot1 : " + itemslot1
                + ", itemslot2 : " + itemslot2
                + ", itemslot3 : " + itemslot3
                + ", itemslot4 : " + itemslot4
                + ", itemslot5 : " + itemslot5
                + ", itemslot6 : " + itemslot6
                + ", itemslot7 : " + itemslot7
                + ", itemslot8 : " + itemslot8
                + ", itemslot9 : " + itemslot9
                + ", itemslot10 : " + itemslot10
                + ", itemslot11 : " + itemslot11
                + ", itemslot12 : " + itemslot12
                + ", itemslot13 : " + itemslot13
                + ", itemslot14 : " + itemslot14
                + ", itemslot15 : " + itemslot15
                + ", itemslot16 : " + itemslot16
                + ", itemslot17 : " + itemslot17
                + ", itemslot18 : " + itemslot18
                + ", itemslot19 : " + itemslot19
                + ", itemslot20 : " + itemslot20;
        }
    }
    private void Awake()
    {
        dropSlotArr = inventoryUI.GetComponentsInChildren<DropSlot>();
        itemsArr = Resources.LoadAll<GameObject>("Items");
    }
    public void GetInventoryInfo(string _nickName)
    {
        StartCoroutine(GetInventoryInfoCoroutine(_nickName));
    }
    private IEnumerator GetInventoryInfoCoroutine(string _nickName)
    {
        WWWForm form = new WWWForm();
        form.AddField("nickName", _nickName);
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:80/biffprj/Inventory/GetInventoryInfo.php", form))
        {
            yield return www.SendWebRequest();
            if (DBUtility.CheckError(www))
            {
                Debug.Log(www.error);
            }
            else if (www.result.ToString().Equals("Success"))
            {
               //  Debug.Log("DB Connection Success");
                string data = www.downloadHandler.text;

                // Debug.Log(data);
                if (data.Equals("No CharacterInventoryInformation"))
                {
                    Debug.Log("No CharacterInventoryInformation");
                }
                else
                {
                    // Debug.Log(data);
                    List<InventoryDB.CharacterInventoryInfo> inventories = JsonConvert.DeserializeObject<List<InventoryDB.CharacterInventoryInfo>>(data);
                    for (int i = 0; i < inventories.Count; i++)
                    {
                        //Debug.Log(inventories[i].ToString());
                        // # dbÀÇ itemslot1ÀÌ¸é slot1¿¡ ¸ÅÄª½ÃÄÑ¼­ ³Ö¾îÁà¾ß ÇÔ
                        // Debug.Log("itemslot1 : " + inventories[i].itemslot1);

                        // # money
                        moneyManager.SetMoneyInfo(inventories[i].money);

                        // null ÀÌ¸é? 
                        for (int j = 0; j < (int)ItemInfo.EItemName.Len; j++)
                        {
                            // ÄÚµå ÁÙÀÌ´Â ¹æ¹ý...............? 
                            if (inventories[i].itemslot1.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 1¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[0].gameObject.transform);
                            }
                            if (inventories[i].itemslot2.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 2¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[1].gameObject.transform);
                            }
                            if (inventories[i].itemslot3.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 3¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[2].gameObject.transform);
                            }
                            if (inventories[i].itemslot4.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 4¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[3].gameObject.transform);
                            }
                            if (inventories[i].itemslot5.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 5¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[4].gameObject.transform);
                            }
                            if (inventories[i].itemslot6.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 6¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[5].gameObject.transform);
                            }
                            if (inventories[i].itemslot7.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 7¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[6].gameObject.transform);
                            }
                            if (inventories[i].itemslot8.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 8¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[7].gameObject.transform);
                            }
                            if (inventories[i].itemslot9.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 9¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[8].gameObject.transform);
                            }
                            if (inventories[i].itemslot10.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 10¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[9].gameObject.transform);
                            }
                            if (inventories[i].itemslot11.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 11¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[10].gameObject.transform);
                            }
                            if (inventories[i].itemslot12.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 12¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[11].gameObject.transform);
                            }
                            if (inventories[i].itemslot13.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 13¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[12].gameObject.transform);
                            }
                            if (inventories[i].itemslot14.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 14¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[13].gameObject.transform);
                            }
                            if (inventories[i].itemslot15.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 15¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[14].gameObject.transform);
                            }
                            if (inventories[i].itemslot16.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 16¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[15].gameObject.transform);
                            }
                            if (inventories[i].itemslot17.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 17¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[16].gameObject.transform);
                            }
                            if (inventories[i].itemslot18.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 18¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[17].gameObject.transform);
                            }
                            if (inventories[i].itemslot19.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 19¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[18].gameObject.transform);
                            }
                            if (inventories[i].itemslot20.Equals(((ItemInfo.EItemName)j).ToString()))
                            { // 20¹ø ½½·Ô
                                Instantiate(itemsArr[j], dropSlotArr[19].gameObject.transform);
                            }

                        }
                    }
                }
            }
            www.Dispose();
        }
        inventoryManager.CheckInventoryFull();
        inventoryManager.SearchEmptySlot();
    }

    public void UpdateInventoryInfo()
    {
        InventoryDB.CharacterInventoryInfo invenInfo = new InventoryDB.CharacterInventoryInfo();
        invenInfo.userId = PlayerInfoManager.GetID();
        invenInfo.characterNick = PlayerInfoManager.GetNickname();
        invenInfo.money = PlayerInfoManager.GetMoney();

        //.. ¸®ÆÑÅä¸µ
        if (dropSlotArr[0].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot1 = dropSlotArr[0].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot1 = "null";
        }
        if (dropSlotArr[1].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot2 = dropSlotArr[1].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot2 = "null";
        }
        if (dropSlotArr[2].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot3 = dropSlotArr[2].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot3 = "null";
        }
        if (dropSlotArr[3].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot4 = dropSlotArr[3].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot4 = "null";
        }
        if (dropSlotArr[4].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot5 = dropSlotArr[4].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot5 = "null";
        }
        if (dropSlotArr[5].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot6 = dropSlotArr[5].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot6 = "null";
        }
        if (dropSlotArr[6].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot7 = dropSlotArr[6].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot7 = "null";
        }
        if (dropSlotArr[7].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot8 = dropSlotArr[7].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot8 = "null";
        }
        if (dropSlotArr[8].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot9 = dropSlotArr[8].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot9 = "null";
        }
        if (dropSlotArr[9].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot10 = dropSlotArr[9].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot10 = "null";
        }
        if (dropSlotArr[10].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot11 = dropSlotArr[10].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot11 = "null";
        }
        if (dropSlotArr[11].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot12 = dropSlotArr[11].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot12 = "null";
        }
        if (dropSlotArr[12].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot13 = dropSlotArr[12].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot13 = "null";
        }
        if (dropSlotArr[13].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot14 = dropSlotArr[13].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot14 = "null";
        }
        if (dropSlotArr[14].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot15 = dropSlotArr[14].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot15 = "null";
        }
        if (dropSlotArr[15].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot16 = dropSlotArr[15].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot16 = "null";
        }
        if (dropSlotArr[16].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot17 = dropSlotArr[16].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot17 = "null";
        }
        if (dropSlotArr[17].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot18 = dropSlotArr[17].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot18 = "null";
        }
        if (dropSlotArr[18].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot19 = dropSlotArr[18].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot19 = "null";
        }
        if (dropSlotArr[19].gameObject.GetComponentInChildren<DragItem>() != null)
        {
            invenInfo.itemslot20 = dropSlotArr[19].gameObject.GetComponentInChildren<DragItem>().gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            invenInfo.itemslot20 = "null";
        }
        StartCoroutine(UpdateInventoryInfoCoroutine(invenInfo));
    }
    private IEnumerator UpdateInventoryInfoCoroutine(CharacterInventoryInfo _invenInfo)
    {
        WWWForm form = new WWWForm();
        form.AddField("nickName", _invenInfo.characterNick);
        form.AddField("money", _invenInfo.money);
        form.AddField("itemslot1", _invenInfo.itemslot1);
        form.AddField("itemslot2", _invenInfo.itemslot2);
        form.AddField("itemslot3", _invenInfo.itemslot3);
        form.AddField("itemslot4", _invenInfo.itemslot4);
        form.AddField("itemslot5", _invenInfo.itemslot5);
        form.AddField("itemslot6", _invenInfo.itemslot6);
        form.AddField("itemslot7", _invenInfo.itemslot7);
        form.AddField("itemslot8", _invenInfo.itemslot8);
        form.AddField("itemslot9", _invenInfo.itemslot9);
        form.AddField("itemslot10", _invenInfo.itemslot10);
        form.AddField("itemslot11", _invenInfo.itemslot11);
        form.AddField("itemslot12", _invenInfo.itemslot12);
        form.AddField("itemslot13", _invenInfo.itemslot13);
        form.AddField("itemslot14", _invenInfo.itemslot14);
        form.AddField("itemslot15", _invenInfo.itemslot15);
        form.AddField("itemslot16", _invenInfo.itemslot16);
        form.AddField("itemslot17", _invenInfo.itemslot17);
        form.AddField("itemslot18", _invenInfo.itemslot18);
        form.AddField("itemslot19", _invenInfo.itemslot19);
        form.AddField("itemslot20", _invenInfo.itemslot20);


        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:80/biffprj/Inventory/UpdateInventoryInfo.php", form))
        {
            yield return www.SendWebRequest();
            if (DBUtility.CheckError(www))
            {
                Debug.Log(www.error);
            }
            else if (www.result.ToString().Equals("Success"))
            {
                Debug.Log("DB Connection Success");
                string data = www.downloadHandler.text;

                Debug.Log("@@@? "+ data);
            }
            www.Dispose();
        }
    }

} // end of class
