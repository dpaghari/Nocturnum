//Purpose: Allows player to bring up and interact with the build menu

using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class CanBuild : MonoBehaviour {
	
	//Building prefab references
	public Rigidbody generatorBuilding;
	public Rigidbody ballisticsBuilding;
	public Rigidbody underConstructionBuilding;
	public Rigidbody wallBuilding;
	public Rigidbody medBayBuilding;
	public Rigidbody incindiaryBuilding;
	public Rigidbody turretBuilding;
	public Rigidbody photonBuilding;
	public Rigidbody spotlightBuilding;

	//Build and ErrorBuild sounds
	public AudioClip initBuild;
	public AudioClip errBuild;
	
	//
	public dfPanel _buildMenuPanel;


	//If the build menu is activated
	public bool MenuUp { get; private set;}
	
	//The building we are about to construct
	public Rigidbody toBuild {get; private set;}
	

	//BuildingMode occurs when the player has bought a building but has yet to place it onto the map.
	//Setting inBuildingMode true/false will slowdown/restore time
	private float slowDownRatio = 0.5f;
	private float originalFixedUpdate = 0.02f;
	private bool _inBuildingMode = false;
	public bool inBuildingMode{
		get{ return _inBuildingMode;}
		
		set{
			if( value == true){
				Time.timeScale = slowDownRatio;
				Time.fixedDeltaTime = originalFixedUpdate * slowDownRatio;
			}
			else{
				Time.timeScale = 1.0f;
				Time.fixedDeltaTime = originalFixedUpdate;
			}
			_inBuildingMode = value;
		}
		
	}



	//If the player is currently doing a multi building placement by dragging the mouse
	private bool isDragBuilding = false;

	//The time (secs) inbetween each placement of a new building while dragging
	//*** The delay gets affected by build slow down ***
	private float dragDelay = 0.05f; 
	private DumbTimer dragTimer;

	//The gameobject we're using as a cursor for building
	private IsBuildHologram currHologram;

	private CanResearch researchScript;

	

	void Awake(){
		originalFixedUpdate = Time.fixedDeltaTime;
	}


	void Start () {
		toBuild = null;
		MenuUp = false;
		_buildMenuPanel.IsVisible = false;
		researchScript = GetComponent<CanResearch>();
		dragTimer = DumbTimer.New(dragDelay);
	}


	//Returns true only if we have enough lumen, energy, and we satisfy the prereqs
	public bool MeetsRequirement(Rigidbody buildingType){
		Buildable buildInfo = buildingType.GetComponent<Buildable>();
		if ( ResManager.Lumen < buildInfo.lumenCost){
			audio.PlayOneShot(errBuild, 0.5f);
			return false;
		}

		if ( ResManager.Energy < buildInfo.energyCost){
			audio.PlayOneShot(errBuild, 0.5f);
			return false;
		}

		if ( !TechManager.IsTechAvaliable( buildInfo.TechType)) {
			audio.PlayOneShot(errBuild, 0.5f);
			return false;
		}

		return true;
	}


	//Sets buildingType as the structure to build if requirements are met
	public void SetConstruction(Rigidbody buildingType){
		Buildable buildInfo = buildingType.GetComponent<Buildable>();
		if ( MeetsRequirement( buildingType)){
			CloseMenu();
			toBuild = buildingType;
			inBuildingMode = true;
			GameObject holoObj = Instantiate(buildInfo.hologram, Utility.GetMouseWorldPos(0), Quaternion.identity) as GameObject;
			currHologram = holoObj.GetComponent<IsBuildHologram>();
		}
		
	}


	public void OpenMenu(){
		//Prevents having both menus up at once
		if( researchScript != null && researchScript.MenuUp) return;

		MenuUp = true;
		_buildMenuPanel.IsVisible = true;
		toBuild = null;
		inBuildingMode = true;
		if( currHologram) GameObject.Destroy( currHologram.gameObject);
	}


	public void CloseMenu(){
		MenuUp = false;
		_buildMenuPanel.IsVisible = false;
		toBuild = null;
		inBuildingMode = false;
		if( currHologram) GameObject.Destroy( currHologram.gameObject);
	}


	//If requirements are met, creates the chosen building at the mouse position
	private void Construct(){
		Vector3 mousePos = Utility.GetMouseWorldPos(5.25f);
		
		if (MeetsRequirement(toBuild) && dragTimer.Finished() ) {

			//Stops player from shooting when pressing mouse to construct
			GetComponent<CanShoot>().ResetFiringTimer();
			
			if( currHologram) GameObject.Destroy( currHologram.gameObject);
			Rigidbody clone = Instantiate(underConstructionBuilding, mousePos, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
			audio.PlayOneShot(initBuild, 1.0f);			

			//Passing building info to the under-construction object
			IsUnderConstruction constructScript = clone.GetComponent<IsUnderConstruction>();
			Buildable buildInfo = toBuild.GetComponent<Buildable>();
			constructScript.toBuild = toBuild;
			constructScript.totalConstruction = buildInfo.buildTime;
			constructScript.canBuildOutOfLight = !buildInfo.requiresLight;
			
			inBuildingMode = false;
			
			
			ResManager.RmLumen(buildInfo.lumenCost);
			ResManager.RmEnergy(buildInfo.energyCost);
			
			if( toBuild == wallBuilding){
				SetConstruction( wallBuilding);
				isDragBuilding = true;
			}
			else{
				toBuild = null;
				dragTimer.SetProgress(1.0f);
			}
			
			dragTimer.Reset();
		}
	}


	void Update () {


		if( currHologram)
			currHologram.transform.position = Utility.GetMouseWorldPos(0);


		if(GameManager.KeysEnabled){

			//Open/Close build menu
			if(Input.GetKeyDown(KeyCode.Escape))
				CloseMenu ();
			
			if (Input.GetKeyDown(KeyCode.B)){
				if( MenuUp)
					CloseMenu();
				else
					OpenMenu();
			}
			
			//Switch from build menu to research menu
			if ( Input.GetKeyDown( KeyCode.V) && MenuUp && researchScript != null){
				CloseMenu();
				researchScript.OpenMenu();
			}
			//create the inunderconstruction building
			if(Input.GetMouseButton(0) && toBuild != null && currHologram.CanBuildHere){
				if( currHologram.CanBuildHere)
					Construct();
			}


			//If we let go of the mouse, we shouldn't be drag building anymore
			if( Input.GetMouseButtonUp(0) && isDragBuilding){
				CloseMenu();
				isDragBuilding = false;
				dragTimer.SetProgress(1.0f);
			}

		}

		dragTimer.Update();
		
	}
	
}