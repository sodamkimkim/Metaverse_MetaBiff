using Biff.BackgroundInfo;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInstantiateManager : MonoBehaviourPunCallbacks, IPunObservable
{
    // # PlayerPrefabs
    private GameObject playerGo_ = null;

    [SerializeField] private GameObject PlayerPrefab_ = null;
    [SerializeField] private GameObject PlayerModel_JC_ = null;
    [SerializeField] private GameObject PlayerModel_MDS_ = null;
    [SerializeField] private GameObject PlayerModel_KHS_ = null;
    [SerializeField] private GameObject PlayerModel_Iron_ = null;
    [SerializeField] private GameObject PlayerModel_HJW_ = null;
    [SerializeField] private GameObject PlayerModel_Navi_ = null;

    private readonly Vector3 playerInitialPos_ = new Vector3(3.24f, 2.795113f, 19.338f);
    private Vector3 modelLocalPos_ = new Vector3(0f, -0.445f, 0f);

    // # PlayerChat
    [SerializeField]
    private TMP_InputField chatField_ = null;
    [SerializeField]
    private Button btnSend_ = null;
    [SerializeField]
    private GameObject speechBubble_ = null;
    [SerializeField]
    private TextMeshProUGUI bubbleTxtTMP_ = null;
    // # Player KeyInput
    [SerializeField]
    private UIManager uiManager_ = null;
    // # CamRot
    [SerializeField]
    private ScreenSideManager screenSideManager_ = null;
    [SerializeField]
    private MarketManager marketManager_ = null;

    public static PlayerInstantiateManager instance
    {
        get
        {
            if (m_instance_ == null)
            {
                m_instance_ = FindObjectOfType<PlayerInstantiateManager>();
            }
            return m_instance_;
        }
    }
    private static PlayerInstantiateManager m_instance_;
    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    //[PunRPC]
    public void InstantiatePlayer()
    {

            //# table_characterInfo
            //--num, nickname, 캐릭터 pw, model(F_KimHyeSoo, F_Navi, M_HaJeongWoo, M_MaDongSuck, M_JangChen, M_IronMan)
            PhotonNetwork.NickName = PlayerInfoManager.GetNickname();
            playerGo_ = PhotonNetwork.Instantiate(PlayerPrefab_.name, playerInitialPos_, Quaternion.identity);
            //Transform playerGoTr = playerGo_.transform;
            int parentViewId = playerGo_.GetPhotonView().ViewID;
            GameObject modelPrefab = null;

            if (PlayerInfoManager.GetModel().Equals("F_KimHyeSoo"))
            { // 김혜수
                modelPrefab = PlayerModel_KHS_;
            }
            if (PlayerInfoManager.GetModel().Equals("F_Navi"))
            { // 나비
                modelPrefab = PlayerModel_Navi_;
            }
            if (PlayerInfoManager.GetModel().Equals("M_HaJeongWoo"))
            { // 하정우
                modelPrefab = PlayerModel_HJW_;
            }
            if (PlayerInfoManager.GetModel().Equals("M_MaDongSuck"))
            { // 마동석
                modelPrefab = PlayerModel_MDS_;
            }
            if (PlayerInfoManager.GetModel().Equals("M_JangChen"))
            { // 장첸
                modelPrefab = PlayerModel_JC_;
            }
            if (PlayerInfoManager.GetModel().Equals("M_Ironman"))
            { // 아이언맨
                modelPrefab = PlayerModel_Iron_;
            }
            if (modelPrefab != null)
            {
                GameObject modelGo = PhotonNetwork.Instantiate(modelPrefab.name, Vector3.zero, Quaternion.identity);
                int modelViewId = modelGo.GetPhotonView().ViewID;
                photonView.RPC("SetModelParent", RpcTarget.All, modelViewId, parentViewId);
                //SetModelParent();

                SetPlayerChatValues();
                SetKeyInputValues();
                SetCamRotValues();
                SetPlayerClickValues();
            }
        }


       [PunRPC]
    private void SetModelParent(int _modelViewId, int _parentViewId)
    {
        GameObject parentGo = PhotonView.Find(_parentViewId).gameObject;
        GameObject modelGo = PhotonView.Find(_modelViewId).gameObject;
        modelGo.transform.SetParent(parentGo.transform);
        modelGo.transform.localPosition = modelLocalPos_;

    }
    private void SetPlayerClickValues()
    {
        PlayerClick playerClick = playerGo_.GetComponent<PlayerClick>();
        playerClick.SetInitialValues(marketManager_);
    }
    private void SetPlayerChatValues()
    {
        PlayerChat playerChat = playerGo_.GetComponent<PlayerChat>();
        playerChat.SetInitialValues(chatField_, btnSend_, speechBubble_, bubbleTxtTMP_);
    }
    private void SetKeyInputValues()
    {
        PlayerKeyInpupt playerKeyInput = playerGo_.GetComponent<PlayerKeyInpupt>();
        playerKeyInput.SetInitialValues(uiManager_);
    }
    private void SetCamRotValues()
    {
        CamRot camRot = playerGo_.GetComponent<CamRot>();
        camRot.SetInitialValues(screenSideManager_);
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
} // end of class
