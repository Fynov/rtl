using UnityEngine;
using System.Collections;

public class spike_collider_script : MonoBehaviour {

    Animator spikes;
	// Use this for initialization
	void Start () {
        spikes = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        Debug.Log("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
        //print("There are " + collisionInfo.contacts.Length + " point(s) of contacts");
        //print("Their relative velocity is " + collisionInfo.relativeVelocity);
        spikes.SetTrigger("spike_trigger");
    }
}
