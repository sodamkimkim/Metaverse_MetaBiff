using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Biff.BackgroundInfo;

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    private GameObject canvasLogin = null;

    [SerializeField]
    private TMP_InputField idField = null;
    [SerializeField]
    private TMP_InputField pwField = null;

    [SerializeField]
    private Button btnNewAccount = null;
    [SerializeField]
    private Button btnLogin = null;

    [SerializeField]
    private TextMeshProUGUI statusMsgTxt = null;

    [SerializeField]
    private DB db = null;

    [SerializeField]
    JoinManager joinManager = null;

    private static string userId { get; set; }

    public static LoginManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<LoginManager>();
            }
            return m_instance;
        }
    }
    private static LoginManager m_instance;
    
    public struct UserInputData
    {
        public string userID;
        public string userPW;

        public UserInputData(string _id, string _pw)
        {
            userID = _id;
            userPW = _pw;
        }
    } // end of struct
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            
        }
    
        // DontDestroyOnLoad(gameObject);

        btnNewAccount.onClick.AddListener(OnClickNewAccountBtn);
        btnLogin.onClick.AddListener(OnClickLoginBtn);
        // OpenCanvasLogin();
        
    }
    public void OpenCanvasLogin()
    {
        canvasLogin.SetActive(true);
    }
    public void IdFieldOnSelect()
    {
        idField.text = "";
    }
    public void pwFieldOnSelect()
    {
        pwField.text = "";
    }
    private void OnClickNewAccountBtn()
    {
        joinManager.OpenCanvasCreateAccount();

    }
    private void OnClickLoginBtn()
    {
        if(idField.text == "")
        {
            statusMsgTxt.text = "Please Fill Out IDField!!";
            statusMsgTxt.color = new Color(255, 0, 0);
        }
        else if (pwField.text == "")
        {
            statusMsgTxt.text = "Please Fill Out PWField!!";
            statusMsgTxt.color = new Color(255, 0, 0);
        }
        else
        {
            string id = idField.text;
            string pw = pwField.text;

            // 공백 제거
            id.Replace(" ", "");
            pw.Replace(" ", "");

            UserInputData inputData = new UserInputData(id, pw);

            // DB에 로그인 정보 검색
            db.SearchUserInfo(inputData);
        }
    }

    public void NoUserInfoMessage()
    {
        // Debug.Log("No Matching ID & PW");
        statusMsgTxt.text = "No Matching ID & PW";
    }
    public void LoginSuccess()
    {
        PlayerInfoManager.SetID(idField.text);
        PlayerInfoManager.SetPW(pwField.text);
        SceneManager.LoadScene("2_MyCharacters");
        // DontDestroyOnLoad(gameObject);
    }

} // end of class
