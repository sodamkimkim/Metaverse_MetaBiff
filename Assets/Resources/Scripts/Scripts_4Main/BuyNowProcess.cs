using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Biff.BackgroundInfo;

public class BuyNowProcess : MonoBehaviour
{
    [SerializeField]
    private InventoryManager inventoryManager = null;
    [SerializeField]
    private MoneyManager moneyManager = null;
    [SerializeField]
    private GameObject buyNowGo = null;
    [SerializeField]
    private GameObject itemDetails = null;
    ItemDetail[] itemDetailArrs = null;

    public void OpenBuyNowAsk()
    {
        UpdateStatusMsg("");
        buyNowGo.SetActive(true);
    }
    public void NotNow()
    {
        buyNowGo.SetActive(false);
    }
    public void OnClickBuyYes()
    {
        itemDetailArrs = itemDetails.GetComponentsInChildren<ItemDetail>();
        foreach (ItemDetail detail in itemDetailArrs)
        {
            if (detail.gameObject.activeSelf == true)
            {
                // item detail의 price정보 가져오기
                GameObject priceGo = detail.gameObject.transform.Find("Money").gameObject.transform.Find("Price").gameObject;
                string pricetxt = priceGo.GetComponent<TextMeshProUGUI>().text;

                // price 문자열 -> int
                pricetxt = pricetxt.Replace(",", "");
                // Debug.Log("price : " + int.Parse(pricetxt));
                int itemPrice = int.Parse(pricetxt);
                int playerMoney = PlayerInfoManager.GetMoney();
                if (playerMoney >= itemPrice)
                { // 가진돈 >= 가격이면 구매 절차 진행!
                    inventoryManager.SearchEmptySlot();
                   
                    if (inventoryManager.CheckInventoryFull() != true)
                    { // # inventory is not full
                        moneyManager.Pay(itemPrice);
                        inventoryManager.PushIntoInventoryAfterPurchasing(detail.gameObject.name.Replace("(Clone)", ""));
                        Debug.Log(detail.gameObject.name + " 구매 완료! price: " + itemPrice);

                        UpdateStatusMsg("Payment is success, Price : "+itemPrice.ToString());

                        // # ui꺼주기
                        buyNowGo.SetActive(false);
                    }
                    else 
                    { // # inventory is full
                        Debug.Log("Market: 당신은 inventory가 full이여서 거래 할 수 없습니다.");
                        UpdateStatusMsg("Your Inventory is full!!");
                    }
 
                }
                else
                { // # 돈부족
                    Debug.Log("돈이 부족합니다.");
                    UpdateStatusMsg("You are short of balance.");
                }

            }
        }
    }

    private void UpdateStatusMsg(string _msg)
    {
        TextMeshProUGUI tmpStatusMsg = buyNowGo.transform.Find("TMP_Status").gameObject.GetComponent<TextMeshProUGUI>();
        tmpStatusMsg.text = _msg;
        tmpStatusMsg.color = new Color(0f, 0f, 255f);
    }
} // end of class
