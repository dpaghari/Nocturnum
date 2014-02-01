using UnityEngine;
using System.Collections;

public enum Buildings {none, generator, weaponLab};

public class CanBuild : MonoBehaviour {

	public Rigidbody generatorBuilding;
	public Rigidbody weaponLabBuilding;
	private Rigidbody clone;
	private Buildings toBuild = Buildings.none;
	private bool menuUp = false;
	private int menuCounter = 0;


	// Use this for initialization
	void Start () {
		
	}

	void OnGUI() {
		if(menuUp){
			GetComponent<CanShoot>().ResetFiringTimer();
			GUI.Box(new Rect (10,10,100,90), "Loader Menu");

			if(GUI.Button(new Rect(20,40,80,20), "Generator") && ResManager.Lumen >= generatorBuilding.GetComponent<Buildable>().cost && ResManager.MaxEnergy - ResManager.UsedEnergy >= generatorBuilding.GetComponent<Buildable>().energyCost) {
				Debug.Log (weaponLabBuilding.GetComponent<Buildable>().cost.ToString());
				toBuild = Buildings.generator;
				menuUp = false;
				ResManager.RmLumen(generatorBuilding.GetComponent<Buildable>().cost);
				ResManager.AddUsedEnergy(generatorBuilding.GetComponent<Buildable>().energyCost);
			}
			
			// Make the second button.
			if(GUI.Button(new Rect(20,70,80,20), "Weapon Lab") && ResManager.Lumen >= weaponLabBuilding.GetComponent<Buildable>().cost && ResManager.MaxEnergy - ResManager.UsedEnergy >= weaponLabBuilding.GetComponent<Buildable>().energyCost) {
				toBuild = Buildings.weaponLab;
				menuUp = false;
				ResManager.RmLumen(weaponLabBuilding.GetComponent<Buildable>().cost);
				Debug.Log (weaponLabBuilding.GetComponent<Buildable>().cost.ToString());
				ResManager.AddUsedEnergy(weaponLabBuilding.GetComponent<Buildable>().energyCost);
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
