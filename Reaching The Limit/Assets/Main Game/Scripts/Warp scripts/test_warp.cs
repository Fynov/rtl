using UnityEngine;
using System.Collections;

public class test_warp : MonoBehaviour {



	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        collisionInfo.collider.GetComponent<Rigidbody2D>().transform.position = new Vector3(475, 0, 0);
    }
}
