using UnityEngine;
using System.Collections;

public class Trgger_remove_object : MonoBehaviour
{

    // Use this for initialization
    public int numberOfObjects;
    public GameObject[] objectsToRemove;
    public GameObject expl;
    public string existingObjectName; 

    void Start()
    {
        //objectsToRemove = new GameObject[numberOfObjects];
        expl = GameObject.Find("Explosion");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Character")
        {
            if (GameObject.Find(existingObjectName) != null)
            {
                if (objectsToRemove.Length > 0)
                {
                    foreach (GameObject g in objectsToRemove)
                    {
                        GameObject m = (GameObject)Instantiate(expl, new Vector3(g.transform.position.x, g.transform.position.y, 0), Quaternion.identity);
                        Destroy(m.gameObject, 1);
                        Destroy(g, 0.2f);
                    }
                }
            }
        }
    }
}
