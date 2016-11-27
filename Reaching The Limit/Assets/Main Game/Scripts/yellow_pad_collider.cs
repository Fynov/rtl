using UnityEngine;
using System.Collections;

public class yellow_pad_collider : MonoBehaviour
{

    Animator jumpo;
    public float coolDown = 1;
    public float coolDownTimer;

    void Start()
    {
        jumpo = GetComponent<Animator>();
    }

    void Update()
    {
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
            jumpo.SetTrigger("yellow_pad_true");
            collisionInfo.collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 6500));
            jumpo.SetTrigger("yellow_pad_false");
            coolDownTimer = coolDown;
        }

    }
}

