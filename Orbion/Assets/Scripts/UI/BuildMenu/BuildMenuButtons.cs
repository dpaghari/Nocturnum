using UnityEngine;
using System.Collections;

//TODO: doesn't automatically correct changes on prereq information

public class BuildMenuButtons : MonoBehaviour {
	//public dfPanel _panel;
	//private bool menuUp;
	public GameObject canBuildRef;
	public CanBuild buildScript; //Ref. to the build script.
	public CanResearch researchScript; //Ref. to the research script.

	public AudioClip buttonSound;

	public Tech techType = Tech.none;


	private dfLabel descriptionLabel;
	private dfLabel energyLabel;
	private dfLabel lumenLabel;
	//private dfLabel nameLabel;
	private Buildable buildInfo;



	void SetLabel(){
		foreach( Transform child in transform){
			switch( child.name){
				case "Description":
					descriptionLabel = child.GetComponent<dfLabel>();
					break;

				case "Energy":
					energyLabel = child.GetComponent<dfLabel>();
					break;

				case "Lumen":
					lumenLabel = child.GetComponent<dfLabel>();
					break;

				default:
					break;
			}
		}
	}

	Buildable GetBuildInfo(){
		Rigidbody theBuilding = null;
		switch( techType){
			case Tech.ballistics:
				theBuilding = buildScript.ballisticsBuilding;
				break;
			default:
				break;
		}

		if( theBuilding == null){
			string errMsg = string.Format( "techType: {0} of Object: {1}, not covered in switch block in GetBuildInfo() of BuildMenuButtons.cs", techType, this.name);
			Debug.LogError( errMsg);
			return null;
		}

		return theBuilding.GetComponent<Buildable>();

	}


	// Use this for initialization
	void Start () {
		buildScript = GameManager.AvatarContr.buildScript;
		SetLabel();

		if( TechManager.IsBuilding( techType)){
			buildInfo = GetBuildInfo();
			energyLabel.Text = string.Format("{0}: {1}", energyLabel.name, buildInfo.energyCost);
			lumenLabel.Text =  string.Format("{0}: {1}", lumenLabel.name, buildInfo.lumenCost);
		}
		else
			Debug.LogWarning( string.Format( "{0} has non building techType: {1}", this.name, techType)); 


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
		buildScript.SetConstruction(buildScript.spotlightBuilding);
	}
	public void CallBuildTurret(){
		audio.PlayOneShot(buttonSound, 0.2f);
		buildScript.SetConstruction(buildScript.turretBuilding);
	}
	public void CallBuildPhoton(){
		audio.PlayOneShot(buttonSound, 0.2f);
		buildScript.SetConstruction(buildScript.photonBuilding);
	}




}
