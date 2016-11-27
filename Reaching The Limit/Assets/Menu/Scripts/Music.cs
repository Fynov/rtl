using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	static bool AudioBegin = false; 
	void Awake()
	{
		if (!AudioBegin) {
			GetComponent<AudioSource>().Play ();
			DontDestroyOnLoad (gameObject);
			AudioBegin = true;
		} 
	}
	void Update () {

		/*
		if(Application.loadedLevelName == "Options")
		{
			GetComponent<AudioSource>().Stop();
			AudioBegin = false;
		}
		za izklopit ce zelimo musko menjat
		*/ 
	}
}
