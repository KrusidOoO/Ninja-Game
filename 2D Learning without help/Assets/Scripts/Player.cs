using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //References
    public Animator animator;
    public CharacterController2D controller;
    public Rigidbody2D RB2D;
    private GameObject TextTrigger;
    private GameObject player;

    //Floats
    public float Speed = 20f;
    float HorizontalMovement = 0f;
    float VerticalMovement = 0f;

    //Bools
    public bool CanMove;
    bool jump;
    bool crouch;
    bool jumpAtk;
    bool Atk;
    bool isCrouching;

    //GameObjcts
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CanMove = true;
        animator.SetBool("canMove", true);
        TextTrigger = GameObject.Find("Text trigger");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            HorizontalMovement = Input.GetAxisRaw("Horizontal") * Speed;
            animator.SetFloat("Speed", Mathf.Abs(HorizontalMovement));
            animator.SetBool("canMove", true);

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
                animator.SetBool("IsCrouching", true);
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
                animator.SetBool("IsCrouching", false);
            }
            if (Input.GetButtonDown("Attack"))
            {
                Atk = true;
                animator.SetBool("IsAttacking", true);
            }
            else if (Input.GetButtonUp("Attack"))
            {
                animator.SetBool("IsAttacking", false);
            }
            if (Input.GetButtonDown("JumpAtk"))
            {
                jump = true;
                jumpAtk = true;
                animator.SetBool("IsJumpAtk", true);
            }
        }
        else if (!CanMove)
        {
            animator.SetBool("canMove", false);
            controller.Move(0, false,false);
            jump = false;
            jumpAtk = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.name == "Text trigger")
        {
            FindObjectOfType<DialougeTrigger>().TriggerDialogue();
            StartCoroutine("KillSwitch");
            while(animator.GetBool("IsOpen")==true)
            {
                CanMove = false;
            }
        }
    }

    IEnumerator KillSwitch()
    {
        yield return new WaitForSeconds(0);
        Destroy(TextTrigger);
    }


    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsJumpAtk", false);
    }
    public void OnCrouching(bool isCrouching)
    {

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Sword")
        {
            col.gameObject.SetActive(false);
            player.transform.Find("PlayerEvolved");
        }
    }

    private void FixedUpdate()
    {
        controller.Move(HorizontalMovement * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        jumpAtk = false;
        Atk = false;
        CanMove = true;
    }
}
