using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeleteMyCharacterManager : MonoBehaviour
{
    // # delete buttons
    [SerializeField]
    private Button[] btnDeletes = null;

    // # °¡Á®¿Ã nicknameField
    [SerializeField]
    private TextMeshProUGUI[] nicknameTMPs = null;

    // # askUI
    [SerializeField]
    private GameObject askUI = null;

    [SerializeField]
    private Button btnDeleteYes = null;
    [SerializeField]
    private Button btnDeleteNo = null;

    [SerializeField]
    private TextMeshProUGUI askUINick = null;

    [SerializeField]
    private MyCharacDB db = null;

    private void Awake()
    {
        askUI.SetActive(false);
        //Debug.Log(btnDeletes.Length);
        //Debug.Log(nicknameTMPs.Length);

        btnDeleteYes.onClick.AddListener(OnClickDeleteYes);
        btnDeleteNo.onClick.AddListener(OnClickDeleteNo);


        btnDeletes[0].onClick.AddListener(() => OnClickBtnDelete0());
        btnDeletes[1].onClick.AddListener(() => OnClickBtnDelete1());
        btnDeletes[2].onClick.AddListener(() => OnClickBtnDelete2());

    }
    private void Update()
    {
        for (int i = 0; i < nicknameTMPs.Length; i++)
        {
            if (nicknameTMPs[i].text == "")
            {
                btnDeletes[i].gameObject.SetActive(false);
            }
            else
            {
                btnDeletes[i].gameObject.SetActive(true);
            }

        }
    }

    private void OnClickBtnDelete0()
    {

        askUINick.text = nicknameTMPs[0].text;
        askUI.SetActive(true);
    }
    private void OnClickBtnDelete1()
    {

        askUINick.text = nicknameTMPs[1].text;
        askUI.SetActive(true);
    }
    private void OnClickBtnDelete2()
    {
        askUINick.text = nicknameTMPs[2].text;
        askUI.SetActive(true);
    }

    private void OnClickDeleteYes()
    {
        string deleteCharacNick = askUINick.text;
        db.DeleteMyCharacter(deleteCharacNick);
    }
    private void OnClickDeleteNo()
    {
        askUI.SetActive(false);
    }
} // end of class
