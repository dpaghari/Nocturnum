using UnityEngine;
using System.Collections;

public class HarvestLight : MonoBehaviour {

	//the amount we harvest and unload
	public int harvestAmt = 5;

	//used to check if we've harvested
	private bool hasLumen = false;

	//range required to harvest from a lightwell
	public float harvestRange = 2f;

	//range required to unload into a generator
	public float unloadRange = 2f;


	static bool IsLightWell(GameObject gobj){
		if (gobj.tag == "lightwell") return true;
		return false;
	}


	//switches to harvested state if we're in range of a lightwell
	void TryHarvest(float range){
		GameObject geyser = Utility.GetClosestWith(transform.position, range, IsLightWell);
		if( geyser == null) return;
		
		hasLumen = true;
	}
	


	//unloads lumen into the closest generator in range, if any
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
