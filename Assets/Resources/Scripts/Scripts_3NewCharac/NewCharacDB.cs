using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static CreateCharacManager;

public class NewCharacDB : MonoBehaviour
{
    [SerializeField]
    private CreateCharacManager createCharacManager = null;
    //private string testUserId = "theka265";

    public void CheckNickNameAvailability(string _nickName)
    {
        StartCoroutine(CheckNickNameAvailabilityCoroutine(_nickName));
    }
    private IEnumerator CheckNickNameAvailabilityCoroutine(string _nickName)
    {
        WWWForm form = new WWWForm();
        form.AddField("nickname", _nickName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:80/biffprj/CreateCharac/CheckID.php", form))
        {
            yield return www.SendWebRequest();
            if(CheckError(www))
            {
                Debug.Log(www.error);
            }
            else if(www.result.ToString().Equals("Success"))
            {
                Debug.Log("DB Connection Success");
                string data = www.downloadHandler.text;
                if (data.Equals("No UserInformation"))
                {
                    // 일치하는 닉네임정보가 없을 경우 닉네임 is available
                    Debug.Log(data);
                   createCharacManager.NickNameIsAvailableFunction(true);
                }
                else
                {
                    Debug.Log(data);
                    // 입력한 닉네임정보가 이미 있으니 다른 것으로 바꿔라고 메시지 던지기
                    createCharacManager.NickNameIsAvailableFunction(false);
                }
            }
            www.Dispose();
        }
    }
    public void CreateNewCharacter(newCharacInfo _newCharacInfo)
    {
        StartCoroutine(CreateNewCharacterCoroutine(_newCharacInfo));
    }
    private IEnumerator CreateNewCharacterCoroutine(newCharacInfo _newCharacInfo)
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", _newCharacInfo.userId);
        // form.AddField("userId", _newCharacInfo.userId);
        form.AddField("nickname", _newCharacInfo.nickName);
        form.AddField("model", _newCharacInfo.modelName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:80/biffprj/CreateCharac/createcharac.php", form))
        {
            yield return www.SendWebRequest();
            if (CheckError(www))
            {
                Debug.Log(www.error);
                createCharacManager.CreateNewCharacSuccessFunction(false);
            }
            else if (www.result.ToString().Equals("Success"))
            {
                // database insert 
                Debug.Log("DB Connection Success");
                string data = www.downloadHandler.text;
                Debug.Log(data);
                createCharacManager.CreateNewCharacSuccessFunction(true);
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
