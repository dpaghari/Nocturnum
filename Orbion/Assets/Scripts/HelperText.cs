using UnityEngine;
using System.Collections;

public class HelperText : MonoBehaviour {
	public GUISkin uiSkin;
	
	string tutorialText = "Survive against the enemies.\n" + "Use WASD to move and the left mouse button to shoot.\n" + "Walk over Lumen to collect it";
	public GameObject timerRef;
	
	// Use this for initialization
	void Start () {
		timerRef = GameObject.Find ("UserInterface");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI () {

		// Super janky temporary tutorial thing. Happy birthday.
		GUI.skin = uiSkin;
		GUI.Label(new Rect(Screen.width-250, 5, 250, 100), string.Format ("Enemies Killed: {0}/{1}", MetricManager.getEnemiesKilled, "40"));
		GUI.Label(new Rect(30, 40, Screen.width/2, 100), tutorialText);
		if(timerRef.GetComponent<UserInterface>().gameTimeSec < 35 && timerRef.GetComponent<UserInterface>().gameTimeMin == 0){
			/*if (timerRef.GetComponent<UserInterface>().gameTimeSec > 3 && timerRef.GetComponent<UserInterface>().gameTimeSec < 10 && ResManager.Lumen == 0){
				tutorialText = "Use left mouse button to shoot, and walk over Lumen to collect it.";
			} else*/ if(ResManager.Lumen > 0 && ResManager.MaxEnergy == 0){
				tutorialText = "Press B to open the Build Wheel. Build a generator.";
			} else if(ResManager.Lumen > 0 && ResManager.MaxEnergy > 0 && ResManager.Lumen > 431){
				tutorialText = "Now, build a Ballistics Building. You can only build in the light.";
			} else if(ResManager.Lumen > 0 && ResManager.MaxEnergy > 0 && ResManager.Lumen < 431 && timerRef.GetComponent<UserInterface>().gameTimeSec < 30){
				tutorialText = "You can also shoot buildings to make them build faster.\n" + "Press V to open the Upgrade Wheel and choose scattershot once your building is done.";
			} else if(ResManager.Lumen > 0 && ResManager.MaxEnergy > 0 && ResManager.Lumen < 431 && timerRef.GetComponent<UserInterface>().gameTimeSec > 30){
				tutorialText = "Build your base, get upgrades, and defend against the incoming enemies!";
			}
		} else {
			tutorialText = "";
		}
	}
}
