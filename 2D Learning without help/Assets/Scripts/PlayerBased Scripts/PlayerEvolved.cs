﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerEvolved : MonoBehaviour
{
    //References
    public Animator animator;
    public CharacterController2D controller;
    public Rigidbody2D rb2D;
    private GameObject playerEvolved;

    //Floats
    public float Speed = 20f;
    float HorizontalMovement = 0f;

    //Bools
    bool CanMove;
    bool jump;
    bool crouch;
    bool isCrouching;
    bool jumpAtk;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CanMove = true;
        animator.SetBool("canMove", true);
        jumpAtk = false;
        playerEvolved = GameObject.Find("PlayerEvolved");
    }

    // Update is called once per frame
    void Update()
    {
            animator.SetBool("canMove", true);
            HorizontalMovement = Input.GetAxisRaw("Horizontal") * Speed;
            animator.SetFloat("Speed", Mathf.Abs(HorizontalMovement));

            if(Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping",true);
            }
            else if(Input.GetButtonUp("Jump"))
            {
                animator.SetBool("IsJumping", false);
            }
            if(Input.GetButtonDown("Crouch"))
            {
                crouch = true;
                animator.SetBool("Crouching", true);
            }
            else if(Input.GetButtonUp("Crouch"))
            {
                crouch = false;
                animator.SetBool("Crouching", false);
            }
            if(Input.GetButtonDown("Attack"))
            {
                animator.SetTrigger("Attack");
            }
            if(Input.GetButtonDown("JumpAtk"))
            {
                jump = true;
                jumpAtk = true;
                animator.SetTrigger("JumpAtk");
            }
            if(Input.GetButtonDown("Strike"))
            {
                animator.SetTrigger("StrikeAtk");
            }

    }
    public void OnLanding()
    {

    }

    public void OnCrouching()
    {

    }

    private void FixedUpdate()
    {
        controller.Move(HorizontalMovement * Time.deltaTime, crouch, jump);
        jump = false;
        jumpAtk = false;
        crouch = false;
        CanMove = true;
        animator.SetBool("canMove", true);
    }



}
