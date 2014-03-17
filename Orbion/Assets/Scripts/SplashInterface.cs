using UnityEngine;
using System.Collections;

public class SplashInterface : MonoBehaviour {

	public GUISkin uiSkin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter)){
			AutoFade.LoadLevel ("scene1", 1, 1, Color.black);
		}
		//"MyLevelName" ,3,1,Color.black
	}

	void OnGUI () {
		GUI.skin = uiSkin;

		GUI.Label(new Rect(Screen.width/2-70, Screen.height/2, 500, 50), "Press Enter to Start.");
	}
}
