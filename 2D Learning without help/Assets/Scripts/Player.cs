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
    private GameObject playerEvolved;

    //Floats
    public float Speed = 20f;
    float HorizontalMovement = 0f;

    //Bools
    public bool CanMove;
    bool jump;
    bool crouch;
    bool isCrouching;
    bool jumpAtk;

    //GameObjcts
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CanMove = true;
        jumpAtk = false;
        animator.SetBool("canMove", true);
        TextTrigger = GameObject.Find("Text trigger");
        player = GameObject.Find("Player");
        playerEvolved = GameObject.Find("PlayerEvolved");
        playerEvolved.GetComponent<SpriteRenderer>().enabled = false;
        playerEvolved.GetComponent<BoxCollider2D>().enabled = false;
        playerEvolved.GetComponent<CircleCollider2D>().enabled=false;
        playerEvolved.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove == true)
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
                animator.SetTrigger("IsAttacking");
            }
            if (Input.GetButtonDown("JumpAtk"))
            {
                jump = true;
                jumpAtk = true;
                animator.SetTrigger("IsJumpAttacking");
            }
        }
        else if (CanMove==false)
        {
            controller.Move(0, false,false);
            jump = false;
            jumpAtk=false;
            crouch = false;
            HorizontalMovement = 0f;
            animator.SetFloat("Speed", 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.name == "Text trigger")
        {
            CanMove = false;
            animator.SetBool("canMove", false);
            FindObjectOfType<DialougeTrigger>().TriggerDialogue();
            StartCoroutine("KillSwitch");
            while(animator.GetBool("IsOpen")==true)
            {
                CanMove = false;
                animator.SetBool("canMove", false);
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
            Destroy(GameObject.Find("Sword"));
            Destroy(GameObject.Find("Player Cam"));
            Destroy(GameObject.Find("Player"));
            playerEvolved.GetComponent<SpriteRenderer>().enabled=true;
            playerEvolved.GetComponent<BoxCollider2D>().enabled = true;
            playerEvolved.GetComponent<CircleCollider2D>().enabled=true;
            playerEvolved.GetComponent<Rigidbody2D>().gravityScale = 3;
            playerEvolved.transform.position = player.transform.position;
            player.SetActive(false);
            Debug.Log("Player has been transformed");
        }
    }

    private void FixedUpdate()
    {
        CanMove = true;
        controller.Move(HorizontalMovement * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        crouch=false;
        animator.SetBool("canMove", true);
    }
}
