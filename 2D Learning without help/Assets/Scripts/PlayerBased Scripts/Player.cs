using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //References
    public Animator animator;
    public CharacterController controller;
    public Rigidbody2D RB2D;
    private GameObject TextTrigger;
    private GameObject player;
    private GameObject playerEvolved;
    public GameObject CameraController;

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
        playerEvolved.GetComponent<SpriteRenderer>().enabled = false;
        playerEvolved.GetComponent<BoxCollider2D>().enabled = false;
        playerEvolved.GetComponent<CircleCollider2D>().enabled=false;
        playerEvolved.GetComponent<Rigidbody2D>().gravityScale = 0;
        CameraController.GetComponent<CameraController>().player = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanMove)
        {
            RB2D.velocity = Vector2.zero;
            return;
        }
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
            else if (Input.GetButtonUp("Jump"))
            {
                jump = false;
                animator.SetBool("IsJumping", false);
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
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.name == "Text trigger")
        {
            CanMove = false;
            animator.SetBool("canMove", false);
            FindObjectOfType<DialougeTrigger>().TriggerDialogue();
            StartCoroutine("KillSwitch");
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
    }
    public void OnCrouching(bool isCrouching)
    {

    }
    private void FixedUpdate()
    {
        CanMove = true;
        jump = false;
        crouch=false;
        animator.SetBool("canMove", true);
    }
}
