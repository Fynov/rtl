using UnityEngine;
using System.Collections;

public class moving_spike_block : MonoBehaviour {

    // Use this for initialization
    public float speed = 30f;
    public float leftXpos;
    public float rightXpos;
    public float currentXpos;
    GameObject o;
    Rigidbody2D r;

    public float currentCoolDown;
    public float coolDown = 2;


    void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (currentCoolDown < 0)
        {
            currentCoolDown = 0;
        }

        if (currentCoolDown > 0)
        {
            currentCoolDown -= Time.deltaTime;
        }

        currentXpos = transform.position.x;
        //if (GetComponent<Rigidbody2D>().velocity.x < 29f)
        //{
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        //}
	    if(currentXpos < leftXpos && currentCoolDown == 0)
        {
            currentCoolDown = coolDown;
            GetComponent<Rigidbody2D>().velocity = new Vector2(+speed, 0);
        }
        if (currentXpos > rightXpos && currentCoolDown == 0)
        {
            currentCoolDown = coolDown;
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        }
    }

  
}
