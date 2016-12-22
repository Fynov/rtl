using UnityEngine;
using System.Collections;

public class Explosion_on_collision : MonoBehaviour {

    // Use this for initialization
    public GameObject[] destroy;
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
            if (destroy.Length > 0)
            {
                foreach(GameObject g in destroy)
                {
                    Destroy(g.gameObject, 0.2f);
                }
            }
        }
    }
}
