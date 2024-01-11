using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Market에서 item 클릭하면 MarketItemDetail띄워주는 클래스
public class ShowItemDetail : MonoBehaviour
{
    Button btn = null;
    string goName = null;
    Transform rootparentTr = null;

    // GameObject marketUI = null;
    // GameObject InventoryUI = null;

    GameObject itemDetails = null;
    private MarketManager marketManager = null;
    private void Awake()
    {
        marketManager = GameObject.FindWithTag("Market").gameObject.GetComponent<MarketManager>();
        btn = this.gameObject.GetComponent<Button>();
        goName = this.gameObject.name;
        rootparentTr = this.transform.root;
        if (rootparentTr.gameObject.name == "Canvas_Market")
        {
          //  marketUI = rootparentTr.Find("MarketUI").gameObject;
            itemDetails = rootparentTr.Find("MarketUI").gameObject.transform.Find("ItemDetails").gameObject;
            btn.onClick.AddListener(MarketItemDetailOpen);
        }
        else if(rootparentTr.gameObject.name == "Canvas_Player")
        {
            itemDetails = rootparentTr.Find("InventoryUI").gameObject.transform.Find("ItemDetails").gameObject;
            btn.onClick.AddListener(InventoryItemDetailOpen);
        }
    }

    private void MarketItemDetailOpen()
    {
        marketManager.CloseAllDetails();
        marketManager.OpenItemDetails(true);
        GameObject itemDetail = itemDetails.transform.Find(goName.Replace("(Clone)", "")).gameObject;
        itemDetail.SetActive(true);
    }
    private void InventoryItemDetailOpen()
    {
        // 모든 detail꺼주기 호출
        CloseAllDetails();
        // itemdetails는 켜주기
        OpenItemDetails(true);

        Debug.Log("Inventory ItemDetails open");
       
        GameObject itemDetail = itemDetails.transform.Find(goName.Replace("(Clone)","")).gameObject;
        itemDetail.SetActive(true);
    }
    private void CloseAllDetails()
    {
        ItemDetail[] itemDetailArr = null;
        itemDetailArr = itemDetails.GetComponentsInChildren<ItemDetail>();
        foreach (ItemDetail detail in itemDetailArr)
        {
            detail.gameObject.SetActive(false);
        }
    }
    private void OpenItemDetails(bool _bool)
    {
        itemDetails.SetActive(_bool);
    }
} // end of class
