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

	public void MuteAudio(){
			if (AudioListener.volume == 0) {
				AudioListener.volume = 0.5f;
				volumeSlider.value = AudioListener.volume;

			} else {
				AudioListener.volume = 0f;
				volumeSlider.value = AudioListener.volume;
			}
		
	}
}