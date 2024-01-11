using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// MarketUI에서 Tab누르면 해당하는 슬롯들 보여주는 클래스
public class MarketUITab : MonoBehaviour
{
    // Tab buttons
    [SerializeField]
    private Button tabBtn_Clothes = null;
    [SerializeField]
    private Button tabBtn_Access = null;
    [SerializeField]
    private Button tabBtn_Food = null;
    [SerializeField]
    private Button tabBtn_Pets = null;
    private List<Button> tabButtons = new List<Button>();

    // Tab panels
    [SerializeField]
    private GameObject tab_ClothesPan = null;
    [SerializeField]
    private GameObject tab_AccessPan = null;
    [SerializeField]
    private GameObject tab_FoodPan = null;
    [SerializeField]
    private GameObject tab_PetsPan = null;
    private List<GameObject> tabList = new List<GameObject>();

    // 버튼 활성화 컬러
    Color btnActiveColor = new Color(0, 0, 0, 255f);
    Color textActiveColor = new Color(255, 255, 255, 0.9f);
    // 버튼 비활성화 컬러
    Color btnDisabledColor = new Color(0, 0, 0, 0f);
    Color textDisabledColor = new Color(255, 255, 255, 0.1f);

    private void Awake()
    {
        tabButtons.Add(tabBtn_Clothes);
        tabButtons.Add(tabBtn_Access);
        tabButtons.Add(tabBtn_Food);
        tabButtons.Add(tabBtn_Pets);

        tabList.Add(tab_ClothesPan);
        tabList.Add(tab_AccessPan);
        tabList.Add(tab_FoodPan);
        tabList.Add(tab_PetsPan);
    }
    private void BtnDisableSetting()
    {
        // 버튼
        foreach (Button btn in tabButtons)
        {
            Image btnImg = btn.gameObject.GetComponent<Image>();
            btnImg.color = btnDisabledColor;

            TextMeshProUGUI text = btn.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            text.color = textDisabledColor;
        }
    }
    private void PanDisableSetting()
    {
        // 패널
        foreach (GameObject go in tabList)
        {
            go.SetActive(false);
        }
    }
    private void OpenThisTabSetting(Button _tabBtn, GameObject _tabPan)
    {
        BtnDisableSetting();
        Image activeBtnImg = _tabBtn.gameObject.GetComponent<Image>();
        activeBtnImg.color = btnActiveColor;

        TextMeshProUGUI text = _tabBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.color = textActiveColor;

        PanDisableSetting();
        _tabPan.SetActive(true);

    }
    public void OpenTabClothes()
    {
        OpenThisTabSetting(tabBtn_Clothes, tab_ClothesPan);
    }

    public void OpenTabAccessory()
    {
        OpenThisTabSetting(tabBtn_Access, tab_AccessPan);
    }
    public void OpenTabFood()
    {
        OpenThisTabSetting(tabBtn_Food, tab_FoodPan);
    }
    public void OpenTabPets()
    {
        OpenThisTabSetting(tabBtn_Pets, tab_PetsPan);
    }
} // end of class
