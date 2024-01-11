using System.Collections;
using System.Collections.Generic;
using TMPro;
using Biff.BackgroundInfo;
using UnityEngine;
public class MoneyManager:MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryUI = null;

    public  int money { get; private set; }

    public void Pay(int _price)
    {
        if (money >= _price)
        {
            money -= _price;
            Debug.Log("pay : " + _price + "≥≤¿∫ µ∑ : "+money);
            UpdateMoneyInfoUI();
        }
        else
        {
            Debug.Log("µ∑¿Ã ∫Œ¡∑«’¥œ¥Ÿ");
        }
    }
    public void SetMoneyInfo(int _money)
    {
        money = _money;
        PlayerInfoManager.SetMoney(money);
        UpdateMoneyInfoUI();
    }
    private void UpdateMoneyInfoUI()
    {
        inventoryUI.transform.Find("Money").GetComponentInChildren<TextMeshProUGUI>().text = money.ToString();
    }
} // end of class
