// PURPOSE:  Checks if the player presses Enter and moves to level 1
using UnityEngine;
using System.Collections;

public class isLoadScreen : MonoBehaviour {
	public GUISkin titleSkin;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown ("return") || Input.GetKeyDown ("enter")){
			AutoFade.LoadLevel("level1" ,2.0f,2.0f,Color.black);
		}
	}

	void OnGUI(){
		GUI.skin = titleSkin;
		GUI.Label(new Rect(Screen.width/2-150,Screen.height/2+350,300,128), "Press ENTER to Continue.");
	}


}
