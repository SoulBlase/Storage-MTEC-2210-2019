using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpScript : MonoBehaviour
{
    public bool onGround = false;
    public bool jump = false;
    public float maxSpeedX = 3f;
    public float maxSpeedY = 3f;
    public LayerMask whatIsGround;
    public Transform groundChecker;

    private float groundRadius = 0.1f;
    private bool facingRight = true;
    public bool touchLeft = false;
    public bool touchRight = false;
    private BoxCollider2D boxcollider;

    public float velocityX;
    public float velocityY;
    private float EPSILON;

    public float WallSpeed;
    public Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float forceX = 0;
        float forceY = 0;
        velocityX = GetComponent<Rigidbody2D>().velocity.x;
        velocityY = GetComponent<Rigidbody2D>().velocity.y;
        //I test if my character is stuck to a wall
        checkWallPosition();

        onGround = Physics2D.OverlapCircle(groundChecker.position, groundRadius, whatIsGround);
        float moveH = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButtonDown("Jump");

        Anim.SetFloat("Wall", WallSpeed);

        //If I touch the ground
        if (onGround)
        {
            if (System.Math.Abs(moveH) > EPSILON)
            {
                forceX = maxSpeedX * moveH;
            }
            if (jump && !touchLeft && !touchRight)
            {
                forceY = maxSpeedY;
            }
            else if (jump && (touchLeft || touchRight))
            {
                if (touchLeft)
                {
                    forceX = maxSpeedX;
                }
                else
                {
                    forceX = -maxSpeedX;
                }
                forceY = maxSpeedY;
                Flip();
            }
            else
            {
                forceY = GetComponent<Rigidbody2D>().velocity.y;
            }
        }


        //If I do not touch the ground
        else
        {
            if (!jump && !touchLeft && !touchRight)
            {
                forceY = GetComponent<Rigidbody2D>().velocity.y;
                forceX = GetComponent<Rigidbody2D>().velocity.x;
            }
            else if (jump && (touchLeft || touchRight))
            {
                if (touchLeft)
                {
                    forceX = maxSpeedX;
                    if (!facingRight)
                        Flip();
                }
                else
                {
                    forceX = -maxSpeedX;
                    if (facingRight)
                        Flip();
                }
                forceY = maxSpeedY;
            }
            else if (touchLeft || touchRight)
            {
                if (System.Math.Abs(moveH) < EPSILON)
                {
                    forceX = 0;
                    forceY = GetComponent<Rigidbody2D>().velocity.y;
                }
                else
                {
                    if ((touchLeft && System.Math.Abs(moveH - -1) < EPSILON) || (touchRight && moveH == 1))
                    {
                        if (System.Math.Abs(moveH - -1) < EPSILON && facingRight)
                        {
                            Flip();
                        }
                        if (System.Math.Abs(moveH - 1) < EPSILON && !facingRight)
                        {
                            Flip();
                        }
                        forceY = GetComponent<Rigidbody2D>().velocity.y / 3;
                    }
                }
            }
        }

        //To verify the animator whether character is on the ground or not
        //Anim.SetBool("OnGround", onGround);

        //Here the forces are applied
        GetComponent<Rigidbody2D>().velocity = new Vector2(forceX, forceY);


        //Here the character is returned according to his position
        if (onGround)
        {
            if (moveH > 0 && !facingRight)
            {
                Flip();
            }
            else if (moveH < 0 && facingRight)
            {
                Flip();
            }
        }

    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void checkWallPosition()
    {
        touchLeft = touchRight = false;
        float yCircle = transform.localPosition.y + (boxcollider.offset.y * transform.localScale.y);
        Vector3 circlePosition = new Vector3(transform.localPosition.x, yCircle, transform.localPosition.z);
        bool hitRight = Physics2D.Raycast(circlePosition, transform.right, boxcollider.bounds.extents.x * transform.localScale.y + transform.localScale.y, 1 << LayerMask.NameToLayer("Walls"));
        bool hitLeft = Physics2D.Raycast(circlePosition, -transform.right, boxcollider.bounds.extents.x * transform.localScale.y + transform.localScale.y, 1 << LayerMask.NameToLayer("Walls"));
        if (hitRight)
        {
            touchRight = true;
        }
        if (hitLeft)
        {
            touchLeft = true;
        }
    }
}

