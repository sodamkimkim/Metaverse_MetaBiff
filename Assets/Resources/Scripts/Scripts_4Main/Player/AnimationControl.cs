using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    private Animator anim = null;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        MovingProcess();

    }
    private void MovingProcess()
    {
        anim.SetBool("isWalk", false);
        anim.SetBool("isPose", false);

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isWalk", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isWalk", true);
        }
        if (Input.GetKey(KeyCode.P))
        {
            anim.SetBool("isPose", true);
        }
    }
}