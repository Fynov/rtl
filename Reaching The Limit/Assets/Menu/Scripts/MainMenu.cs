// main menu
// attach to main camera

using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public Texture backgroundTexture;

	void onGUI(){
		//display bg texture
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), backgroundTexture);
	}
}
