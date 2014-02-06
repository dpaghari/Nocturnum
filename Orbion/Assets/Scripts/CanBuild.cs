using UnityEngine;
using System.Collections;

public enum Buildings {none, generator, weaponLab, wall};

public class CanBuild : MonoBehaviour {

	public Rigidbody generatorBuilding;
	public Rigidbody weaponLabBuilding;
	public Rigidbody wallBuilding;
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
				//Debug.Log (weaponLabBuilding.GetComponent<Buildable>().cost.ToString());
				toBuild = Buildings.generator;
				menuUp = false;
				ResManager.RmLumen(generatorBuilding.GetComponent<Buildable>().cost);
				ResManager.AddUsedEnergy(generatorBuilding.GetComponent<Buildable>().energyCost);
			}
			
			// Make the second button.
			pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);
			GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);

			if(GUI.Button(new Rect(Screen.width/2-190,Screen.height/2-140,128,128), "Weapon Lab") && ResManager.Lumen >= weaponLabBuilding.GetComponent<Buildable>().cost && ResManager.MaxEnergy - ResManager.UsedEnergy >= weaponLabBuilding.GetComponent<Buildable>().energyCost) {
				toBuild = Buildings.weaponLab;
				menuUp = false;
				ResManager.RmLumen(weaponLabBuilding.GetComponent<Buildable>().cost);
				Debug.Log (weaponLabBuilding.GetComponent<Buildable>().cost.ToString());
				ResManager.AddUsedEnergy(weaponLabBuilding.GetComponent<Buildable>().energyCost);
			}

			// Make the third button.
			GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			
			if(GUI.Button(new Rect(Screen.width/2-195,Screen.height/2-160,128,128), "Wall"))  {
				toBuild = Buildings.wall;
				menuUp = false;
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
			Vector3 adjustY = Vector3.zero;
			adjustY.y += 1;
			//Build the right building
			switch(toBuild){
			case Buildings.generator:
				GetComponent<CanShoot>().ResetFiringTimer();
				generatorBuilding = Instantiate(generatorBuilding, hit.point + adjustY, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
				break;
			case Buildings.weaponLab:
				GetComponent<CanShoot>().ResetFiringTimer();
				weaponLabBuilding = Instantiate(weaponLabBuilding, hit.point + adjustY, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
				break;
			case Buildings.wall:
				GetComponent<CanShoot>().ResetFiringTimer();
				wallBuilding = Instantiate(wallBuilding, hit.point + adjustY, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
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
