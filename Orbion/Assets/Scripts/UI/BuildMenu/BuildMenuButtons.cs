using UnityEngine;
using System.Collections;

public class BuildMenuButtons : MonoBehaviour {
	//public dfPanel _panel;
	//private bool menuUp;
	public GameObject canBuildRef;
	public CanBuild buildScript;
	public CanResearch researchScript;

	// Use this for initialization
	void Start () {
		buildScript = GameManager.AvatarContr.GetComponent<CanBuild>();
		//menuUp = false;
		//_panel.IsVisible = false;
		//canBuildRef = GameObject.Find ("player_prefab");
	}
	
	// Update is called once per frame
	void Update () {

			if(buildScript.MenuUp){
				if(Input.GetKeyDown(KeyCode.Alpha1)){
				CallBuildGenerator();
				}
				else if(Input.GetKeyDown(KeyCode.Alpha2))
				CallBuildBallistics();

				else if(Input.GetKeyDown(KeyCode.Alpha3))
				CallBuildMedBay();

				else if(Input.GetKeyDown(KeyCode.Alpha4))
				CallBuildTurret();

				else if(Input.GetKeyDown(KeyCode.Alpha5))
				CallBuildIncendiary();

				else if(Input.GetKeyDown(KeyCode.Alpha6))
				CallBuildPhoton();

				else if(Input.GetKeyDown(KeyCode.Alpha7))
				CallBuildSpotlight();

				else if(Input.GetKeyDown(KeyCode.Alpha8))
				CallBuildWall();
			}

	}

	public void CallBuildGenerator(){
		buildScript.ConstructGenerator();
	}
	public void CallBuildBallistics(){
		buildScript.ConstructBallistics();
	}
	public void CallBuildWall(){
		buildScript.ConstructWall();
	}
	public void CallBuildMedBay(){
		buildScript.ConstructMedBay();
	}
	public void CallBuildIncendiary(){
		buildScript.ConstructIncendiary();
	}
	public void CallBuildSpotlight(){
		buildScript.ConstructSpotlight();
	}
	public void CallBuildTurret(){
		buildScript.ConstructTurret();
	}
	public void CallBuildPhoton(){
		buildScript.ConstructPhoton();
	}
}
