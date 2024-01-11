using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private List<GameObject> currentUIList_ = new List<GameObject>();
    private int orderInLayer_ = 0;

    [SerializeField]
    private InventoryManager inventoryManager_ = null;
    private GameObject inventoryUIGo_ = null;
    [SerializeField]
    private EntireMapManager entireMapManager_ = null;
    private GameObject entireMapUIGo_ = null;
    [SerializeField]
    private NowWearingManager nowWearingManager_ = null;
    private GameObject nowWearingUIGo_ = null;

    [SerializeField]
    private Button inventoryBtnX_ = null;
    [SerializeField]
    private Button entireMapBtnX_ = null;
    [SerializeField]
    private Button nowWearingBtnX_ = null;

    // # Debugging
    [SerializeField]
    private TextMeshProUGUI debugTMP_ID_ = null;
    [SerializeField]
    private TextMeshProUGUI debugTMP_Nickname_ = null;
    [SerializeField]
    private TextMeshProUGUI debugTMP_PlayerIdx_ = null;
    [SerializeField]
    private TextMeshProUGUI debugTMP_Model_ = null;

    private void Awake()
    {
        currentUIList_.Clear();
        inventoryUIGo_ = inventoryManager_.GetUIGo();
        nowWearingUIGo_ = nowWearingManager_.GetUIGo();
        entireMapUIGo_ = entireMapManager_.GetUIGo();

        inventoryBtnX_.onClick.AddListener(OpenOrCloseInventory);
        entireMapBtnX_.onClick.AddListener(OpenOrCloseEntireMap);
        nowWearingBtnX_.onClick.AddListener(OpenOrCloseNowWearing);
    }

    public void SetIDDebug(string _str)
    {
        debugTMP_ID_.text = "ID : " + _str;
    }
    public void SetNicknameDebug(string _str)
    {
        debugTMP_Nickname_.text = "Nick : " + _str;
    }
    public void SetPlayerIdx(int _idx)
    {
        debugTMP_PlayerIdx_.text = "PlayerIdx : " + _idx;
    }
    public void SetModelDebug(string _str)
    {
        debugTMP_Model_.text = "Model : " + _str;
    }
    /// <summary>
    /// 현재 켜져있는 currentUIList 관리
    /// </summary>
    /// <param name="_uiGo"></param>
    /// <param name="_addList"> List에 add할지 여부</param>
    public void UpdateList(GameObject _uiGo, bool _addList)
    {
        if (_addList)
        {
            currentUIList_.Add(_uiGo);
            // _uiGo본인이나 부모에서 CanvasUI찾아서 CanvasGameObject 찾기
            GameObject canvasGo = _uiGo.GetComponentInParent<CanvasUI>().gameObject;
            canvasGo.transform.SetAsLastSibling(); // CanvasGameObect를 형제들 중 젤 마지막으로 하이라키 이동
            Canvas canvas = canvasGo.GetComponent<Canvas>();
            canvas.sortingLayerName = "UI";
            canvas.sortingOrder = orderInLayer_++;
        }
        else if (!_addList)
        {
            currentUIList_.Remove(_uiGo);
        }
    }
    /// <summary>
    /// 켜져 있는 UI Pop해서 SetActive(false)
    /// </summary>
    public void CloseTopUI()
    {
        if (currentUIList_.Count <= 0)
            return;
        else
        {
            GameObject removeGo = currentUIList_[currentUIList_.Count - 1];
            CanvasUI canvasUI = removeGo.GetComponentInParent<CanvasUI>();// 자신이나 부모 중에 있으면 가지고 옴
            if (canvasUI != null && canvasUI.isUIOpen)
            {
                // # remove 대상이 inventoryUI
                if (removeGo.gameObject.name.Equals(inventoryManager_.GetUIGo().name))
                {

                    inventoryManager_.CloseInvenUI();
                }
                // # remove 대상이 nowWearingUI
                else if (removeGo.gameObject.name.Equals(nowWearingManager_.GetUIGo().name))
                {
                    nowWearingManager_.CloseNowWearingUI();
                }
                // # remove 대상이 entireMapUI
                else if (removeGo.gameObject.name.Equals(entireMapManager_.GetUIGo().name))
                {
                    entireMapManager_.CloseEntireMapUI();
                }
                canvasUI.isUIOpen = false;
            }
            currentUIList_.Remove(removeGo);
        }
    }

    /// <summary>
    /// Open/Close Inventory
    /// </summary>
    public void OpenOrCloseInventory()
    {
        GameObject activeGo = inventoryManager_.GetUIGo();
        CanvasUI canvasUI = activeGo.GetComponentInParent<CanvasUI>();

        if (!canvasUI.isUIOpen)
        { // # 꺼져있으면 -> 켜기
            inventoryManager_.OpenInvenUI();
            bool setActive = true;
            UpdateList(activeGo, setActive);
            canvasUI.isUIOpen = setActive;
        }
        else if (canvasUI.isUIOpen)
        { // # 켜져있으면 -> 끄기
            inventoryManager_.CloseInvenUI();
            bool setActive = false;
            UpdateList(activeGo, setActive);
            canvasUI.isUIOpen = setActive;
        }
    }
    /// <summary>
    /// Open/Close EntireMap
    /// </summary>
    public void OpenOrCloseEntireMap()
    {
        GameObject activeGo = entireMapManager_.GetUIGo();
        CanvasUI canvasUI = activeGo.GetComponentInParent<CanvasUI>();

        if (!canvasUI.isUIOpen)
        { // # 꺼져있으면 -> 켜기
            entireMapManager_.OpenEntireMapUI();
            bool setActive = true;
            UpdateList(activeGo, setActive);
            canvasUI.isUIOpen = setActive;
        }
        else if (canvasUI.isUIOpen)
        { // # 켜져있으면 -> 끄기
            entireMapManager_.CloseEntireMapUI();
            bool setActive = false;
            UpdateList(activeGo, setActive);
            canvasUI.isUIOpen = setActive;
        }
    }
    /// <summary>
    /// Open/Close NowWearing
    /// </summary>
    public void OpenOrCloseNowWearing()
    {
        GameObject activeGo = nowWearingManager_.GetUIGo();
        CanvasUI canvasUI = activeGo.GetComponentInParent<CanvasUI>();

        if (!canvasUI.isUIOpen)
        { // # 꺼져있으면 -> 켜기
            nowWearingManager_.OpenNowWearingUI();
            bool setActive = true;
            UpdateList(activeGo, setActive);
            canvasUI.isUIOpen = setActive;
        }
        else if (canvasUI.isUIOpen)
        { // # 켜져있으면 -> 끄기
            nowWearingManager_.CloseNowWearingUI();
            bool setActive = false;
            UpdateList(activeGo, setActive);
            canvasUI.isUIOpen = setActive;
        }

    }

} // end of class
