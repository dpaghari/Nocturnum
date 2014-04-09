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
	public AudioClip initBuild;

	private Rigidbody clone;
	private Rigidbody toBuild = null;
	private bool menuUp = false;
	private int menuCounter = 0;
	
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

	//Checks (temporary until we have metrics manager working.
	public bool builtBallistics = false;
	public bool builtGenerator = false;
	

	// Use this for initialization
	void Start () {
		
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
			menuUp = false;
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
		if(menuUp){
			GetComponent<CanShoot>().ResetFiringTimer();
			
			if( GUI.Button(new Rect(Screen.width/2-64,Screen.height/2-192,128,128), button_generator)) {
				SetConstruction(generatorBuilding);
			}
			
			// Make the second button.
			//pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if( GUI.Button(new Rect(Screen.width/2-192,Screen.height/2-192,128,128), button_ballistics)) {
				SetConstruction(ballisticsBuilding);
			}
			
			
			// Make the third button.
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if( GUI.Button(new Rect(Screen.width/2+64,Screen.height/2-192,128,128), button_wall))  {
				SetConstruction(wallBuilding);
			}
			
			
			// Make the fourth button.
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if( GUI.Button(new Rect(Screen.width/2-192,Screen.height/2-64,128,128), button_medbay))  {
				SetConstruction(medBayBuilding);
			}
			
			
			// Make the fifth button.
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);;
			if( GUI.Button(new Rect(Screen.width/2+64,Screen.height/2-64,128,128), button_incendiary)) {
				SetConstruction(incindiaryBuilding);
			}
			// Make the sixth button.
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);;
			if( GUI.Button(new Rect(Screen.width/2+64,Screen.height/2+92,128,128), button_turret)) {
				SetConstruction(turretBuilding);
			}
		}
	}
	

	// Update is called once per frame
	void Update () {

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
			

			if (MeetsRequirement(toBuild)) {
				GetComponent<CanShoot>().ResetFiringTimer();
				audio.PlayOneShot(initBuild, 1.0f);
				if (toBuild == generatorBuilding){
					//mousePos.y += 1;
					clone = Instantiate(generatorBuilding, mousePos, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
					builtGenerator = true;
				}
				else{
					mousePos.y += 5.25f;

					//Quaternion.LookRotation(Vector3.forward, Vector3.up)
					clone = Instantiate(underConstructionBuilding, mousePos, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
					clone.GetComponent<IsUnderConstruction>().toBuild = toBuild;

				}
				
				Buildable buildInfo = toBuild.GetComponent<Buildable>();
				ResManager.RmLumen(buildInfo.cost);
				ResManager.AddUsedEnergy(buildInfo.energyCost);

				toBuild = null;
			}

		}
		


		if (Input.GetKeyDown(KeyCode.B) && !menuUp){
			menuUp = true;
			toBuild = null;
			menuCounter = 50;
		}

		if (Input.GetKeyDown(KeyCode.B) && menuCounter <= 0){
			menuUp = false;
			toBuild = null;
		}

		if (menuCounter > 0) {
			menuCounter --;
		}
		
	}
	
}