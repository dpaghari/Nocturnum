using UnityEngine;
using System.Collections;

public class HarvestLight : MonoBehaviour {
	public int harvestAmt = 5;
	private bool hasLumen = false;
	public float harvestRange = 2f;
	public float unloadRange = 2f;

	private bool lit = false;
	private bool canDeposit = false;


	static bool IsLightWell(GameObject gobj){
		if (gobj.tag == "lightwell") return true;
		return false;
	}


	void TryHarvest(float range){
		GameObject geyser = Utility.GetClosestWith(transform.position, range, IsLightWell);
		if( geyser == null) return;
		
		hasLumen = true;
	}
	

	void TryUnload(float range){
		GameObject generator = Utility.GetClosestWith(transform.position, range, Utility.GoHasComponent<IsGenerator>);
		if( generator == null) return;
		generator.GetComponent<IsGenerator>().CurrLumen += harvestAmt;
		hasLumen = false;
	}
	

	// Use this for initialization
	void Start () {
		//hasLumen = false;
		Debug.Log(hasLumen);
	}



	// Update is called once per frame
	void Update () {

		
		if(Input.GetMouseButtonDown(1)){
			if (hasLumen) 
				TryUnload(unloadRange);
			else 
				TryHarvest(harvestRange); 

		}
		
	}


	void OnTriggerStay(Collider other){
	}
 


}
