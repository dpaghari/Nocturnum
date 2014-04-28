using UnityEngine;
using System.Collections;

public class HelperText : MonoBehaviour {
	public GUISkin uiSkin;
	
	string tutorialText = "Survive against the enemies.";
	int makeMiddle = Screen.width/2-130;
	public GameObject timerRef;
	public GameObject isBuilt;
	public DumbTimer timerScript;
	private bool missionComplete;
	//public bool wait = false;
	
	// Use this for initialization
	void Start () {
		missionComplete = false;
		timerScript = DumbTimer.New(10.0f, 1.0f);
		timerRef = GameObject.Find ("UserInterface");
		isBuilt = GameObject.Find ("player_prefab");
	}
	
	// Update is called once per frame
	void Update () {
		if(missionComplete){
			timerScript.Update();

		}
		if(timerScript.Finished()){
			ResManager.Reset();
			TechManager.Reset();
			Application.LoadLevel("scene1");
			timerScript.Reset();


		}
		
	}
	
	void OnGUI () {
		
		// Super janky temporary tutorial thing. Happy birthday.
		GUI.skin = uiSkin;
		//GUI.Label(new Rect(Screen.width-250, 5, 250, 100), string.Format ("Enemies Killed: {0}/{1}", MetricManager.getEnemiesKilled, "40"));
		GUI.Label(new Rect(makeMiddle, 70, Screen.width/2, 100), tutorialText);
		if(timerRef.GetComponent<UserInterface>().gameTimeSec < 59 && timerRef.GetComponent<UserInterface>().gameTimeMin == 0){
			if (timerRef.GetComponent<UserInterface>().gameTimeSec > 3 && timerRef.GetComponent<UserInterface>().gameTimeSec < 10 && ResManager.Lumen == 0 /*&& !wait*/){
				makeMiddle = Screen.width/2-170;
				tutorialText = "Collect Lumen to build structures.";
			} else if(ResManager.Lumen > 0 && ResManager.MaxEnergy == 0){
				makeMiddle = Screen.width/2-90;
				tutorialText = "Press B to access Build Grid.";
			} else if(/*ResManager.Lumen > 0 && ResManager.MaxEnergy > 0 &&*/ isBuilt.GetComponent<CanBuild>().builtGenerator && TechManager.GetNumBuilding(Tech.ballistics) == 0){
				makeMiddle = Screen.width/2-150;
				tutorialText = "You can only build in the Generator's light.";
			} else if(/*ResManager.Lumen > 0 && ResManager.MaxEnergy > 0 &&*/ isBuilt.GetComponent<CanBuild>().builtGenerator && TechManager.GetNumBuilding(Tech.ballistics) > 0 /*&& isBuilt.GetComponent<CanBuild>().MeetsRequirement(isBuilt.GetComponent<CanBuild>().ballisticsBuilding)*/){
				makeMiddle = Screen.width/2-160;
				tutorialText = "Press V to open the Upgrade Grid.";
			}
			else if(TechManager.hasGenerator == true0 && TechManager.hasScatter == true && TechManager.hasTurret == true && TechManager.hasWolves == true && TechManager.hasBeatenWolf){
				
				tutorialText = "Mission Clear!";
				missionComplete = true;


			}
			Invoke ("clearText", 3);
		} else {
			tutorialText = "";
		}



	}
	
	void clearText(){
		tutorialText = "";
		//wait = true;
	}
}