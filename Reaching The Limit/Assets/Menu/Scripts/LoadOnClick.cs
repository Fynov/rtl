using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public void loadScene(int level){
		Application.LoadLevel(level);
	}

	public void quitThis(){
		Application.Quit();
	}
}
