using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Biff.BackgroundInfo;
using UnityEngine.Networking;

public class NowWearingDB : MonoBehaviour
{
    public void UpdateNowWearingInfo(string _nickName)
    {
        StartCoroutine(UpdateNowWearingInfoCoroutine(_nickName));
    }
    private IEnumerator UpdateNowWearingInfoCoroutine(string _nickName)
    {
        WWWForm form = new WWWForm();

        // static 값 들어감
        form.AddField("nickname", PlayerInfoManager.GetNickname());
        form.AddField("Clothes", PlayerInfoManager.GetNW_Clothes());
        form.AddField("Hands", PlayerInfoManager.GetNW_Hands());
        form.AddField("Head", PlayerInfoManager.GetNW_Head());
        form.AddField("Bag", PlayerInfoManager.GetNW_Bag());
        form.AddField("Pet", PlayerInfoManager.GetNW_Pet());
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:80/biffprj/MyCharacs/updatemynowwearing.php", form))
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
            }
            www.Dispose();
        }
        yield return null;
    }
} // end of class
