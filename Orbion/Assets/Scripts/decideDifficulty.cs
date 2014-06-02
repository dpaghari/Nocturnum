//PURPOSE:  functions to be called by the onClick event for the buttons in the Difficulty Screen
// sets the difficulty of the spawner to the selected difficulty.
using UnityEngine;
using System.Collections;

public class decideDifficulty : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void setNormal(){
		GameManager.GameDifficulty = 1;
		AutoFade.LoadLevel("IntroStory", 2.0f, 2.0f, Color.black);
	}
	public void setVeteran(){
		GameManager.GameDifficulty = 2;
		AutoFade.LoadLevel("IntroStory", 2.0f, 2.0f, Color.black);
	}
	public void setMaster(){
		GameManager.GameDifficulty = 3;
		AutoFade.LoadLevel("IntroStory", 2.0f, 2.0f, Color.black);
	}
}
