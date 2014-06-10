using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private bool beatenGame;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void gostartGame(){

		AutoFade.LoadLevel("difficultylevelselect", 2.0f, 2.0f, Color.black);
	}
	public void golevelSelect(){


		AutoFade.LoadLevel("LevelSelect", 1.0f, 2.0f, Color.black);
	}
	public void goOptions(){
		//GameManager.GameDifficulty = 3;
		AutoFade.LoadLevel("Options", 1.0f, 2.0f, Color.black);
	}
	public void goExit(){
		//GameManager.GameDifficulty = 3;
		Application.Quit();
	}
}
