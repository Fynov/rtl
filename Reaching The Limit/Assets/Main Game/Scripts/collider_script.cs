using UnityEngine;
using System.Collections;

public class collider_script : MonoBehaviour {

    Animator jumpo;
    public float coolDown = 1;
    public float coolDownTimer;
	public float forceUP = 4000;
	public float forceSIDE = 0;

    void Start () {
       
        jumpo = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (coolDownTimer < 0)
        {
            coolDownTimer = 0;
        }

        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (coolDownTimer == 0 && other.gameObject.name == "Character")
        {
            jumpo.SetTrigger("green_Pad-true");
			other.collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceSIDE, forceUP));
            jumpo.SetTrigger("green_Pad-false");
            coolDownTimer = coolDown;
        }
    }
}
