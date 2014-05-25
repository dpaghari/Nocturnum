using UnityEngine;
using System.Collections;

public enum Buildings {none, generator, ballistics, wall, medBay, incindiary};
[RequireComponent(typeof(AudioSource))]
public class CanBuild : MonoBehaviour {
	
	public bool MenuUp { get; private set;}
	private Rigidbody clone;
	public Rigidbody toBuild {get; private set;}
	private int menuCounter = 0;
	private CanResearch researchScript;
	public dfPanel _buildMenuPanel;
	private IsBuildHologram currHologram;



	private bool isDragBuilding = false;
	private float dragDelay = 0.05f; //note: the delay gets affected by build slow down
	private DumbTimer dragTimer;

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
	public AudioClip initBuild;
	public AudioClip errBuild;


	//UI Stuff
	public GUISkin buildWheelSkin;
	private float rotAngle = 40;
	private Vector2 pivotPoint;
	public Texture2D button_incendiary;
	public Texture2D button_wall;
	public Texture2D button_generator;
	public Texture2D button_ballistics;
	public Texture2D button_medbay;
	public Texture2D button_turret;
	public Texture2D button_photon;
	public Texture2D button_spotlight;


	//Checks (temporary until we have metrics manager working.
	public bool builtBallistics = false;
	public bool builtGenerator = false;


	//Setting inBuildingMode will slowdown/restore time
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


	

	// Use this for initialization
	void Start () {
		MenuUp = false;
		_buildMenuPanel.IsVisible = false;
		researchScript = GetComponent<CanResearch>();
		dragTimer = DumbTimer.New(dragDelay);
	}


	//Returns true only if we have enough lumen, energy, and we satisfy the prereqs
	//Don't have restrictions on energy if we're making a generator because
	//it won't let you build another one if you're UsedEnergy > MaxEnergy
	bool MeetsRequirement(Rigidbody buildingType){
		Buildable buildInfo = buildingType.GetComponent<Buildable>();
		if ( ResManager.Lumen < buildInfo.cost){
			audio.PlayOneShot(errBuild, 0.5f);
			return false;
		}
		/*
		if ( ResManager.Energy + buildInfo.energyCost > ResManager.MaxEnergy){
			if( buildingType != generatorBuilding){
				audio.PlayOneShot(errBuild, 0.5f);
				return false;
			}

		}
		*/
		if ( !TechManager.IsTechAvaliable( buildInfo.TechType)) {
			audio.PlayOneShot(errBuild, 0.5f);
			return false;
		}
		return true;
	}


	void SetConstruction(Rigidbody buildingType){
		Buildable buildInfo = buildingType.GetComponent<Buildable>();
		if ( MeetsRequirement( buildingType)){
			CloseMenu();
			toBuild = buildingType;
			inBuildingMode = true;
			GameObject holoObj = Instantiate(buildInfo.hologram, Utility.GetMouseWorldPos(0), Quaternion.identity) as GameObject;
			currHologram = holoObj.GetComponent<IsBuildHologram>();
		}
		
	}

	// Grabs Lumen and Energy.
	int getLumen(Rigidbody buildingType){
		Buildable buildInfo = buildingType.GetComponent<Buildable>();
		return buildInfo.cost;
	}
	
	int getEnergy(Rigidbody buildingType){
		Buildable buildInfo = buildingType.GetComponent<Buildable>();
		return buildInfo.energyCost;
	}


