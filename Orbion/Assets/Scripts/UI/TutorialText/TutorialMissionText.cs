using UnityEngine;
using System.Collections;

public class TutorialMissionText : MonoBehaviour {
	public dfLabel tutorialLine;

	public hasOverdrive overdriveScript;
	public DumbTimer timerScript;
	bool gotLumen;
	bool builtGenerator;
	
	// Use this for initialization
	void Start () {
		timerScript = DumbTimer.New(5.0f, 1.0f);
		overdriveScript = GameManager.AvatarContr.GetComponent<hasOverdrive>();
		tutorialLine.IsVisible = true;
		gotLumen = false;
		builtGenerator = false;

	}
	
	// Update is called once per frame
	void Update () {

		if(ResManager.Lumen <= 0 && gotLumen == false){
			tutorialLine.Text = "Collect Lumen to build structures";
		}			
		if(ResManager.Lumen > 0 && gotLumen == false){
			tutorialLine.Text = "Press B to Open the Build Menu";
			gotLumen = true;
		}
		if(gotLumen == true){
			if(builtGenerator)
			timerScript.Update();
			if(Input.GetKeyDown(KeyCode.B)){
				tutorialLine.Text = "Build a Generator to provide light and energy";
				builtGenerator = true;
			}
			if(timerScript.Finished() == true){

				tutorialLine.Text = "Shoot at the building under construction to speed it up";
				timerScript.Reset();
			}
		}
		if(overdriveScript.overdriveOn){
			if(!overdriveScript.overdriveActive)
				tutorialLine.Text = "Press SPACE to activate Overdrive!";
			
			if(Input.GetKeyDown(KeyCode.Space)){
				
				tutorialLine.Text = "";
			}
		}
		
	}
}
