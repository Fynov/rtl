using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

    Object[] myMusic; // declare this as Object array

    public AudioSource audio;

    //public AudioClip loop;

    //public AudioClip[] rap;

    //public AudioClip[] jazz;

    //public AudioClip[] claasic;

    //public AudioClip[] house;
    void Awake()
	{
            if (!audio.isPlaying)
            {
                int o = GameObject.FindGameObjectsWithTag("music").Length;
                if(o<=1)
                {
                    myMusic = Resources.LoadAll("Loop", typeof(AudioClip));
                    audio.clip = myMusic[0] as AudioClip;
                    DontDestroyOnLoad(this);
                }
            }
    }
    
    void Start()
    {
        Application.runInBackground = true;
    }

	void Update () {
            if (!audio.isPlaying)
            {
                int o = GameObject.FindGameObjectsWithTag("music").Length;
                if (o <= 1)
                {
                    playRandomMusic();
                }
            }

        /*
		if(Application.loadedLevelName == "Options")
		{
			GetComponent<AudioSource>().Stop();
			AudioBegin = false;
		}
		za izklopit ce zelimo musko menjat
		*/
    }

    // Update is called once per frame

    void playRandomMusic()
    {
        audio.clip = myMusic[Random.Range(0, myMusic.Length)] as AudioClip;
        audio.Play();
    }

    public void playRap()
    {
        myMusic = Resources.LoadAll("Rap", typeof(AudioClip));
        playRandomMusic();
    }
    public void playHouse()
    {
        myMusic = Resources.LoadAll("House", typeof(AudioClip));
        playRandomMusic();
    }
    public void playJazz()
    {
        myMusic = Resources.LoadAll("Jazz", typeof(AudioClip));
        playRandomMusic();
    }
    public void playClassic()
    {
        myMusic = Resources.LoadAll("Classic", typeof(AudioClip));
        playRandomMusic();
    }
    public void playLoop()
    {
        myMusic = Resources.LoadAll("Loop", typeof(AudioClip));
        playRandomMusic();
    }
}
