using UnityEngine;
using System.Collections;

public class boxcontrollerscript : MonoBehaviour {


    private bool facingRight = true;
    Animator anim;
    bool grounded = false;
    public int jump = 0;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask WhatIsGround;
    public float jumpForce = 70f;
    public float maxSpeed = 1f;
    float move;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, WhatIsGround);
        anim.SetBool ("ground", grounded);

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        if (grounded)
        {
            jump = 0;
        }

        move = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(move));
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }


    }

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump++;
            if (jump >= 2 && grounded)
            {
                jump = 0;
            }
            else if (jump < 2)
            {
                if (jump == 1)
                {
                    anim.SetBool("ground", false);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
                }
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce/2));
                }

            }
        }
       if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetBool("idle", true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) && move != 0)
        {
            anim.SetBool("idle", false);
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        anim.SetBool("facingRight", facingRight);
    }
}