using UnityEngine;
using System.Collections;

public class TutorialMissionText : MonoBehaviour {
	public dfLabel tutorialLine;

	public hasOverdrive overdriveScript;
	public DumbTimer timerScript;
	public DumbTimer nextlevelScript;
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
	
	// Use this for initialization
	void Start () {
		mouseHelp = false;
		moveHelp = false;
		madeBat = false;
		madeWolf = false;
		timerScript = DumbTimer.New(4.5f, 1.0f);
		nextlevelScript = DumbTimer.New(10.0f);
		//cleartextScript = DumbTimer.New(5.0f, 1.0f);
		overdriveScript = GameManager.AvatarContr.GetComponent<hasOverdrive>();
		tutorialLine.IsVisible = true;
		gotLumen = false;
		builtGenerator = false;

	}
	
	// Update is called once per frame
	void Update () {
	//	cleartextScript.Update();

		if(!moveHelp){
			tutorialLine.Text = "Press  W   A   S   D  to move";
			timerScript.Update();
			if(timerScript.Finished()){
				//tutorialLine.Text = "";
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
			//clone = Instantiate(demoWolf, pos, Quaternion.identity) as GameObject;
		}
		if(MetricManager.getEnemiesKilled >= 2){
			tutorialLine.Text = "Good Luck!";

			nextlevelScript.Update();
			if(nextlevelScript.Finished()){
				TechManager.missionComplete = true;
				nextlevelScript.Reset();
			}

		}


		/*
		if(cleartextScript.Finished() == true){
			tutorialLine.Text = "";
			cleartextScript.Reset();
		}
		*/
		if(overdriveScript.overdriveOn){
			if(!overdriveScript.overdriveActive)
				tutorialLine.Text = "Press SPACE to activate Overdrive!";
			
			if(Input.GetKeyDown(KeyCode.Space)){
				
				tutorialLine.Text = "";
			}
		}
		
	}
}
