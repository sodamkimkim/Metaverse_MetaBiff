using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static DB;

public class DB : MonoBehaviour
{
    [SerializeField]
    private JoinManager joinManager = null;
    // 로그인용 함수(id && pw)
    public void SearchUserInfo(LoginManager.UserInputData _userInputData)
    {
        StartCoroutine(LoginCoroutine(_userInputData));
    }
    private IEnumerator LoginCoroutine(LoginManager.UserInputData _userInputData)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", _userInputData.userID);
        form.AddField("pw", _userInputData.userPW);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:80/biffprj/login.php", form))
        {
            yield return www.SendWebRequest();
            if (CheckError(www))
            {
                Debug.Log(www.error);
            }
            else if (www.result.ToString().Equals("Success"))
            {
                Debug.Log("DB Connection Success");
                string data = www.downloadHandler.text;

                // Debug.Log(data);
                if (data.Equals("No UserInformation"))
                {
                    // 일치하는 User Information 없을 경우 Process
                    LoginManager.instance.NoUserInfoMessage();
                    // LoginManager호출
                }
                else
                {
                    // 회원 정보 조회 완료.
                    // 캐릭터 고르기 씬으로 전환
                    LoginManager.instance.LoginSuccess();

                }
            }

            www.Dispose();
        }
    }

    // id 중복체크용 함수(id)
    public void SearchUserID(string _userID)
    {
        StartCoroutine(CheckIDAvailabilityCoroutine(_userID));
    }
    private IEnumerator CheckIDAvailabilityCoroutine(string _userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", _userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:80/biffprj/idcheck.php", form))
        {
            yield return www.SendWebRequest();
            if (CheckError(www))
            {
                Debug.Log(www.error);
            }
            else if (www.result.ToString().Equals("Success"))
            {
                Debug.Log("DB Connection Success");
                string data = www.downloadHandler.text;

                Debug.Log(data);
                // Debug.Log(data);
                if (data.Equals("No UserInformation"))
                {
                    // 일치하는 User Information 없을 경우 ID is available
                    Debug.Log(data);
                    joinManager.IDIsAvailableFunction(true);
                }
                else
                {
                    Debug.Log(data);
                    // id정보가 이미 있으니 다른 것으로 바꿔라고 메시지 던지기
                    joinManager.IDIsAvailableFunction(false);
                }
            }
            www.Dispose();
        }
    }

    // 회원가입
    public void CreateNewAccount(JoinManager.UserJoinData _userJoinData)
    {
        StartCoroutine(JoinCoroutine(_userJoinData));
    }
    private IEnumerator JoinCoroutine(JoinManager.UserJoinData _userJoinData)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", _userJoinData.userId);
        form.AddField("pw", _userJoinData.userPw);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:80/biffprj/join.php", form))
        {
            yield return www.SendWebRequest();
            if (CheckError(www))
            {
                Debug.Log(www.error);
            }
            else if (www.result.ToString().Equals("Success"))
            {
                // database insert 
                Debug.Log("DB Connection Success");
                string data = www.downloadHandler.text;
                Debug.Log(data);
            }
            www.Dispose();
        }
    }
    private bool CheckError(UnityEngine.Networking.UnityWebRequest _www)
    {
        return _www.result == UnityWebRequest.Result.ConnectionError ||
            _www.result == UnityWebRequest.Result.DataProcessingError ||
            _www.result == UnityWebRequest.Result.ProtocolError;
    }

} // end of class
