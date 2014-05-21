using UnityEngine;
using System.Collections;

public class TutorialText : MonoBehaviour {
	public dfLabel tutorialLine;
	bool gotLumen = false;
	bool pressedB = false;
	bool fiveSecPassed = false;
	bool pressedV = false;
	float delay;
	public hasOverdrive overdriveScript;

	// Use this for initialization
	void Start () {
		overdriveScript = GameManager.AvatarContr.GetComponent<hasOverdrive>();
		tutorialLine.IsVisible = true;
		delay = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(!gotLumen && !pressedB && !fiveSecPassed && !pressedV && TechManager.GetNumBuilding (Tech.generator) == 0){
			tutorialLine.Text = "Collect Lumen to build structures.";
			if(ResManager.Lumen > 0){
				tutorialLine.Text = "";
				gotLumen = true;
			}
		} else if(gotLumen && !pressedB && !fiveSecPassed && !pressedV && TechManager.GetNumBuilding (Tech.generator) == 0){
			tutorialLine.Text = "Press B to open/close the Build Grid.";
			if(Input.GetKeyDown(KeyCode.B)){
				tutorialLine.Text = "Build a Generator. (Shoot to build faster.)";
				pressedB = true;
			}

		} else if(TechManager.GetNumBuilding (Tech.generator) > 0 && !fiveSecPassed && !pressedV){
			tutorialLine.Text = "You can only build other structures within a Generator's light.";
			gotLumen = true;
			pressedB = true;
			if(delay == 0){
				delay = Time.time + 5;
			} else {
				if(Time.time >= delay){
					delay = 0;
				fiveSecPassed = true;

				}
			}
		} else if(fiveSecPassed && !pressedV){
			tutorialLine.Text = "Press V to open/close the Upgrade Grid.";
			if(Input.GetKeyDown(KeyCode.V)){
				pressedV = true;
				tutorialLine.Text = "Upgrade to Scattershot.";
			}
		} else if(TechManager.hasScatter){
			gotLumen = true;
			pressedB = true;
			pressedV = true;
			fiveSecPassed = true;
			pressedV = true;
			tutorialLine.Text = "";
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
