using UnityEngine;
using System.Collections;

public class TutorialMissionText : MonoBehaviour {
	public dfLabel tutorialLine;

	public hasOverdrive overdriveScript;
	public DumbTimer timerScript;
	public DumbTimer plantTutScript;
	//public DumbTimer cleartextScript;
	public Transform tutWolfpos;
	public GameObject demoWolf;
	public GameObject demoBat;
	private GameObject clone;
	bool gotLumen;
	bool builtGenerator;
	bool madeWolf;
	bool madeBat;
	bool moveHelp;
	bool mouseHelp;
	bool hasKilledtutWolves;
	bool hastaughtPlants;
	bool hastaughtStructures;
	bool lastString;
	bool hastaughtDash;
	// Use this for initialization
	void Start () {
		hastaughtDash = false;
		lastString = false;
		hastaughtStructures = false;
		hastaughtPlants = false;
		hasKilledtutWolves = false;
		mouseHelp = false;
		moveHelp = false;
		madeBat = false;
		madeWolf = false;
		timerScript = DumbTimer.New(4.5f, 1.0f);
		plantTutScript = DumbTimer.New(10.0f);
		//cleartextScript = DumbTimer.New(5.0f, 1.0f);
		tutorialLine.IsVisible = true;
		gotLumen = false;
		builtGenerator = false;

	}
	
	// Update is called once per frame
	void Update () {
	//	cleartextScript.Update();

		if(!moveHelp){
			tutorialLine.Text = "Move with W A S D";
			timerScript.Update();
			if(timerScript.Finished()){
				timerScript.Reset();
				moveHelp = true;
			}

		}
		if(moveHelp && !mouseHelp && ResManager.Lumen <= 0){
			tutorialLine.Text = "Your Orbion's light shines toward your mouse";
  			timerScript.Update();
			if(timerScript.Finished()){
				timerScript.Reset();

				mouseHelp = true;
			}

		}
		if(ResManager.Lumen <= 0 && gotLumen == false && mouseHelp == true){
			tutorialLine.Text = "Collect Lumen to build structures";
		}			
		if(ResManager.Lumen > 0 && gotLumen == false){
			tutorialLine.Text = "Press B to Open the Build Menu";
			gotLumen = true;
		}
		if(gotLumen == true && TechManager.hasGenerator == false){
			if(builtGenerator)
			timerScript.Update();
			if(Input.GetKeyDown(KeyCode.B)){
				tutorialLine.Text = "Build a Generator to provide light and energy";
				builtGenerator = true;
			}
			if(timerScript.Finished() == true && MetricManager.getEnemiesKilled < 3){

				tutorialLine.Text = "Shoot at the building under construction to speed it up";
				timerScript.Reset();
			}
		}
		if(TechManager.hasGenerator && madeWolf == false){

			Vector3 pos = tutWolfpos.position;
			clone = Instantiate(demoWolf, pos, Quaternion.identity) as GameObject;
			clone = Instantiate(demoWolf, pos, Quaternion.identity) as GameObject;
			tutorialLine.Text = "Defeat Enemies to gain Lumen";
			madeWolf = true;
			//TechManager.missionComplete = true;
		}
		if(MetricManager.getEnemiesKilled == 2 && madeBat == false){
			Vector3 pos = tutWolfpos.position;
			clone = Instantiate(demoBat, pos, Quaternion.identity) as GameObject;
			madeBat = true;
			tutorialLine.Text = "Enemies are weaker and slower in your generator's light";
			hasKilledtutWolves = true;
			//clone = Instantiate(demoWolf, pos, Quaternion.identity) as GameObject;
		}
		if(hasKilledtutWolves && !hastaughtStructures){
			tutorialLine.Text = "Structures you build can unlock upgrades";
			plantTutScript.Update();
			hastaughtStructures = true;
		}
		if(hastaughtStructures){
			plantTutScript.Update();
		}


		if(plantTutScript.Finished() && !hastaughtDash){
			tutorialLine.Text =  "Shift + Direction to [Dash]";
			plantTutScript.Reset();
			hastaughtDash = true;
		}

		if(plantTutScript.Finished() && !hastaughtPlants){

			tutorialLine.Text = "Shooting plants can activate them and be used to your advantage!";
			hastaughtPlants = true;
			plantTutScript.Reset();
			lastString = true;
		}
		if(plantTutScript.Finished() && lastString){

			tutorialLine.Text = "Good Luck Agent!";
		}

	
		
	}
}
