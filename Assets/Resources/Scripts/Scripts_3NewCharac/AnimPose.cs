using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPose : MonoBehaviour
{
  private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();   
    }
    private void Update()
    {
        anim.SetBool("isPose", false);
        if (CameraMoving.nowModelName == this.gameObject.name)
        {
            anim.SetBool("isPose", true);
        }
    }
}
