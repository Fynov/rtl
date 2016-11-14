using UnityEngine;
using System.Collections;

public class User_movement : MonoBehaviour {

    public float maxSpeed = 30f;
    bool facingRight = true;

    //Skakanje
    bool grounded = false;
    public Transform groundcheck;
    float groundRadious = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 700f;
    int doubleJump = 0;
    Animator anim;
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        grounded = Physics2D.OverlapCircle(groundcheck.position, groundRadious, whatIsGround);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);



        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));

        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if(move > 0 && !facingRight)
        {
            flip();
        }
        else if( move < 0 && facingRight)
        {
            flip();
        }
	}

    void Update()
    {
        if (grounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetBool("Ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
        if (!grounded && Input.GetKeyDown(KeyCode.UpArrow) && doubleJump < 4)
        {
            anim.SetBool("Ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            doubleJump++;
        }
        if (grounded)
        {
            doubleJump = 0;
        }
    }

   /* void doubleJump()
    {
        if (dJump)
        {
            int doubleJump = 0;
            if (!grounded && Input.GetKeyDown(KeyCode.UpArrow) && doubleJump < 5)
            {
                anim.SetBool("Ground", false);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
                doubleJump++;
            }
            if (grounded)
            {
                doubleJump = 0;
            }
        }
    }*/

    //Obracanje charactarja
    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
