using UnityEngine;
using System.Collections;

public enum Buildings {none, generator, ballistics, wall, medBay, incindiary};
[RequireComponent(typeof(AudioSource))]
public class CanBuild : MonoBehaviour {
	
	public Rigidbody generatorBuilding;
	public Rigidbody ballisticsBuilding;
	public Rigidbody underConstructionBuilding;
	public Rigidbody wallBuilding;
	public Rigidbody medBayBuilding;
	public Rigidbody incindiaryBuilding;
	public Rigidbody turretBuilding;
	public Rigidbody refractionBuilding;
	public Rigidbody spotlightBuilding;
	public AudioClip initBuild;

	private Rigidbody clone;

	//temporarily public until we make function to turn on/off menu
	//private Rigidbody toBuild = null;
	public Rigidbody toBuild = null;

	//temporarily public until we make function to turn on/off menu
	//public bool MenuUp { get; private set;}
	public bool MenuUp { get; set;}

	//temporarily public until we make function to turn on/off menu
	//private int menuCounter = 0;
	public int menuCounter = 0;
	
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
	public Texture2D button_refraction;
	public Texture2D button_spotlight;
	//Checks (temporary until we have metrics manager working.
	public bool builtBallistics = false;
	public bool builtGenerator = false;
	//For slowing down
	public bool inBuilding = false;

	private bool isDragBuilding = false;
	private CanResearch researchScript;

	private float dragDelay = 0.15f; //note: the delay gets affected by build slow down
	private DumbTimer dragTimer;
	

	// Use this for initialization
	void Start () {
		MenuUp = false;
		researchScript = GetComponent<CanResearch>();
		dragTimer = DumbTimer.New(dragDelay);
	}


	//Returns true only if we have enough lumen, energy, and we satisfy the prereqs
	//Don't have restrictions on energy if we're making a generator because
	//it won't let you build another one if you're UsedEnergy > MaxEnergy
	bool MeetsRequirement(Rigidbody buildingType){
		Buildable buildInfo = buildingType.GetComponent<Buildable>();
		if ( ResManager.Lumen < buildInfo.cost) return false;
		if ( ResManager.UsedEnergy + buildInfo.energyCost > ResManager.MaxEnergy)
			if( buildingType != generatorBuilding) return false;
		if ( !TechManager.IsTechAvaliable( buildInfo.TechType)) return false;
		return true;
	}


