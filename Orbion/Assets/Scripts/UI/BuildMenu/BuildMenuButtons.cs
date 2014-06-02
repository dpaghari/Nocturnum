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
		buildScript.SetConstruction(buildScript.generatorBuilding);
	}
	public void CallBuildBallistics(){
		buildScript.SetConstruction(buildScript.ballisticsBuilding);
	}
	public void CallBuildWall(){
		buildScript.SetConstruction(buildScript.wallBuilding);
	}
	public void CallBuildMedBay(){
		buildScript.SetConstruction(buildScript.medBayBuilding);
	}
	public void CallBuildIncendiary(){
		buildScript.SetConstruction(buildScript.incindiaryBuilding);
	}
	public void CallBuildSpotlight(){
		buildScript.SetConstruction(buildScript.turretBuilding);
	}
	public void CallBuildTurret(){
		buildScript.SetConstruction(buildScript.photonBuilding);
	}
	public void CallBuildPhoton(){
		buildScript.SetConstruction(buildScript.spotlightBuilding);
	}




}
