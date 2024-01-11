using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerChat : MonoBehaviourPun
{
    [SerializeField]
    private TMP_InputField chatField_ = null;
    [SerializeField]
    private Button btnSend_ = null;
    [SerializeField]
    private GameObject speechBubble_ = null;
    [SerializeField]
    private TextMeshProUGUI bubbleTxtTMP_ = null;
    private string bubbleTxt = "";

    private void Start()
    {
        if (!photonView.IsMine)
            return;
        btnSend_.onClick.AddListener(OnClickBtnSend);
        speechBubble_.SetActive(false);
    }
    public void SetInitialValues(TMP_InputField _chatField, Button _btnSend, GameObject _speechBubble, TextMeshProUGUI _bubbleTxtTMP)
    {
        chatField_ = _chatField;
        btnSend_ = _btnSend;
        speechBubble_ = _speechBubble;
        bubbleTxtTMP_ = _bubbleTxtTMP;
    }
    private void Update()
    {
        if (!photonView.IsMine)
            return;
        SetChatBubblePos();

    }
    public void OnClickBtnSend()
    {
        if (chatField_.text != "")
        {
            bubbleTxt = chatField_.text;
            UpdateChatBubbleUI();
        }
        else
        {
            return;
        }
        chatField_.text = "";
        speechBubble_.SetActive(true);
        Invoke("CloseChatBubble", 7);

    }
    private void SetChatBubblePos()
    {
        //Vector3 characterScreenPos = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        //characterScreenPos.x += 940f;
        //characterScreenPos.y += 350f;
        // buubleGoRtr.transform.position = characterScreenPos;
        Vector3 characPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        Vector3 newPos = characPos;
        newPos.y = characPos.y + 150f;

        Vector3 camPos = Camera.main.transform.position;
        camPos.y = 0f;
        camPos.x = 0f;
        speechBubble_.transform.position = newPos;
        speechBubble_.transform.LookAt(camPos);
        Vector3 euler = new Vector3(0f, 90f, -45f);
        speechBubble_.transform.Rotate(euler);
        Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);
        speechBubble_.transform.localScale = scale;
    }
    private void UpdateChatBubbleUI()
    { // update chatBubble
        bubbleTxtTMP_.text = bubbleTxt;
    }
    private void CloseChatBubble()
    {
        speechBubble_.SetActive(false);
    }

} // end of class