	void SetConstruction(Rigidbody buildingType){
		Buildable buildInfo = buildingType.GetComponent<Buildable>();
		if ( MeetsRequirement( buildingType)){
			MenuUp = false;
			toBuild = buildingType;

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

	
	void OnGUI() {
		GUI.skin = buildWheelSkin;
		if(MenuUp){

			GetComponent<CanShoot>().ResetFiringTimer();
			// Generator Button
			if( GUI.Button(new Rect(Screen.width/2-64,Screen.height/2-192,128,128), button_generator)) {
				SetConstruction(generatorBuilding);
			}
			
			// Ballistics Button 
			//pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint
			if( GUI.Button(new Rect(Screen.width/2-192,Screen.height/2-192,128,128), button_ballistics)) {
				SetConstruction(ballisticsBuilding);
			}
			
			// Wall Button
			// Make the third button.
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if( GUI.Button(new Rect(Screen.width/2+64,Screen.height/2-192,128,128), button_wall))  {
				SetConstruction(wallBuilding);
			}
			
			// Medbay Button
			// Make the fourth button.
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if( GUI.Button(new Rect(Screen.width/2-192,Screen.height/2-64,128,128), button_medbay))  {
				SetConstruction(medBayBuilding);
			}
			//Incendiary Button
			
			// Make the fifth button.
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);;
			if( GUI.Button(new Rect(Screen.width/2+64,Screen.height/2-64,128,128), button_incendiary)) {
				SetConstruction(incindiaryBuilding);
			}
			// Turret Button
			// Make the sixth button.
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);;
			if( GUI.Button(new Rect(Screen.width/2+64,Screen.height/2+92,128,128), button_turret)) {
				SetConstruction(turretBuilding);
			}
			// Refraction Button
			// Make the seventh button.
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);;
			if( GUI.Button(new Rect(Screen.width/2-64,Screen.height/2+92,128,128), button_refraction)) {
				SetConstruction(refractionBuilding);
			}
			// Spotlight Button
			// Make the eigth button.
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);;
			if( GUI.Button(new Rect(Screen.width/2-192,Screen.height/2+92,128,128), button_spotlight)) {
				SetConstruction(spotlightBuilding);
			}
			if(Time.timeScale ==1.0f){
				Time.timeScale = 0.5f;
				// Checks for if the player is 
				inBuilding = true;
		}
	} else {
			// is set to false when the player puts down a building or
			// if the menu button is gone and the player hasn't chosen.
			// a building. Check for this is in the Update().
			if (!inBuilding){
				Time.timeScale = 1.0f;
				Time.fixedDeltaTime = 0.02f*Time.timeScale;
			}
		}
	}
	

	// Update is called once per frame
	void Update () {

		dragTimer.Update();

		if(Input.GetMouseButton(0) && toBuild != null){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			// Casts the ray and get the first game object hit
			Physics.Raycast(ray, out hit);
			//floor layer is currently at 10 update this if that changes
			int layerMask = 1 << 10; 
			
			//raycasting onto lightshards were making the angle at which we shoot wonky
			//so only raycast onto things with the floor layer
			Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
			
			//[Don't delete] debug code for showing where our mouse position is parsing into. 
			//Debug.DrawRay (ray.origin, hit.point);
			Vector3 mousePos = hit.point;
			

			if (MeetsRequirement(toBuild) && dragTimer.Finished() ) {
				GetComponent<CanShoot>().ResetFiringTimer();
				audio.PlayOneShot(initBuild, 1.0f);
				if (toBuild == generatorBuilding){
					mousePos.y += 5.25f;
					clone = Instantiate(underConstructionBuilding, mousePos, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
					clone.GetComponent<IsUnderConstruction>().toBuild = toBuild;
					clone.GetComponent<IsUnderConstruction>().canBuildOutOfLight = true;
					builtGenerator = true;
					// Slows down during placing building.
					inBuilding = false;
				}
				else{
					mousePos.y += 5.25f;

					//Quaternion.LookRotation(Vector3.forward, Vector3.up)
					clone = Instantiate(underConstructionBuilding, mousePos, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
					clone.GetComponent<IsUnderConstruction>().toBuild = toBuild;
					// Slows down during placing building.
					inBuilding = false;
				}
				
				Buildable buildInfo = toBuild.GetComponent<Buildable>();
				ResManager.RmLumen(buildInfo.cost);
				ResManager.AddUsedEnergy(buildInfo.energyCost);
					
				if( toBuild == wallBuilding){
					inBuilding = true;
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
			
			toBuild = null;
			inBuilding = false;
			isDragBuilding = false;
			dragTimer.SetProgress(1.0f);
		}
		




		if (Input.GetKeyDown(KeyCode.B) && !MenuUp){
			if( researchScript != null && !researchScript.MenuUp){
				MenuUp = true;
				toBuild = null;
				menuCounter = 50;
			}
		}
			

		if (Input.GetKeyDown(KeyCode.B) && menuCounter <= 0){
			MenuUp = false;
			toBuild = null;
			// Check for if the player just opens and closes.
			inBuilding = false;
		}

		if (menuCounter > 0) {
			menuCounter --;
		}

		if ( Input.GetKeyDown( KeyCode.V) && MenuUp && researchScript != null){
			MenuUp = false;
			toBuild = null;
			// Check for if the player just opens and closes.
			inBuilding = false;

			researchScript.MenuUp=true;
		}

		
	}
	
}