	public void OpenMenu(){
		MenuUp = true;
		_buildMenuPanel.IsVisible = true;
		toBuild = null;
		menuCounter = 50;
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

	
	void Awake(){
		originalFixedUpdate = Time.fixedDeltaTime;
		toBuild = null;
	}

	public void ConstructGenerator(){
		SetConstruction(generatorBuilding);
	}

	public void ConstructBallistics(){
		SetConstruction(ballisticsBuilding);
	}

	public void ConstructWall(){
		SetConstruction(wallBuilding);
	}

	public void ConstructMedBay(){
		SetConstruction(medBayBuilding);
	}

	public void ConstructIncendiary(){
		SetConstruction(incindiaryBuilding);
	}

	public void ConstructTurret(){
		SetConstruction(turretBuilding);
	}

	public void ConstructPhoton(){
		SetConstruction(photonBuilding);
	}

	public void ConstructSpotlight(){
		SetConstruction(spotlightBuilding);
	}


/*	void OnGUI() {
		GUI.skin = buildWheelSkin;
		if(MenuUp){
			GetComponent<CanShoot>().ResetFiringTimer();

			// Generator Button
			if( GUI.Button(new Rect(Screen.width/2-64,Screen.height/2-192,128,128), button_generator)){
				SetConstruction(generatorBuilding);
				//TechManager.hasGenerator = true;
			}
			
			// Ballistics Button 
			if( GUI.Button(new Rect(Screen.width/2-192,Screen.height/2-192,128,128), button_ballistics))
				SetConstruction(ballisticsBuilding);
			
			// Wall Button
			if( GUI.Button(new Rect(Screen.width/2+64,Screen.height/2-192,128,128), button_wall))
				SetConstruction(wallBuilding);
			
			// Medbay Button
			if( GUI.Button(new Rect(Screen.width/2-192,Screen.height/2-64,128,128), button_medbay))
				SetConstruction(medBayBuilding);

			//Incendiary Button
			if( GUI.Button(new Rect(Screen.width/2+64,Screen.height/2-64,128,128), button_incendiary))
				SetConstruction(incindiaryBuilding);

			// Turret Button
			if( GUI.Button(new Rect(Screen.width/2+64,Screen.height/2+92,128,128), button_turret))
				SetConstruction(turretBuilding);

			// Photon Button
			if( GUI.Button(new Rect(Screen.width/2-64,Screen.height/2+92,128,128), button_photon))
				SetConstruction(photonBuilding);

			// Spotlight Button
			if( GUI.Button(new Rect(Screen.width/2-192,Screen.height/2+92,128,128), button_spotlight))
				SetConstruction(spotlightBuilding);

		}

	}*/

	// Update is called once per frame
	void Update () {
		dragTimer.Update();


		if(MenuUp){
			GetComponent<CanShoot>().ResetFiringTimer();
		}

		if( currHologram)
			currHologram.transform.position = Utility.GetMouseWorldPos(0);


		//   INPUT
		//---------------------------------------------------------------------------------------------------//

		if(GameManager.KeysEnabled){
			if(Input.GetMouseButtonDown(0) && toBuild != null){
				if(MeetsRequirement(toBuild)){
				audio.PlayOneShot(initBuild, 1.0f);
				}
			}

			if(Input.GetMouseButton(0) && toBuild != null && currHologram.CanBuildHere){

				Vector3 mousePos = Utility.GetMouseWorldPos(5.25f);

				if (MeetsRequirement(toBuild) && dragTimer.Finished() ) {
					GetComponent<CanShoot>().ResetFiringTimer();

					if( currHologram) GameObject.Destroy( currHologram.gameObject);
					clone = Instantiate(underConstructionBuilding, mousePos, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;

					IsUnderConstruction constructScript = clone.GetComponent<IsUnderConstruction>();
					Buildable buildInfo = toBuild.GetComponent<Buildable>();

					constructScript.toBuild = toBuild;
					constructScript.totalConstruction = buildInfo.buildTime;

					// Slows down during placing building.
					inBuildingMode = false;

					if( toBuild == generatorBuilding){
						clone.GetComponent<IsUnderConstruction>().canBuildOutOfLight = true;
						builtGenerator = true;
					}

					ResManager.RmLumen(buildInfo.cost);
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
			
			//If we let go of the mouse, we shouldn't be building walls anymore
			if( Input.GetMouseButtonUp(0) && isDragBuilding){
				CloseMenu();
				isDragBuilding = false;
				dragTimer.SetProgress(1.0f);
			}

			if(Input.GetKeyDown(KeyCode.Escape))
				CloseMenu ();



			if (Input.GetKeyDown(KeyCode.B) && !MenuUp)
				if( researchScript != null && !researchScript.MenuUp)
					OpenMenu();
			
				
			// Check for if the player just opens and closes.
			if (Input.GetKeyDown(KeyCode.B) && menuCounter <= 0)
				CloseMenu();

			if (menuCounter > 0)
				menuCounter --;


			if ( Input.GetKeyDown( KeyCode.V) && MenuUp && researchScript != null){
				CloseMenu();
				researchScript.OpenMenu();
			}
		}

		
	}
	
}