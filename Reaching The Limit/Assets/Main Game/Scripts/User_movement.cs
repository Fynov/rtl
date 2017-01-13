using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class User_movement : MonoBehaviour {

    public float maxSpeed = 30f;
    public float jumpForce = 2650f;
    public bool facingRight = true;
    public float DoubleJumpingCoolDown = 10;
    public float DoubleJumpingCoolDownTimer;

    public bool enchanted;
    public bool hell;
    public Text attemptText;
    public Text scoreText;
    public Text speedText;
    public Text forceText;
    public Text AJText;
    public Text DeathText;
    public Animator anim;
    private Camera cam;
    public Color default_colour = new Color(92f / 255f, 192f / 255f, 233f / 255f, 255f);
    public Color hell_colour = new Color(79f / 255f, 14f / 255f, 14f / 255f, 255f);
    public GameObject sparks;
    public SpriteRenderer render;
    public AudioSource SoundJump;
    public AudioSource SoundCoin;
    public AudioSource SoundHell;
    public AudioSource SoundDiamond;
    public AudioSource SoundSpike;
    public AudioSource SoundHellWhispers;
    public AudioSource SoundHeartbeat;
    private bool showPopUp = false;

    int ingameo = 0;

    //Skakanje
    bool grounded = false;
    public Transform groundcheck;
    float groundRadious = 0.2f;
    public LayerMask whatIsGround;
    int doubleJump = 0;
    int amountOfAdditionalJumps = 0;

    public bool dead = false;
    public int attempt;
    public int score;

    void Start () {
        dead = false;
        enchanted = false;
        hell = false;
        attempt = 1;
        score = 0;
        IzpisiText();
        anim = GetComponent<Animator>();
        cam = Camera.main;
        anim.SetBool("Hell", false);
        anim.SetBool("Alive", true);
        //sparks = GameObject.Find("Sparks");

    }
	
	void FixedUpdate () {
        if (dead == false)
        {
            grounded = Physics2D.OverlapCircle(groundcheck.position, groundRadious, whatIsGround);
            anim.SetBool("Ground", grounded);
            anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);



            float move = Input.GetAxis("Horizontal");

            anim.SetFloat("Speed", Mathf.Abs(move));
            anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
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
        doubleJumpTimer();
        check_if_hell();
        test_setting();
        In_game_settings();
        if (dead == false)
        {
            //Skakanje
            if (grounded && Input.GetKeyDown(KeyCode.UpArrow))
            {
                anim.SetBool("Ground", false);
                SoundJump.Play();
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            }
            if (!grounded && Input.GetKeyDown(KeyCode.UpArrow) && doubleJump < amountOfAdditionalJumps)
            {
                anim.SetBool("Ground", false);
                SoundJump.Play();
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
                doubleJump++;
            }
            if (grounded)
            {
                doubleJump = 0;
            }
        }
        if (dead == true)
        {
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
    void OnTriggerEnter2D(Collider2D other)
    {
        coin_pick_up(other);

        if (other.gameObject.tag == "Explosion" && dead == false)
        {
            dead = true;
            anim.SetBool("Alive", false);
            anim.SetTrigger("Dying");
            attempt++;
            IzpisiText();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Spike" && dead == false)
        {
            dead = true;
            anim.SetBool("Alive", false);
            anim.SetTrigger("Dying");
            SoundSpike.Play();
            attempt++;
            IzpisiText();
        }

        if (other.gameObject.tag == "Spodnji_oblaki" && dead == false)
        {
            dead = true;
            anim.SetBool("Alive", false);
            anim.SetTrigger("Dying");
            attempt++;
            IzpisiText();
        }

        if(other.gameObject.name == "Finito")
        {
            showPopUp = true;
        }
    }


    void OnGUI()
    {
        if (showPopUp)
        {
            GUI.Window(0, new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 75
                   , 300, 250), ShowGUI, "CONGRATS!");

        }
    }

    void ShowGUI(int windowID)
    {

        GUI.Label(new Rect(65, 40, 200, 30), "Go to the next level!");
        

        if (GUI.Button(new Rect(50, 150, 75, 30), "NEXT"))
        {
            showPopUp = false;
            int ind = Application.loadedLevel;

            //save progress
            WWWForm empty = new WWWForm();
            string url = "http://test7293.comli.com/lvl.php";
            string hash = "securitystuff";
            string formNick = PlayerPrefs.GetString("myform_nick");
            string formPassword = "";
            

            WWWForm form = new WWWForm();
            form.AddField("myform_hash", hash); //hash and hash :D preveri če je isti uporabnik (sql)
            form.AddField("myform_nick", formNick);//check nick
            form.AddField("myform_pass", formPassword);//check pass
            form.AddField("myform_lvl", ind+1);//level check
            WWW www = new WWW(url, form);

            StartCoroutine(WaitForRequest(www));
            form = empty;

            Application.LoadLevel(ind + 1);
        }

    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
    }


    void coin_pick_up(Collider2D other)
    {
        if(other.gameObject.tag == "Gold_coin")
        {
            SoundCoin.Play();
            score += 25;
            IzpisiText();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Silver_coin")
        {
            SoundCoin.Play();
            score += 100;
            IzpisiText();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Blue_coin")
        {
            SoundCoin.Play();
            score += 500;
            IzpisiText();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Red_coin")
        {
            SoundHellWhispers.Play();
            SoundHell.Play();
            SoundHeartbeat.Play();
            score += 5000;
            //GetComponent<SpriteRenderer>().color = Color.red;
            jumpForce = jumpForce - (jumpForce / 5);
            maxSpeed = maxSpeed - (maxSpeed / 5);
            cam.backgroundColor = hell_colour;
            IzpisiText();
            hell = true;
            anim.SetBool("Hell", true);
            anim.SetTrigger("ToHell");
            if (sparks.transform.localScale.x > 1)
                sparks.transform.localScale = new Vector3(0, 0, 0);


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

        if (other.gameObject.tag == "Diamond")
        {
            SoundDiamond.Play();
            hell = false;
            anim.SetBool("Hell", false);
            anim.SetTrigger("ToEden");
            score += 1500;
            jumpForce = 2650;
            maxSpeed = 30f;
            amountOfAdditionalJumps++;
            DoubleJumpingCoolDownTimer = DoubleJumpingCoolDown;
            if (sparks.transform.localScale.x == 0)
                sparks.transform.localScale = new Vector3(3f, 3f, 0);
            IzpisiText();
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
            SoundHellWhispers.Stop();
            SoundHeartbeat.Stop();
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

    void In_game_settings() // Gumbi so v LoadOnClick.cs!
    {
        if(Input.GetKeyDown(KeyCode.O))//or when pause button.
        {
            //load half transparent scene on top of this one, with sound control and exit button.
            pause();
            ingameo++;
            if(ingameo == 1)
            {
                Application.LoadLevelAdditive("InGameOptions");
            }
            else
            {
                Destroy(GameObject.Find("DestroyMe"));
                ingameo = 0;
            }
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

    void doubleJumpTimer()
    {
        if (DoubleJumpingCoolDownTimer < 0)
        {
            DoubleJumpingCoolDownTimer = 0;
        }

        if (DoubleJumpingCoolDownTimer > 0)
        {
            DoubleJumpingCoolDownTimer -= Time.deltaTime;
        }

        if(DoubleJumpingCoolDownTimer == 0)
        {
            amountOfAdditionalJumps = 0;
            if (sparks.transform.localScale.x > 1)
                sparks.transform.localScale = new Vector3(0, 0, 0);
            IzpisiText();
        }
    }
}
