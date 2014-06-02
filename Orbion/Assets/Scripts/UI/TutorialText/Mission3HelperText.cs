using UnityEngine;
using System.Collections;

public class Mission3HelperText : MonoBehaviour {
	public dfLabel tutorialLine;
	bool gotLumen = false;
	bool pressedB = false;
	bool fiveSecPassed = false;
	bool pressedV = false;
	float delay;
	public hasOverdrive overdriveScript;
	public DumbTimer timerScript;
	
	// Use this for initialization
	void Start () {
		timerScript = DumbTimer.New(5.0f, 1.0f);
		overdriveScript = GameManager.AvatarContr.GetComponent<hasOverdrive>();
		tutorialLine.IsVisible = true;
		delay = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(overdriveScript.overdriveOn){
			if(!overdriveScript.overdriveActive)
				tutorialLine.Text = "Press SPACE to activate Overdrive!";
			
			if(Input.GetKeyDown(KeyCode.Space)){
				
				tutorialLine.Text = "";
			}
		}
		
	}
}
