using UnityEngine;
using System.Collections;

public class TitleText : MonoBehaviour {

	//public GUISkin titleSkin;
	public dfLabel pressAnyKey;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		pressAnyKey.Text = "Press Any Key";
		if(Input.anyKeyDown){
			AutoFade.LoadLevel("MainMenu" ,1.0f,1.0f,Color.black);
		}
	}
	/*
	void OnGUI(){
		//GUI.skin = titleSkin;
		//GUI.Label(new Rect(Screen.width/2-70,Screen.height/2+120,300,128), "Press Enter.");
	}
	*/
}
