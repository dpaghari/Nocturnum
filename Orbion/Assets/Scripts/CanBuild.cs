using UnityEngine;
using System.Collections;

public enum Buildings {none, generator, weaponLab};

public class CanBuild : MonoBehaviour {

	public Rigidbody generatorBuilding;
	public Rigidbody weaponLabBuilding;
	private Rigidbody clone;
	private Buildings toBuild = Buildings.none;
	private bool menuUp = false;

	// Use this for initialization
	void Start () {
		
	}

	void OnGUI() {
		if(menuUp){
			GetComponent<CanShoot>().ResetFiringTimer();
			GUI.Box(new Rect (10,10,100,90), "Loader Menu");

			if(GUI.Button(new Rect(20,40,80,20), "Generator")) {
				toBuild = Buildings.generator;
				menuUp = false;
				//ResourceManager.Instance.RemoveLight(generatorBuilding.cost);
				//ResourceManager.Instance.AddUsedEnergy(generatorBuilding.energyCost);
			}
			
			// Make the second button.
			if(GUI.Button(new Rect(20,70,80,20), "Weapon Lab")) {
				toBuild = Buildings.weaponLab;
				menuUp = false;
				//ResourceManager.Instance.RemoveLight(weaponLabBuilding.cost);
				//ResourceManager.Instance.AddUsedEnergy(weaponLabBuilding.energyCost);
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
				clone = Instantiate(generatorBuilding, hit.point + adjustY, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
				break;
			case Buildings.weaponLab:
				GetComponent<CanShoot>().ResetFiringTimer();
				clone = Instantiate(weaponLabBuilding, hit.point + adjustY, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
				break;
			default:
				break;
			}
			toBuild = Buildings.none;
		}

		if (Input.GetKeyDown(KeyCode.B)){
			menuUp = true;
		}
	}

}
