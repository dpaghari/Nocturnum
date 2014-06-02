using UnityEngine;
using System.Collections;

public class BuildMenuButtons : MonoBehaviour {
	//public dfPanel _panel;
	//private bool menuUp;
	public GameObject canBuildRef;
	public CanBuild buildScript; //Ref. to the build script.
	public CanResearch researchScript; //Ref. to the research script.

	public AudioClip buttonSound;

	// Use this for initialization
	void Start () {
		buildScript = GameManager.AvatarContr.GetComponent<CanBuild>();
		//menuUp = false;
		//_panel.IsVisible = false;
		//canBuildRef = GameObject.Find ("player_prefab");
	}
	
	// Update is called once per frame
	void Update () {

		//Shortcuts
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

	//Methods to call for Buildings.
	public void CallBuildGenerator(){
		audio.PlayOneShot(buttonSound, 0.2f);
		buildScript.SetConstruction(buildScript.generatorBuilding);
	}
	public void CallBuildBallistics(){
		audio.PlayOneShot(buttonSound, 0.2f);
		buildScript.SetConstruction(buildScript.ballisticsBuilding);
	}
	public void CallBuildWall(){
		audio.PlayOneShot(buttonSound, 0.2f);
		buildScript.SetConstruction(buildScript.wallBuilding);
	}
	public void CallBuildMedBay(){
		audio.PlayOneShot(buttonSound, 0.2f);
		buildScript.SetConstruction(buildScript.medBayBuilding);
	}
	public void CallBuildIncendiary(){
		audio.PlayOneShot(buttonSound, 0.2f);
		buildScript.SetConstruction(buildScript.incindiaryBuilding);
	}
	public void CallBuildSpotlight(){
		audio.PlayOneShot(buttonSound, 0.2f);
		buildScript.SetConstruction(buildScript.turretBuilding);
	}
	public void CallBuildTurret(){
		audio.PlayOneShot(buttonSound, 0.2f);
		buildScript.SetConstruction(buildScript.photonBuilding);
	}
	public void CallBuildPhoton(){
		audio.PlayOneShot(buttonSound, 0.2f);
		buildScript.SetConstruction(buildScript.spotlightBuilding);
	}




}
