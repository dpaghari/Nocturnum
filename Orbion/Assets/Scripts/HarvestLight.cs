using UnityEngine;
using System.Collections;

public class HarvestLight : MonoBehaviour {
	public int LumenCount = 0;
	public bool hasLumen = false;
	private bool lit = false;
	private bool canDeposit = false;
	// Use this for initialization
	void Start () {
		Debug.Log(hasLumen);
	}
	
	// Update is called once per frame
	void Update () {



		// If the player is near a Light Geyser and Presses Right Click
		// Harvest the Lumen
		if(lit) {
			if(Input.GetMouseButtonDown(1) && hasLumen == false){
			hasLumen = true;
			Debug.Log (hasLumen);
			}
		}


		//  If the player is near a Generator and Has Harvested Lumen
		//  and if he presses right click it gives the generator +20 Lumen
		if(canDeposit){

			if(Input.GetMouseButtonDown(1) && hasLumen == true){

				ResManager.AddLumen(20);
				hasLumen = false;
				Debug.Log(LumenCount);

			}

		}


	}

	void OnTriggerStay(Collider other){
		if(other.tag == "lightwell") 
			lit = true;
		else
			lit = false;


		if(other.tag == "Generator"){
			canDeposit = true;
			//Debug.Log (canDeposit);
		}
		else{
			canDeposit = false;
			//Debug.Log (canDeposit);
		}
	}


}
