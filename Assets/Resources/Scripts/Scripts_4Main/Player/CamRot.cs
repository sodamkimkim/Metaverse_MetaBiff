using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CamRot : MonoBehaviourPun
{
    [SerializeField]
    private ScreenSideManager screenSideManager_ = null;
    [SerializeField]
    private GameObject smallMapCam_ = null;
    [SerializeField]
    private GameObject ps_InMap_ = null;

    private Camera cam_ = null;
    private readonly Vector3 camOriginLocalPos_ = new Vector3(0f, 7.7f, -7.7f);
    private readonly Quaternion camOrginLocalrotQt_ = Quaternion.Euler(42f, 0f, 0f);
    private void Start()
    {
        if (!photonView.IsMine)
            return;
        cam_ = Camera.main;
        cam_.transform.parent = this.gameObject.transform;
        cam_.transform.localPosition = camOriginLocalPos_;
        cam_.transform.localRotation = camOrginLocalrotQt_;

        smallMapCam_.SetActive(true);
        ps_InMap_.SetActive(true);
    }
    private void Update()
    {
        if (!photonView.IsMine)
            return;
        ConstraintCamRotate();
        CamRotation();
    }
    
    public Transform GetTransform()
    {
        return transform;
    }
    public void SetInitialValues(ScreenSideManager _screemsideManager)
    {
        screenSideManager_ = _screemsideManager;
    }
    private void ConstraintCamRotate()
    {
        // À§¾Æ·¡
        if (cam_.transform.localRotation.x < 0f)
        {
            Quaternion q = cam_.transform.localRotation;
            q.x = 0f;
            cam_.transform.localRotation = q;
        }
        // ¾ç¿·
        if (cam_.transform.localRotation.y < -0.5f)
        {
            // µü ¿·º¸±â´Â -0.70 0.70
            // µÚ Á¤·Î º¸±â´Â -0.95, 0.955
            Quaternion q = cam_.transform.localRotation;
            q.y = -0.5f;
            cam_.transform.localRotation = q;
        }
        if (cam_.transform.localRotation.y > 0.5f)
        {
            Quaternion q = cam_.transform.localRotation;
            q.y = 0.5f;
            cam_.transform.localRotation = q;
        }
    }
    private void CamRotation()
    {
        float angleOffset = 0.001f;
        if (screenSideManager_.isScreenSideTop) 
        {
            Quaternion qt = cam_.transform.localRotation;
            qt.y = 0f;
            qt.x -= angleOffset;
            cam_.transform.localRotation = qt;
        }
        if (screenSideManager_.isScreenSideLeft && !screenSideManager_.isScreenSideRight)
        {
            Quaternion qt = cam_.transform.localRotation;
            qt.x = 0f;
            qt.y -= angleOffset;
            cam_.transform.localRotation = qt;
        }

        if (screenSideManager_.isScreenSideRight && !screenSideManager_.isScreenSideLeft)
        {
            Quaternion qt = cam_.transform.localRotation;
            qt.x = 0f;
            qt.y += angleOffset;
            cam_.transform.localRotation = qt;
        }
        if (!screenSideManager_.isScreenSideTop && !screenSideManager_.isScreenSideBottom && !screenSideManager_.isScreenSideLeft && !screenSideManager_.isScreenSideRight)
        {
            cam_.transform.localRotation = camOrginLocalrotQt_;
        }
    }
} // end of class
