using UnityEngine;
using System.Collections;

public class yellow_pad_collider : MonoBehaviour {

    Animator jumpo;
    // Use this for initialization
    void Start()
    {
        Debug.Log("DELA");
        jumpo = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        Debug.Log("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
        //print("There are " + collisionInfo.contacts.Length + " point(s) of contacts");
        //print("Their relative velocity is " + collisionInfo.relativeVelocity);
        jumpo.SetTrigger("yellow_pad_true");
        collisionInfo.collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 6500));
        jumpo.SetTrigger("yellow_pad_false");
    }
}

