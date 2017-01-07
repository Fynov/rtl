using UnityEngine;
using System.Collections;

public class fallingSpikes : MonoBehaviour {

    // Use this for initialization
    public float cXpos;
    public bool moving = false;
    public float TriggerPosition;
    public GameObject c;
    public AudioSource SoundDrop;


    void Start () {
        c = GameObject.Find("Character");
	}
	
	// Update is called once per frame
	void Update () {
        cXpos = c.transform.position.x;
        if (cXpos > TriggerPosition && moving == false)
        {
            moving = true;
            this.GetComponent<Rigidbody2D>().isKinematic = false;
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name != "Character")
        {
            SoundDrop.Play();
            this.tag = "Untagged";
        }
    }
}
