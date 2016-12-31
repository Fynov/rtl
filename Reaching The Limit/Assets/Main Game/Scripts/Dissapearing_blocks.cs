using UnityEngine;
using System.Collections;

public class Dissapearing_blocks : MonoBehaviour {

    // Use this for initialization
    public float currentCoolDown;
    public float coolDown = 2;
    bool touched = false;
    public GameObject diss;
    bool happened = false;

    void Start () {
	   
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
        if(currentCoolDown == 0 && touched && !happened)
        {
            GameObject m = (GameObject)Instantiate(diss, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(m, 0.8f);
            Destroy(this.gameObject, 0.4f);
            happened = true;

        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Character")
        {
            touched = true;
            currentCoolDown = coolDown;
        }
    }
}
