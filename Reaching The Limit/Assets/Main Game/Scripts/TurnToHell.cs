using UnityEngine;
using System.Collections;

public class TurnToHell : MonoBehaviour {

    // Use this for initialization
    public GameObject play;
    public SpriteRenderer render;

    void Start()
    {
        play = GameObject.Find("Character");
    }
    // Update is called once per frame
    void Update() {
    /*    User_movement is_hell = GetComponent<User_movement>();
        if (is_hell.hell == true)
        {
            foreach (SpriteRenderer render in GetComponents<SpriteRenderer>())
            {
                render.material.color = Color.red;
            }
        }
        else
        {
             foreach (SpriteRenderer render in GetComponents<SpriteRenderer>())
             {
                render.material.color = Color.white;
             }
        }*/
    }
}
