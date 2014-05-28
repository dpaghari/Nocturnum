using UnityEngine;
using System.Collections;

public class TitleText : MonoBehaviour {

	public GUISkin titleSkin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown ("return") || Input.GetKeyDown ("enter")){
			AutoFade.LoadLevel("tutorial" ,1.0f,1.0f,Color.black);
		}
	}

	void OnGUI(){
		GUI.skin = titleSkin;
		GUI.Label(new Rect(Screen.width/2-70,Screen.height/2+100,300,128), "Press Enter.");
	}
}
