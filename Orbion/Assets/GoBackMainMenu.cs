using UnityEngine;
using System.Collections;

public class GoBackMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void goMainMenu(){
			Time.timeScale = 1.0f;

			AutoFade.LoadLevel("MainMenu", 3.0f, 3.0f, Color.black);
	}
}
