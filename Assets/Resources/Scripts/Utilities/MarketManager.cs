using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public enum EItemCategory { Top, Bottom, Accessory, Food, Pets, Len }

    /// <summary>
    /// items 폴더에 저장된 프리팹 순서대로 enum 작성할 것
    /// </summary>

    [SerializeField]
    private GameObject marketUIGo = null;
    private bool isOpen = false;

    GameObject itemDetails = null;
    ItemDetail[] itemDetailArr = null;
    BuyNowProcess buyNowProcess = null;

    private MarketUITab marketUITab = null;

    private void Awake()
    {
        marketUITab = this.gameObject.GetComponent<MarketUITab>();
        itemDetails = marketUIGo.transform.Find("ItemDetails").gameObject;
        buyNowProcess = this.gameObject.GetComponent<BuyNowProcess>();
    }
    public void OpenMarketPan()
    {
        // 기본으로 Clothes tab을 오픈
        marketUITab.OpenTabClothes();
        // 모든 디테일 다꺼주기
        CloseAllDetails();
        OpenItemDetails(false);
        isOpen = true;
        Debug.Log("Open MarketPan");
        if (isOpen == true)
        {
            // open Market
            marketUIGo.SetActive(true);
        }
    }
    public void CloseAllDetails()
    {
        // Debug.Log("Close All Details");
        itemDetailArr = itemDetails.GetComponentsInChildren<ItemDetail>();
        foreach (ItemDetail detail in itemDetailArr)
        {
            detail.gameObject.SetActive(false);
        }
    }
    public void OpenItemDetails(bool _bool)
    {
        itemDetails.SetActive(_bool);
    }
    public void CloseMarketPan()
    {
        CloseAllDetails();
        OpenItemDetails(false);
        buyNowProcess.NotNow();
        isOpen = false;
        Debug.Log("Close MarketPan");
        if (isOpen == false)
        {
            // close Market
            marketUIGo.SetActive(false);
        }
    }

} // end of class
