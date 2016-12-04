using UnityEngine;
using System.Collections;

public class cactus : MonoBehaviour {

    Animator cactu;
	// Use this for initialization
	void Start () {
        cactu = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            cactu.SetTrigger("triggered");
        }
    }
}
