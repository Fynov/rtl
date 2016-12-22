using UnityEngine;
using System.Collections;

public class Explosion_on_collision : MonoBehaviour {

    // Use this for initialization
    public GameObject destroy1;
    public GameObject destroy2;
    public GameObject destroy3;
    public GameObject destroy4;
    public GameObject expl;

    void Start () {
        expl = GameObject.Find("Explosion");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Character")
        {
            GameObject m = (GameObject)Instantiate(expl, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(m.gameObject, 1);
            Destroy(this.gameObject);
            if(destroy1 != null)
            {
                Destroy(destroy1.gameObject,0.2f);
            }
            if (destroy2 != null)
            {
                Destroy(destroy2.gameObject,0.2f);
            }
            if (destroy3 != null)
            {
                Destroy(destroy3.gameObject,0.2f);
            }
            if (destroy4 != null)
            {
                Destroy(destroy4.gameObject,0.2f);
            }
        }
    }
}
