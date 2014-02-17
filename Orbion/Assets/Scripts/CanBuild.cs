using UnityEngine;
using System.Collections;

public enum Buildings {none, generator, ballistics, wall, medBay, incindiary};

public class CanBuild : MonoBehaviour {
	
	public Rigidbody generatorBuilding;
	public Rigidbody ballisticsBuilding;
	public Rigidbody underConstructionBuilding;
	public Rigidbody wallBuilding;
	public Rigidbody medBayBuilding;
	public Rigidbody incindiaryBuilding;

	private Rigidbody clone;
	private Rigidbody toBuild = null;
	private bool menuUp = false;
	private int menuCounter = 0;
	
	//UI Stuff
	public GUISkin buildWheelSkin;
	private float rotAngle = 40;
	private Vector2 pivotPoint;
	

	// Use this for initialization
	void Start () {
		
	}


	//Returns true only if we have enough lumen, energy, and we satisfy the prereqs
	bool MeetsRequirement(Rigidbody buildingType){
		Buildable buildInfo = buildingType.GetComponent<Buildable>();
		if ( ResManager.Lumen < buildInfo.cost) return false;
		if ( ResManager.UsedEnergy + buildInfo.energyCost > ResManager.MaxEnergy) return false;
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

	
	void OnGUI() {
		GUI.skin = buildWheelSkin;
		if(menuUp){
			GetComponent<CanShoot>().ResetFiringTimer();
			
			if( GUI.Button(new Rect(Screen.width/2-200,Screen.height/2-120,128,128), "Generator")) {
				SetConstruction(generatorBuilding);
			}
			
			// Make the second button.
			pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);
			GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if( GUI.Button(new Rect(Screen.width/2-190,Screen.height/2-140,128,128), "Ballistics")) {
				SetConstruction(ballisticsBuilding);
			}
			

			// Make the third button.
			GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if( GUI.Button(new Rect(Screen.width/2-195,Screen.height/2-160,128,128), "Wall"))  {
				SetConstruction(wallBuilding);
			}
			

			// Make the fourth button.
			GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if( GUI.Button(new Rect(Screen.width/2-210,Screen.height/2-180,128,128), "Med Bay"))  {
				SetConstruction(medBayBuilding);
			}
			

			// Make the fifth button.
			GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);;
			if( GUI.Button(new Rect(Screen.width/2-180,Screen.height/2-200,128,128), "Incindiary")) {
				SetConstruction(incindiaryBuilding);
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
				if (toBuild == generatorBuilding){
					mousePos.y += 1;
					clone = Instantiate(generatorBuilding, mousePos, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
				}
				else{
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