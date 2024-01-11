using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntireMapManager : MonoBehaviour
{
    [SerializeField]
    private GameObject entireMapUIGo_ = null;
    private void Awake()
    {
        entireMapUIGo_.SetActive(false);
    }
    /// <summary>
    /// 켜고 끄는 대상이 되는 GameObject 반환
    /// </summary>
    /// <returns></returns>
    public GameObject GetUIGo()
    {
        return entireMapUIGo_;
    }

    public void OpenEntireMapUI()
    {
        entireMapUIGo_.SetActive(true);
    }
    public void CloseEntireMapUI()
    {
        entireMapUIGo_.SetActive(false);
    }
} // end of class
