using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

    public GameObject expl;
    public GameObject bigExpl;
    public float ObjectX;
    public float ObjectY;
    //public float DestroyObjectAtX;
    public float DestroyObjectAtY;
    public float currentYposition;
    public float currentXposition;

    GameObject pl;
    public float triggerPositionX;
    public float playerPosX;
    public float playerPosY;

    void Start()
    {
        pl = GameObject.Find("Character");
        bigExpl = GameObject.Find("Big Explosion");
        expl = GameObject.Find("Explosion");
        this.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosX = pl.transform.position.x;
        playerPosY = pl.transform.position.y;
        if(playerPosX > triggerPositionX)
        {
            this.GetComponent<Rigidbody2D>().isKinematic = false;
        }

        currentXposition = this.transform.position.x;
        currentYposition = this.transform.position.y;
        if (transform.position.y < DestroyObjectAtY)
        {
            GameObject m = (GameObject)Instantiate(bigExpl,new Vector3(currentXposition, currentYposition, 0), Quaternion.identity);
            Destroy(m.gameObject, 1);
            Destroy(this.gameObject);

        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name != "Character")
        {
            ObjectX = other.transform.position.x;
            ObjectY = other.transform.position.y;
            GameObject m = (GameObject)Instantiate(expl, new Vector3(ObjectX, ObjectY, 0), Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(m.gameObject, 1);
        }
    }
}
