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
	private Buildings toBuild = Buildings.none;
	private bool menuUp = false;
	private int menuCounter = 0;
	
	//UI Stuff
	public GUISkin buildWheelSkin;
	private float rotAngle = 40;
	private Vector2 pivotPoint;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI() {
		GUI.skin = buildWheelSkin;
		if(menuUp){
			GetComponent<CanShoot>().ResetFiringTimer();
			
			if(GUI.Button(new Rect(Screen.width/2-200,Screen.height/2-120,128,128), "Generator") && ResManager.Lumen >= generatorBuilding.GetComponent<Buildable>().cost && ResManager.MaxEnergy - ResManager.UsedEnergy >= generatorBuilding.GetComponent<Buildable>().energyCost) {
				//Debug.Log (ballisticsBuilding.GetComponent<Buildable>().cost.ToString());
				toBuild = Buildings.generator;
				menuUp = false;
				ResManager.RmLumen(generatorBuilding.GetComponent<Buildable>().cost);
				ResManager.AddUsedEnergy(generatorBuilding.GetComponent<Buildable>().energyCost);
			}
			
			// Make the second button.
			pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);
			GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			
			if(GUI.Button(new Rect(Screen.width/2-190,Screen.height/2-140,128,128), "Ballistics") && ResManager.Lumen >= ballisticsBuilding.GetComponent<Buildable>().cost && ResManager.MaxEnergy - ResManager.UsedEnergy >= ballisticsBuilding.GetComponent<Buildable>().energyCost) {
				toBuild = Buildings.ballistics;
				menuUp = false;
				ResManager.RmLumen(ballisticsBuilding.GetComponent<Buildable>().cost);
				ResManager.AddUsedEnergy(ballisticsBuilding.GetComponent<Buildable>().energyCost);
			}
			
			// Make the third button.
			GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			
			if(GUI.Button(new Rect(Screen.width/2-195,Screen.height/2-160,128,128), "Wall") && ResManager.Lumen >= wallBuilding.GetComponent<Buildable>().cost && ResManager.MaxEnergy - ResManager.UsedEnergy >= wallBuilding.GetComponent<Buildable>().energyCost)  {
				toBuild = Buildings.wall;
				menuUp = false;
				ResManager.RmLumen(wallBuilding.GetComponent<Buildable>().cost);
				ResManager.AddUsedEnergy(wallBuilding.GetComponent<Buildable>().energyCost);
			}
			
			// Make the fourth button.
			GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			
			if(GUI.Button(new Rect(Screen.width/2-210,Screen.height/2-180,128,128), "Med Bay") && ResManager.Lumen >= medBayBuilding.GetComponent<Buildable>().cost && ResManager.MaxEnergy - ResManager.UsedEnergy >= medBayBuilding.GetComponent<Buildable>().energyCost)  {
				toBuild = Buildings.medBay;
				menuUp = false;
				ResManager.RmLumen(medBayBuilding.GetComponent<Buildable>().cost);
				ResManager.AddUsedEnergy(medBayBuilding.GetComponent<Buildable>().energyCost);
//				Debug.Log("hullo");
			}
			
			// Make the fifth button.
			GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);;
			
			if(GUI.Button(new Rect(Screen.width/2-180,Screen.height/2-200,128,128), "Incindiary") && ResManager.Lumen >= incindiaryBuilding.GetComponent<Buildable>().cost && ResManager.MaxEnergy - ResManager.UsedEnergy >= incindiaryBuilding.GetComponent<Buildable>().energyCost)  {
				toBuild = Buildings.incindiary;
				menuUp = false;
				ResManager.RmLumen(incindiaryBuilding.GetComponent<Buildable>().cost);
				ResManager.AddUsedEnergy(incindiaryBuilding.GetComponent<Buildable>().energyCost);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButton(0) && toBuild != Buildings.none){
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
			
			//Build the right building
			switch(toBuild){
			case Buildings.generator:
				mousePos.y += 1;
				GetComponent<CanShoot>().ResetFiringTimer();
				clone = Instantiate(generatorBuilding, mousePos, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
				break;
			case Buildings.ballistics:
				GetComponent<CanShoot>().ResetFiringTimer();
				clone = Instantiate(underConstructionBuilding, hit.point, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
				clone.gameObject.GetComponent<IsUnderConstruction>().toBuild = ballisticsBuilding;
				break;
			case Buildings.wall:
				GetComponent<CanShoot>().ResetFiringTimer();
				clone = Instantiate(underConstructionBuilding, hit.point, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
				clone.gameObject.GetComponent<IsUnderConstruction>().toBuild = wallBuilding;
				break;
			case Buildings.medBay:
				GetComponent<CanShoot>().ResetFiringTimer();
				clone = Instantiate(underConstructionBuilding, hit.point, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
				clone.gameObject.GetComponent<IsUnderConstruction>().toBuild = medBayBuilding;
				break;
			case Buildings.incindiary:
				GetComponent<CanShoot>().ResetFiringTimer();
				clone = Instantiate(underConstructionBuilding, hit.point, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
				clone.gameObject.GetComponent<IsUnderConstruction>().toBuild = incindiaryBuilding;
				break;
			default:
				break;
			}
			toBuild = Buildings.none;
		}
		
		if (Input.GetKeyDown(KeyCode.B) && !menuUp){
			menuUp = true;
			menuCounter = 50;
		}
		if (Input.GetKeyDown(KeyCode.B) && menuCounter <= 0){
			menuUp = false;
		}
		if (menuCounter > 0) {
			menuCounter --;
		}
		
	}
	
}