using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.SceneManagement; 
using Biff.BackgroundInfo;

public class MyCharacDB : MonoBehaviour
{
    [SerializeField]
    private ChoosingMyCharacterManager choosingMyCharacterManager = null;
    [SerializeField]
    private GameObject mycharactersUI = null;
    private List<MyCharacDB.MyCharacterInfoDTO> myCharacList = new List<MyCharacDB.MyCharacterInfoDTO>();
    private void Awake()
    {
        PlayerInfoManager.myCharacsNowWearingDic_.Clear();
    }
    public struct MyCharacterInfoDTO
    {
        public string nickName;
        public string userId;
        public string model;

        public override string ToString()
        {
            return "nickName : " + nickName + ", userId : " + userId + ", model : " + model;
        }
    } // end of struct MyCharacterInfo
    public struct NowWearingDTO
    {
        public string nickname;
        public string clothes;
        public string hands;
        public string head;
        public string bag;
        public string pet;

        public NowWearingDTO(string nickname, string clothes, string hands, string head, string bag, string pet)
        {
            this.nickname = nickname;
            this.clothes = clothes;
            this.hands = hands;
            this.head = head;
            this.bag = bag;
            this.pet = pet;
        }

        public override string ToString()
        {
            return "nickname : " + nickname + ", clothes : " + clothes
                            + ", hands : " + hands + ", head : " + head + ", bag : " + bag
                            + ", pet : " + pet;
        }
    } // end of struct NowWearing
    // # MyCharacters 불러오는 함수
    public void GetMyCharacters(string _userId)
    {
        StartCoroutine(GetMyCharactersCoroutine(_userId));
        // StartCoroutine(GetMyCharactersCoroutine(testUserId)); // test용
    }
    private IEnumerator GetMyCharactersCoroutine(string _userId)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", _userId);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:80/biffprj/MyCharacs/searchmycharacters.php", form))
        {
            yield return www.SendWebRequest();
            if (DBUtility.CheckError(www))
            {
                Debug.Log(www.error);
            }
            else if (www.result.ToString().Equals("Success"))
            {
                // Debug.Log("DB Connection Success");
                string data = www.downloadHandler.text;

                // Debug.Log(data);
                if (data.Equals("No UserInformation"))
                {
                    // 일치하는 User Information 없을 경우 Process
                    choosingMyCharacterManager.NoCharacterProcess();
                }
                else
                {
                    // 리스트에 저장해두고 
                    List<MyCharacDB.MyCharacterInfoDTO> myCharacs = JsonConvert.DeserializeObject<List<MyCharacDB.MyCharacterInfoDTO>>(data);
                    myCharacList.Clear();
                    myCharacList = myCharacs;
                    MyCharacUI[] myCharacArr = mycharactersUI.GetComponentsInChildren<MyCharacUI>();

                    // # 캐릭터 3개 이상이면 더이상 만들지 못하게 버튼 막아줘야 함.
                    if (myCharacs.Count >= 3)
                    {
                        choosingMyCharacterManager.MoreCreateCharacter(myCharacs.Count, false);
                    }
                    else if (myCharacs.Count < 3)
                    {
                        choosingMyCharacterManager.MoreCreateCharacter(myCharacs.Count, true);
                    }

                    for (int i = 0; i < myCharacs.Count; i++)
                    {
                        Debug.Log(myCharacs[i].ToString());
                        // 받은 데이터 화면에 뿌려줌

                        // # 닉네임
                        TextMeshProUGUI tmpNick = myCharacArr[i].gameObject.transform.Find("TMP_Nick").gameObject.GetComponent<TextMeshProUGUI>();
                        tmpNick.text = myCharacs[i].nickName;
                        // # 모델
                        TextMeshProUGUI tmpModel = myCharacArr[i].gameObject.transform.Find("TMP_Model").gameObject.GetComponent<TextMeshProUGUI>();
                        tmpModel.text = myCharacs[i].model;

                        choosingMyCharacterManager.SettingBtnPicks();
                        GetNowWearing(myCharacs[i].nickName);
                    }
                    // Debug.Log(data);
                }
            }
            www.Dispose();
        }
        yield return null;
    }
    // # Character별 NowWearing정보 불러오는 함수
    public void GetNowWearing(string _nickName)
    {
        StartCoroutine(GetNowWearingCoroutine( _nickName));
    }
    private IEnumerator GetNowWearingCoroutine(string _nickName)
    {
        WWWForm form = new WWWForm();
        form.AddField("nickName", _nickName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:80/biffprj/MyCharacs/getnowwearing.php", form))
        {
            yield return www.SendWebRequest();
            if (DBUtility.CheckError(www))
            {
                Debug.Log(www.error);
            }
            else if (www.result.ToString().Equals("Success"))
            {
                Debug.Log("DB Connection Success");
                string data = www.downloadHandler.text;

                // Debug.Log(data);
                if (data.Equals("No UserInformation"))
                { // # 일치하는 nowWearing 정보 없을 경우 Process

                    Debug.Log("해당 닉네임의 nowWearing정보가 생성되어 있지 않습니다.");
                }
                else
                { // # wearing 정보 불러오기
                    List<MyCharacDB.NowWearingDTO> nowWearings = JsonConvert.DeserializeObject<List<MyCharacDB.NowWearingDTO>>(data);
                    MyCharacDB.NowWearingDTO nowWDto = new MyCharacDB.NowWearingDTO(
                        nowWearings[0].nickname, nowWearings[0].clothes, nowWearings[0].hands, nowWearings[0].head, nowWearings[0].bag, nowWearings[0].pet
                        );
                    
                    Debug.Log("@@@ " + data);
                    //nowWearingDic.Add(_nickName, nowWDto);
                    PlayerInfoManager.myCharacsNowWearingDic_.Add(_nickName, nowWDto);
                    //    MyCharacUI[] myCharacArr = mycharactersUI.GetComponentsInChildren<MyCharacUI>();
                    Debug.Log("!!!! nowWearingsDic"+ PlayerInfoManager.myCharacsNowWearingDic_[_nickName].ToString());
                    choosingMyCharacterManager.SetActivePicBtn(true);
                    
                }
            }
            www.Dispose();
        }
        yield return null;
    }
    /// <summary>
    ///  매개변수 받아서 nowWearing정보 static class에 저장하고 mainScene으로 들고갈 수 있게 해주는 함수
    /// </summary>
    /// <param name="_nick">선택된 캐릭터의 닉네임</param>
    public void UpdateMyCharacNowWearingInfo(string _nick)
    {
        PlayerInfoManager.SetNW_Clothes(PlayerInfoManager.myCharacsNowWearingDic_[_nick].clothes);
        PlayerInfoManager.SetNW_Hands(PlayerInfoManager.myCharacsNowWearingDic_[_nick].hands);
        PlayerInfoManager.SetNW_Head(PlayerInfoManager.myCharacsNowWearingDic_[_nick].head);
        PlayerInfoManager.SetNW_Bag(PlayerInfoManager.myCharacsNowWearingDic_[_nick].bag);
        PlayerInfoManager.SetNW_Pet(PlayerInfoManager.myCharacsNowWearingDic_[_nick].pet);
    }
    public void DeleteMyCharacter(string _deleteCharacNick)
    {
        StartCoroutine(DeleteMyCharacCoroutine(_deleteCharacNick));
    }
    private IEnumerator DeleteMyCharacCoroutine(string _deleteCharacNick)
    {
        WWWForm form = new WWWForm();
        form.AddField("nickname", _deleteCharacNick);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:80/biffprj/MyCharacs/deletemycharac.php", form))
        {
            yield return www.SendWebRequest();
            if (DBUtility.CheckError(www))
            {
                Debug.Log(www.error);
            }
            else if (www.result.ToString().Equals("Success"))
            {
                Debug.Log("DB Connection Success");
                string data = www.downloadHandler.text;
                Debug.Log(data);
                // character 나타나있는 정보 없애주기
                RemoveMyCharacterInfo(_deleteCharacNick);

            }
            www.Dispose();
        }
        yield return null;
    }
    public string GetPickedModel(int _btnIdx)
    {
        return myCharacList[_btnIdx].model;
    }
    private void RemoveMyCharacterInfo(string _nickName)
    {
        SceneManager.LoadScene("2_MyCharacters");
    }
} // end of class
