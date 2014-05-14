using UnityEngine;
using System.Collections;

public class TutorialText : MonoBehaviour {
	public dfLabel tutorialLine;
	bool gotLumen = false;
	bool pressedB = false;
	bool fiveSecPassed = false;
	bool pressedV = false;

	// Use this for initialization
	void Start () {
		tutorialLine.IsVisible = false;
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
			tutorialLine.Text = "Press B to access the Build Grid.";
			if(Input.GetKeyDown(KeyCode.B)){
				tutorialLine.Text = "Build a Generator.";
			}
		} else if(TechManager.GetNumBuilding (Tech.generator) > 0 && !fiveSecPassed && !pressedV){
			tutorialLine.Text = "You can only build structures within a Generator's light.";
		}
	}
}
