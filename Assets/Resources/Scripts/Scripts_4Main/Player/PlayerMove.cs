using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun
{
    public enum eMoveDir { Forward, Backward, Left, Right }
    private Animator anim_;
    private Vector3 moveDir_ = Vector3.zero;
    private CharacterController characterController_;
    private readonly float moveSpeedOrigin_ = 0.1f;
    private float moveSpeed_ = 0.1f;

    private readonly float gravity_ = -9.81f;
    private readonly float jumpPower_ = 2f;

    public bool isGround_ { get; set; }
    private bool isWalking_ = false;
    public bool isJumpStart_ { get; set; }
    private bool isRun_ = false;

    private void Start()
    {
        if (!photonView.IsMine)
            return;
        characterController_ = GetComponentInParent<CharacterController>();
        anim_ = GetComponentInChildren<Animator>();
        isGround_ = true;
        isJumpStart_ = false;
    }
    private Transform GetTr()
    {
        return transform;
    }
    private void Update()
    {
        if (!photonView.IsMine)
            return;
        //   if (!photonView.IsMine) return;
        MovingProcess();
    }
    private void StopJump()
    {
        moveDir_ = Vector3.zero;
        moveDir_.y = gravity_;
        isJumpStart_ = false;
        characterController_.Move(moveDir_ * moveSpeed_);
        anim_.SetBool("isJump", false);
    }
    private void StopForwardJump()
    {
        isWalking_ = false;
        isJumpStart_ = false;
        moveDir_ = transform.forward;
        moveDir_.y = gravity_;
        characterController_.Move(moveDir_ * moveSpeed_);
        anim_.SetBool("isForwardJump", false);
    }
    private void StopPosAnim()
    {
            anim_.SetBool("isPose", false);
    }
    private void MovingProcess()
    {
        anim_.SetBool("isWalk", false);
        anim_.SetBool("isRun", false); 

        if (Input.GetKeyDown(KeyCode.P))
        {
            anim_.SetBool("isPose", true);
            Invoke("StopPosAnim", 1f);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround_ == true)
            {
                isJumpStart_ = true;
            }
        }
        if (isJumpStart_ && !isWalking_)
        {
            anim_.SetBool("isJump", true);
            moveDir_ = Vector3.zero;
            moveDir_.y = jumpPower_;
            characterController_.Move(moveDir_ * moveSpeed_);
            Invoke("StopJump", 0.2f);

        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRun_ = true;
            float runSpeed = moveSpeedOrigin_ * 2f;
            moveSpeed_ = runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRun_ = false;
            moveSpeed_ = moveSpeedOrigin_;
        }
        if (Input.GetKey(KeyCode.W))
        {
            Walk(eMoveDir.Forward);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Walk(eMoveDir.Backward);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Walk(eMoveDir.Left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
                Walk(eMoveDir.Right);
        }
    }

    private void Walk(eMoveDir _dir)
    {
        isWalking_ = true;
        moveDir_ = transform.forward;
        moveDir_.y = gravity_;
        if (isWalking_)
        {
            anim_.SetBool("isWalk", isWalking_);
        }
        if (isRun_)
        {
            anim_.SetBool("isRun", isRun_);
        }
        if (isJumpStart_ && isWalking_)
        {
            anim_.SetBool("isForwardJump", true);
            moveDir_ = transform.forward;
            moveDir_.y = jumpPower_ / 3;
            characterController_.Move(moveDir_ * moveSpeed_);
            Invoke("StopForwardJump", 0.2f);
        }
        if (_dir == eMoveDir.Forward)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (_dir == eMoveDir.Backward)
        {
            transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        }
        else if (_dir == eMoveDir.Left)
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
        else if (_dir == eMoveDir.Right)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        characterController_.Move(moveDir_ * moveSpeed_);
        isWalking_ = false;
    }
} // end of class

