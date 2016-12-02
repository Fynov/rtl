using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class User_movement : MonoBehaviour {

    public float maxSpeed = 30f;
    public bool facingRight = true;
    public bool hell;
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
    public float jumpForce = 2650f;
    int doubleJump = 0;
    Animator anim;
    private Camera cam;
    Color default_colour = new Color(92f / 255f, 192f / 255f, 233f / 255f, 255f);
    Color hell_colour = new Color(79f/255f, 14f/255f, 14f/255f, 255f);
    SpriteRenderer render;
    int amountOfAdditionalJumps = 0;

    GameObject[] blokeci;
    GameObject[] rastline;
    GameObject[] drugo;

    int dead = 0;
    public int attempt;
    public int score;

    void Start () {
        hell = false;
        attempt = 1;
        score = 0;
        IzpisiText();
        anim = GetComponent<Animator>();
        cam = Camera.main;

    }
	
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
        check_if_hell();
        test_setting();
        blokeci = GameObject.FindGameObjectsWithTag("Blocks");
        rastline = GameObject.FindGameObjectsWithTag("Plants");
        drugo = GameObject.FindGameObjectsWithTag("Enviorment");
        In_game_settings();
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
        coin_pick_up(other);
        if (other.gameObject.tag == "Spike")
        {              
            dead++;
            attempt++;
            IzpisiText();
        }

        if (other.gameObject.tag == "Spodnji_oblaki")
        {
            dead++;
            attempt++;
            IzpisiText();
        }
    }


    void coin_pick_up(Collision2D other)
    {
        if(other.gameObject.tag == "Gold_coin")
        {
            score += 25;
            IzpisiText();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Silver_coin")
        {
            score += 100;
            IzpisiText();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Blue_coin")
        {
            score += 500;
            IzpisiText();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Red_coin")
        {
            score += 5000;
            GetComponent<SpriteRenderer>().color = Color.red;
            jumpForce = jumpForce - (jumpForce / 5);
            maxSpeed = maxSpeed - (maxSpeed / 5);
            cam.backgroundColor = hell_colour;
            IzpisiText();
            hell = true;

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Blocks"))
            {
                g.GetComponent<SpriteRenderer>().color = Color.red;
            }

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Plants"))
            {
                g.GetComponent<SpriteRenderer>().color = Color.red;
            }

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enviorment"))
            {
                g.GetComponent<SpriteRenderer>().color = Color.red;
            }

            Destroy(other.gameObject);
        }
    }

    void check_if_hell()
    {
        if(hell == true)
        {
            amountOfAdditionalJumps = 0;
        }
        if (maxSpeed >= 30f && jumpForce >= 2650f)
        {
            hell = false;
            cam.backgroundColor = default_colour;
            GetComponent<SpriteRenderer>().color = Color.white;
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Blocks"))
            {
                g.GetComponent<SpriteRenderer>().color = Color.white;
            }

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Plants"))
            {
                g.GetComponent<SpriteRenderer>().color = Color.white;
            }

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enviorment"))
            {
                g.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    void In_game_settings()
    {
        if(Input.GetKeyDown(KeyCode.O))//or when pause button.
        {
            //load half transparent scene on top of this one, with sound control and exit button.
            pause();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            //Freeze until unpaused
            pause();
        }
    }


    void test_setting()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space))
        {
            reset();
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

    void reset()
    {
        float lvlJumpForce = jumpForce;
        float lvlMaxSpeed = maxSpeed;
        Application.LoadLevel(Application.loadedLevelName);
        jumpForce = lvlJumpForce;
        maxSpeed = lvlMaxSpeed;
    }

    void pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
