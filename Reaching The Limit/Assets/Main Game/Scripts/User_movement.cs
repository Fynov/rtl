using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class User_movement : MonoBehaviour {

    public float maxSpeed = 30f;
    bool facingRight = true;
    public Text attemptText;
    public Text scoreText;
    public Text speedText;
    public Text forceText;
    public Text AJText;
    public Text DeathText;

    //Skakanje
    bool grounded = false;
    public Transform groundcheck;
    float groundRadious = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 700f;
    int doubleJump = 0;
    Animator anim;
    int amountOfAdditionalJumps = 0;
    // Use this for initialization

    int dead = 0;
    public int attempt;
    public int score;

    void Start () {
        attempt = 1;
        score = 0;
        IzpisiText();
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (dead == 0)
        {
            grounded = Physics2D.OverlapCircle(groundcheck.position, groundRadious, whatIsGround);
            anim.SetBool("Ground", grounded);
            anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);



            float move = Input.GetAxis("Horizontal");

            anim.SetFloat("Speed", Mathf.Abs(move));

            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (move > 0 && !facingRight)
            {
                flip();
            }
            else if (move < 0 && facingRight)
            {
                flip();
            }
        }
	}

    void Update()
    {
        test_setting();
        if (dead == 0)
        {
            //Skakanje
            if (grounded && Input.GetKeyDown(KeyCode.UpArrow))
            {
                anim.SetBool("Ground", false);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            }
            if (!grounded && Input.GetKeyDown(KeyCode.UpArrow) && doubleJump < amountOfAdditionalJumps)
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
        if (dead == 1)
        {
            //Vector3 theScale = transform.localScale;
            //theScale.y *= -1;
            //transform.localScale = theScale;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            DeathText.text = "Try Again\n Press Space To Respawn";

            
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

    //spike hit
    void OnCollisionEnter2D(Collision2D other)
        {
        if (other.gameObject.tag == "Spike")
        {               // < ---Write whatever you want.make sure that you object that you collide with has the same tag.
            dead++;
            attempt++;
            IzpisiText();
            // GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Out Of Bounds")
        {               // < ---Write whatever you want.make sure that you object that you collide with has the same tag.
            dead++;
            attempt++;
            IzpisiText();
            
        }
    }


    //void OnCollisionStay(Collision collisionInfo)
    //{
    //    print(gameObject.name + " and " + collisionInfo.collider.name + " are still colliding");
    //}

    //void OnCollisionExit(Collision collisionInfo)
    //{
    //    print(gameObject.name + " and " + collisionInfo.collider.name + " are no longer colliding");
    //}
    void test_setting()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space))
        {
            float lvlJumpForce = jumpForce;
            float lvlMaxSpeed = maxSpeed;
            Application.LoadLevel(Application.loadedLevelName);
            jumpForce = lvlJumpForce;
            maxSpeed = lvlMaxSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            maxSpeed = maxSpeed + 10f;
            IzpisiText();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            maxSpeed = maxSpeed - 10f;
            IzpisiText();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            jumpForce = jumpForce + 200f;
            IzpisiText();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            jumpForce = jumpForce - 200f;
            IzpisiText();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            amountOfAdditionalJumps++;
            IzpisiText();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if(amountOfAdditionalJumps != 0)
            amountOfAdditionalJumps--;
            IzpisiText();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Application.LoadLevel(0);
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
			if (AudioListener.volume == 0) {
				AudioListener.volume = 0.5f;

			} else {
				AudioListener.volume = 0f;
			}
		}
    }

    void IzpisiText()
    {
        attemptText.text = "Attempt: " + attempt.ToString();
        scoreText.text = "Score: " + score.ToString();
        speedText.text = "Speed: " + maxSpeed.ToString();
        forceText.text = "Force: " + jumpForce.ToString();
        AJText.text = "AJ:" + amountOfAdditionalJumps.ToString();
    }
}
