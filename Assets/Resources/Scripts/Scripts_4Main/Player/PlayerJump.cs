using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerMove playerMove_ = null;
    private int jumpLayerMask_;
    private void Awake()
    {
        playerMove_ = GetComponentInChildren<PlayerMove>();
        jumpLayerMask_ = 1 << LayerMask.NameToLayer("Jumpable");
    }
    private void OnControllerColliderHit(ControllerColliderHit _collision)
    {
        if (_collision.gameObject.layer == jumpLayerMask_)
        {
            playerMove_.isGround_ = true;
            Debug.Log("isGround_" + playerMove_.isGround_);
        }
    }
} // end of class
