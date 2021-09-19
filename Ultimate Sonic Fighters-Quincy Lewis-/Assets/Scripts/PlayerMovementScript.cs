using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    //Movement Variables
    public float maxSpeed;
    public float jumpForce;
    public float movement;
    Rigidbody2D myRB;
    public Animator myAnimate;
    public bool facingRight = true;

    public bool IsGrounded;
    public Transform Ground;
    public LayerMask WhatIsGround;
    private new BoxCollider2D collider;

    public bool HitTaken;
    public bool FacePlayer2;
    public bool Alive;
    
    Vector2 Direction;



    public KeyCode AttackKey;

    public int PlayerNumber;
    float move;

    


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();

        //myAnimate = GetComponent<Animator>();

        
        Alive = true;

       

    }

    private void Update()
    {
        if(facingRight)
        {
            Direction = Vector2.left;
        }
        else
        {
            Direction = Vector2.right;
        }

        if (PlayerNumber == 1)
        {
            //When the player pushes a button (a,d arrow keys)
            move = Input.GetAxisRaw("P1HorizontalInput");
            //When the player inputs the jump button, character will jump
            if (Input.GetButtonDown("P1JumpInput") && IsGrounded)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);
                myAnimate.SetTrigger("Jump");
            }

        }

        /*if (Input.GetKeyDown(KeyCode.F))
        {
            myAnimate.SetTrigger("Jab1");
            float ForceX = 0;
            float ForceY = 0;
            myRB.velocity = new Vector2(ForceX, ForceY);
            
            //collider = ;

        }*/

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        RaycastHit2D ray1 = Physics2D.Raycast((Vector2)transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        if (ray1)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }

        

        if (move < 0) GetComponent<Rigidbody2D>().velocity = new Vector3(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        if (move > 0) GetComponent<Rigidbody2D>().velocity = new Vector3(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        //Create a 2d and Manipulate the x value
        myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y);

        myAnimate.SetFloat("Speed", movement);
        myAnimate.SetFloat("Height", 1);


        /*if(Input.GetKeyDown(KeyCode.F))
        {
            myAnimate.SetTrigger("Jab1");
            float ForceX = 0;
            float ForceY = 0;
            myRB.velocity = new Vector2(ForceX, ForceY);

            //collider = ;

        }*/




        //To verify the animator whether character is on the ground or not
        myAnimate.SetBool("IsGrounded", IsGrounded);

        //This conditions where player 1 flips based on which side player2 is on.
        RaycastHit2D rayplayer = Physics2D.Raycast((Vector2)transform.position, Direction, 10, LayerMask.GetMask("Player2"));
        if (rayplayer)
        {
            Debug.Log(rayplayer.transform.name);
            /*if (move < 0 == facingRight)
            {
                flip();
            }
            else
            {
                if (move > 0 == !facingRight)
                {
                    flip();
                }
            }*/
        }
        else
        {
            if(IsGrounded)
            {
                flip();
            }
        }
        

        /* if (move < 0 && !facingRight)
         {
             flip();
         }
         else if(move > 0 && facingRight)
         {
             flip();
         }*/
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180);
    }

   

}
