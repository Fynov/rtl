using UnityEngine;
using System.Collections;

public class collider_script : MonoBehaviour {

    Animator jumpo;
    public float coolDown = 1;
    public float coolDownTimer;
	public float force = 4000;

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
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (coolDownTimer == 0)
        {
            jumpo.SetTrigger("green_Pad-true");
			collisionInfo.collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force));
            jumpo.SetTrigger("green_Pad-false");
            coolDownTimer = coolDown;
        }
    }
}
