using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerAnimController : MonoBehaviour {

    Controller2D controller;
    Animator animator;
  

    void Awake ()
    {
        controller = GetComponent<Controller2D>();
        animator = GetComponent<Animator>();
    }


    public void RunningAction(float movementX)
    {
        if (controller.collisions.below)
        {
            animator.SetFloat("velocityX", movementX);
        }

    }


    public void Jump(bool grounded)
    { 
        animator.SetBool("grounded", grounded);
    }

}
