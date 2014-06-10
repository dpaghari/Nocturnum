using UnityEngine;
using System.Collections;

public class isBackButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {



	}
	public void goBack(){
		if(Application.loadedLevelName == "Options"){
			AutoFade.LoadLevel("MainMenu", 1.0f, 1.0f, Color.black);
		}
		else if(Application.loadedLevelName == "LevelSelect"){
			AutoFade.LoadLevel("MainMenu", 1.0f, 1.0f, Color.black);
		}

	}
}
