using UnityEngine;
using System.Collections;

public class HelperText : MonoBehaviour {
	public GUISkin uiSkin;
	public bool tutorialOn = true;
	public bool depositedLumen = true;
	
	//string tutorialText = "Survive against the enemies.";
	string tutorialText = "Protect your generators.";

	int makeMiddle = Screen.width/2;//-130;
	public GameObject timerRef;
	public GameObject isBuilt;
	public GameObject avatarRef;
	//public bool wait = false;
	
	// Use this for initialization
	void Start () {
		timerRef = GameObject.Find ("UserInterface");
		isBuilt = GameObject.Find ("player_prefab");
		avatarRef = GameObject.Find ("player_prefab");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI () {
		
		// Super janky temporary tutorial thing. Happy birthday.
		GUI.skin = uiSkin;


		//GUI.Label(new Rect(Screen.width-250, 5, 250, 100), string.Format ("Enemies Killed: {0}/{1}", MetricManager.getEnemiesKilled, "40"));
		if(tutorialOn){

		GUI.Box(new Rect(-10, 40, Screen.width+20, 30), tutorialText);
		if(timerRef.GetComponent<UserInterface>().gameTimeSec < 59 && timerRef.GetComponent<UserInterface>().gameTimeMin == 0){
			if (timerRef.GetComponent<UserInterface>().gameTimeSec > 3 && timerRef.GetComponent<UserInterface>().gameTimeSec < 10 && ResManager.Lumen == 0 /*&& !wait*/){
				makeMiddle = Screen.width/2-170;
				//tutorialText = "Collect Lumen to build structures.";
				tutorialText = "Collect Lumen from light geysers.";
					depositedLumen = false;
				} else if(avatarRef.GetComponent<HarvestLight>().HoldingLumen () && timerRef.GetComponent<UserInterface>().gameTimeSec > 11 && timerRef.GetComponent<UserInterface>().gameTimeSec < 15/*ResManager.Lumen > 0 && ResManager.MaxEnergy == 0*/){
				/*makeMiddle = Screen.width/2-90;
				//tutorialText = "Press B to build.";
				tutorialText = "Deposit Lumen in the generators";
					depositedLumen = false;*/
			} else if(/*ResManager.Lumen > 0 && ResManager.MaxEnergy > 0 &&*/ isBuilt.GetComponent<CanBuild>().builtGenerator && TechManager.GetNumBuilding(Tech.ballistics) == 0){
				makeMiddle = Screen.width/2-150;
				//tutorialText = "You can only build in the light.";
				tutorialText = "Shine your flashlight at the fog";
			} else if(/*ResManager.Lumen > 0 && ResManager.MaxEnergy > 0 &&*/ isBuilt.GetComponent<CanBuild>().builtGenerator && TechManager.GetNumBuilding(Tech.ballistics) > 0 /*&& isBuilt.GetComponent<CanBuild>().MeetsRequirement(isBuilt.GetComponent<CanBuild>().ballisticsBuilding)*/){
				makeMiddle = Screen.width/2-160;
				//tutorialText = "Press V to open the upgrade menu.";
				tutorialText = "Trap the fog in the plants";
			}
			Invoke ("clearText", 3);
		} else {
				tutorialOn = false;
			tutorialText = "";
		}
		}
	}
	
	void clearText(){
		tutorialText = "";
		//wait = true;
	}
}