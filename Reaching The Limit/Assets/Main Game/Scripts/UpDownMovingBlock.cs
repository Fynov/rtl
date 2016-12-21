using UnityEngine;
using System.Collections;

public class UpDownMovingBlock : MonoBehaviour {

    // Use this for initialization
    public float speed = 30f;
    public float topYpos;
    public float botYpos;
    public float currentYpos;
    GameObject o;
    Rigidbody2D r;

    public float currentCoolDown;
    public float coolDown = 2;


    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCoolDown < 0)
        {
            currentCoolDown = 0;
        }

        if (currentCoolDown > 0)
        {
            currentCoolDown -= Time.deltaTime;
        }

        currentYpos = transform.position.y;
        //if (GetComponent<Rigidbody2D>().velocity.x < 29f)
        //{
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        //}
        if (currentYpos < botYpos && currentCoolDown == 0)
        {
            currentCoolDown = coolDown;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, +speed);
        }
        if (currentYpos > topYpos && currentCoolDown == 0)
        {
            currentCoolDown = coolDown;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
        }
    }
}
