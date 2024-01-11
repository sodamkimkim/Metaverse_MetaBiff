using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoving : MonoBehaviour
{
    public enum ECharacterModelName
    {
        M_HaJeongWoo,
        F_KimHyeSoo,
        M_MaDongSuck,
        M_JangChen,
        F_Navi,
        M_Ironman,
        Len
    }
    private Camera mainCam = null;
    private Vector3 initialPos = Vector3.zero;

    [SerializeField]
    private Button btnLeftArrow = null;
    [SerializeField]
    private Button btnRightArrow = null;

    [SerializeField]
    private GameObject characters = null;
    private CharacterPosition[] characterArr = null;
    private GameObject targetGo = null;
    private int idx = 2;

    private bool isMoving = false;
    private float movingSpeed = 10f;
    public static string nowModelName = "";


    private void Awake()
    {

        btnLeftArrow.onClick.AddListener(OnClickLeftArrow);
        btnRightArrow.onClick.AddListener(OnClickRightArrow);

        characterArr = characters.GetComponentsInChildren<CharacterPosition>();
        SetCamInitial();
    }
    private void Update()
    {

    }

    private void SetCamInitial()
    {
        mainCam = Camera.main;
        initialPos = mainCam.transform.position;

        Vector3 newPos = initialPos;
        newPos.x = characterArr[idx].gameObject.transform.position.x;
        nowModelName = characterArr[idx].gameObject.name;
        Debug.Log(GetNowModelName());
        initialPos = newPos;
        mainCam.transform.position = newPos;
    }
    private void OnClickLeftArrow()
    {
        int beforeIdx = idx;

        idx--;
        if (idx < 0)
        {
            idx = characterArr.Length - 1;
        }
        nowModelName = characterArr[idx].gameObject.name;
        Debug.Log(GetNowModelName());
        StopAllCoroutines();
        StartCoroutine(MoveCamCoroutine(beforeIdx, idx));

    }

    public string GetNowModelName()
    {
        return nowModelName;
    }
    private void OnClickRightArrow()
    {
        int beforeIdx = idx;

        idx++;
        if (idx > characterArr.Length - 1)
        {
            idx = 0;
        }
        nowModelName = characterArr[idx].gameObject.name;
        Debug.Log(GetNowModelName());
        StopAllCoroutines();
        StartCoroutine(MoveCamCoroutine(beforeIdx, idx));

    }
    private IEnumerator MoveCamCoroutine(int _beforeIdx, int _idx)
    {
        isMoving = true;
        Vector3 newPos = initialPos;
        float beforeX = characterArr[_beforeIdx].gameObject.transform.position.x;
        float destX = characterArr[idx].gameObject.transform.position.x;
        float t = 0f;

        while (t < 1f)
        {

            newPos.x = Mathf.Lerp(beforeX, destX, t);
            t += Time.deltaTime * movingSpeed;
            mainCam.transform.position = newPos;
            yield return null;
        }
        isMoving = false;
    }

} // end of class
