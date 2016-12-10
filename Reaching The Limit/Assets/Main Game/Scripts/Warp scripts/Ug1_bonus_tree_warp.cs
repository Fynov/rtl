using UnityEngine;
using System.Collections;

public class Ug1_bonus_tree_warp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        collisionInfo.collider.GetComponent<Rigidbody2D>().transform.position = new Vector3(23, 350, 0);
    }
}
