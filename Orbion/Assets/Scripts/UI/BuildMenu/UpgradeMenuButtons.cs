using UnityEngine;
using System.Collections;

public class UpgradeMenuButtons : MonoBehaviour {
	//public dfPanel _panel;
	//private bool menuUp;
	public GameObject canResearchRef;

	// Use this for initialization
	void Start () {
		//menuUp = false;
		//_panel.IsVisible = false;
		canResearchRef = GameObject.Find ("player_prefab");
	}
	
	// Update is called once per frame
	void Update () {
		/*if(Input.GetKeyDown(KeyCode.B)){
				menuUp = !menuUp;
		}
		if(menuUp){
			_panel.IsVisible = true;
		} else {
			_panel.IsVisible = false;
		}*/


	}

	public void CallScattershot(){
		canResearchRef.GetComponent<CanResearch>().GetScattershot();
	}
	public void CallClipSize(){
		canResearchRef.GetComponent<CanResearch>().GetClipSize();
	}
	public void CallLightFist(){
		canResearchRef.GetComponent<CanResearch>().GetLightFist();
	}
	public void CallLightGrenade(){
		canResearchRef.GetComponent<CanResearch>().GetLightGrenade();
	}
	public void CallSeeker(){
		canResearchRef.GetComponent<CanResearch>().GetSeeker();
	}
	public void CallRicochet(){
		canResearchRef.GetComponent<CanResearch>().GetRicochet();
	}
}
