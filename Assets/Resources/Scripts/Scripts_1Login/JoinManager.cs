using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class JoinManager : MonoBehaviour
{
    // id 입력
    [SerializeField]
    private GameObject canvasCreateNewAccout = null;
    [SerializeField]
    private TMP_InputField idField = null;

    // id 중복체크
    [SerializeField]
    private Button btnIdAvailabilityCheck = null;
    [SerializeField]
    private TextMeshProUGUI idStatusMsgTxt = null;

    //Pw 입력
    [SerializeField]
    private TMP_InputField pwField = null;

    // result message
    [SerializeField]
    private TextMeshProUGUI resultMsgTxt = null;

    // 회원가입 하기 버튼
    [SerializeField]
    private Button btnCreateNewAccount = null;

    // 로그인 창으로 돌아가기
    [SerializeField]
    private Button btnGotnLogin = null;

    [SerializeField]
    private DB db = null;

    private bool idAvailability = false;

    public struct UserJoinData
    {
        public string userId { get; set; }
        public string userPw { get; set; }
        public UserJoinData(string _id, string _pw)
        {
            userId = _id;
            userPw = _pw;
        } // end of constructor
    }// end of struct UserJoinData


    private void Awake()
    {

        idAvailability = false;
        // canvasCreateNewAccout.SetActive(false);
        btnIdAvailabilityCheck.onClick.AddListener(OnClickCheckIdAvailability);
        btnCreateNewAccount.onClick.AddListener(OnClickCreateNewAccBtn);
        btnGotnLogin.onClick.AddListener(OpenCanvasLogin);
    }


    private void OnClickCheckIdAvailability()
    {
        string id;
        id = idField.text.Replace(" ", "");
        idField.text = id;
        // ID Availability 체크 idAvailability플래그 변수 값 바꿔줌
        idStatusMsgTxt.text = "...Checking...";

        if (idField.text.Equals(""))
        {
            idStatusMsgTxt.text = "Please Enter ID!";
            idStatusMsgTxt.color = new Color(255f, 0f, 0f);
        }
        else
        {
            idStatusMsgTxt.color = new Color(0f, 255f, 0f);
            // db에서 id조회
            db.SearchUserID(id);
        }
    }
    public bool CheckIDAvailability(bool _idAvailability)
    {
        // 아이디 중복체크 버튼 콜백함수 -> db값을 통해서 리턴 값이 바뀜
        return idAvailability = _idAvailability;
    }
    public void IDIsAvailableFunction(bool _idAvailability)
    {
        idAvailability = _idAvailability;
        if (idAvailability)
        {
            // ID is available 일 때의 process
            idStatusMsgTxt.text = "Your ID is available !";
            // 초록색
            idStatusMsgTxt.color = new Color(0f, 255f, 0f);

        }
        else if (idAvailability == false)
        {
            // ID is not available 일 때의 process
            idStatusMsgTxt.text = "The ID is already exit.";
            idStatusMsgTxt.color = new Color(255f, 0f, 0f);
        }
    }

    public void OpenCanvasCreateAccount()
    {
        canvasCreateNewAccout.SetActive(true);
    }
    public void OpenCanvasLogin()
    {
        canvasCreateNewAccout.SetActive(false);
        LoginManager.instance.OpenCanvasLogin();
    }
    public void OnClickCreateNewAccBtn()
    {
        // id중복체크 해줘야함
        // CreateNewAccount Callback함수
        string id = idField.text;
        string pw = pwField.text;
        UserJoinData userData = new UserJoinData(id, pw);
        if (idAvailability)
        {
            resultMsgTxt.text = "...Creating Your Account...";
            resultMsgTxt.color = new Color(0f, 255f, 0f);
            db.CreateNewAccount(userData);
            resultMsgTxt.text = "Done!";
            resultMsgTxt.color = new Color(0f, 255f, 0f);
        }
        else if(idAvailability==false)
        {
            resultMsgTxt.text = "Please Check ID!!";
            idStatusMsgTxt.color = new Color(255f, 0f, 0f);
        }
    }
    public void IdFieldOnSelect()
    {
        idAvailability = false;
        idField.text = "";
    }
    public void pwFieldOnSelect()
    {
        pwField.text = "";
    }




} // end of class
