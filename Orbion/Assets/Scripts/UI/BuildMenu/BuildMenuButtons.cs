using UnityEngine;
using System.Collections;

public class BuildMenuButtons : MonoBehaviour {
	//public dfPanel _panel;
	//private bool menuUp;
	public GameObject canBuildRef;

	// Use this for initialization
	void Start () {
		//menuUp = false;
		//_panel.IsVisible = false;
		canBuildRef = GameObject.Find ("player_prefab");
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

	public void CallBuildGenerator(){
		canBuildRef.GetComponent<CanBuild>().ConstructGenerator();
	}
	public void CallBuildBallistics(){
		canBuildRef.GetComponent<CanBuild>().ConstructBallistics();
	}
	public void CallBuildWall(){
		canBuildRef.GetComponent<CanBuild>().ConstructWall();
	}
	public void CallBuildMedBay(){
		canBuildRef.GetComponent<CanBuild>().ConstructMedBay();
	}
	public void CallBuildIncendiary(){
		canBuildRef.GetComponent<CanBuild>().ConstructIncendiary();
	}
	public void CallBuildSpotlight(){
		canBuildRef.GetComponent<CanBuild>().ConstructSpotlight();
	}
	public void CallBuildTurret(){
		canBuildRef.GetComponent<CanBuild>().ConstructTurret();
	}
	public void CallBuildPhoton(){
		canBuildRef.GetComponent<CanBuild>().ConstructPhoton();
	}
}
