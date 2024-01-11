using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Biff.BackgroundInfo;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using JetBrains.Annotations;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameManager instance
    {
        get
        {
            if (m_instance_ == null)
            {
                m_instance_ = FindObjectOfType<GameManager>();
            }
            return m_instance_;
        }
    }
    private static GameManager m_instance_;
    [SerializeField] private UIManager uiManager_ = null;
    [SerializeField] private NowWearingManager nowWearingManager_ = null;
    [SerializeField] private InventoryManager inventoryManager_ = null;
    [SerializeField] private Button btnBefore_ = null;
    private PlayerInstantiateManager playerInstantiateManager_ = null;
    private int playerCount_;
    private void Awake()
    {
        playerInstantiateManager_ = FindObjectOfType<PlayerInstantiateManager>();
        if (instance != this)
        {
            Destroy(gameObject);
        }

        btnBefore_.onClick.AddListener(OnClickBtnBefore);
        //SetCharacterTestDatas();
        Debug.Log("UserId: " + PlayerInfoManager.GetID());
        Debug.Log("Nick: " + PlayerInfoManager.GetNickname());
        Debug.Log("Model: " + PlayerInfoManager.GetModel());
        SetDebugUI();

        //photonView.RPC("InstantiatePlayer", RpcTarget.All);
    }
    private void Start()
    {

   // photonView.RPC("InstantiatePlayer", RpcTarget.All);

        PhotonNetwork.IsMessageQueueRunning = true;
      playerInstantiateManager_.InstantiatePlayer();
        inventoryManager_.GetInventoryInfo(PlayerInfoManager.GetNickname());
        nowWearingManager_.InstsantiateNowWearingItem();
 //       PlayerInfoManager.IsInGame(false);

    }

    private void Update()
    {
        uiManager_.SetPlayerIdx(PhotonNetwork.CurrentRoom.PlayerCount);
    }
    private void SetDebugUI()
    {
        uiManager_.SetIDDebug(PlayerInfoManager.GetID());
        uiManager_.SetNicknameDebug(PlayerInfoManager.GetNickname());
        uiManager_.SetModelDebug(PlayerInfoManager.GetModel());
    }

    /// <summary>
    /// Test data 넣는 함수
    /// </summary>
    private void SetCharacterTestDatas()
    {
        // character 기본 정보
        PlayerInfoManager.SetID("theka265");
        PlayerInfoManager.SetNickname("sodam1");
        // MyCharacterInfo.SetModel("M_MaDongSuck");
        //MyCharacterInfo.SetModel("F_KimHyeSoo");
        //MyCharacterInfo.SetModel("F_Navi");
        //MyCharacterInfo.SetModel("M_HaJeongWoo");
        PlayerInfoManager.SetModel("M_JangChen");
        //MyCharacterInfo.SetModel("M_Ironman");

        // nowWearinginfo setting
        PlayerInfoManager.SetNW_Clothes("null");
        PlayerInfoManager.SetNW_Hands("null");
        PlayerInfoManager.SetNW_Head("null");
        PlayerInfoManager.SetNW_Bag(ItemInfo.EItemName.A_Wing.ToString());
        PlayerInfoManager.SetNW_Pet(ItemInfo.EItemName.P_Slime.ToString());
        Debug.Log("My now wearing: " + PlayerInfoManager.GetNowWearing());
    }

    private void OnClickBtnBefore()
    { // # 인벤토리 정보 db저장 
        inventoryManager_.UpdateInventoryInfo();
        nowWearingManager_.UpdateNowWearingInfo(PlayerInfoManager.GetNickname());
        // # 씬 전환
        SceneManager.LoadScene("2_MyCharacters");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // stream.SendNext(playerCount_);
            //stream.SendNext(redScore);
            //stream.SendNext(blueScore);
            //stream.SendNext(gameNowtime);
            //stream.SendNext(playerGoList.Count);
            //stream.SendNext(isGameStart);
        }
        else
        {
            //playerCount_ = (int)stream.ReceiveNext();

            //redScore = (int)stream.ReceiveNext();
            //blueScore = (int)stream.ReceiveNext();
            //gameNowtime = (float)stream.ReceiveNext();
            //playerIdx = (int)stream.ReceiveNext();
            //isGameStart = (bool)stream.ReceiveNext();
            //UIManager.instance.SetEndTime(gameEndRunTime);
            //UIManager.instance.SetNowTime(gameNowtime);
            ////  UIManager.instance.SetplayerIdx(playerIdx);

        }
    }
    public override void OnLeftRoom()
    {
        OnClickBtnBefore();
    }
} // end of class
