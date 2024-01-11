using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerKeyInpupt : MonoBehaviourPun
{
    [SerializeField]
    private UIManager uiManager_ = null;
    private PlayerChat playerChat_;
    private PlayerMove playerMove_ = null;
    private void Awake()
    {
        playerChat_ = GetComponent<PlayerChat>();
        playerMove_ = GetComponentInChildren<PlayerMove>();   
    }
    private void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            playerChat_.OnClickBtnSend();
        }

        // ESC·Î UI´Ý±â
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiManager_.CloseTopUI();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            uiManager_.OpenOrCloseInventory();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            uiManager_.OpenOrCloseEntireMap();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            uiManager_.OpenOrCloseNowWearing();
        }

    }
    public void SetInitialValues(UIManager _uiManager)
    {
        uiManager_ = _uiManager;    
    }
} // end of class
