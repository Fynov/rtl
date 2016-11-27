using UnityEngine;

using UnityEngine.UI;

using System.Collections;

public class ChangeVolume : MonoBehaviour {

	void Awake()
	{
		volumeSlider.value = AudioListener.volume;
	}

	public Slider volumeSlider;
	public AudioSource volumeAudio;
	public void VolumeController(){
		AudioListener.volume = volumeSlider.value;
	}
}