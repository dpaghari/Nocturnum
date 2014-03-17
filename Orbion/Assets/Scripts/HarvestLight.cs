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
	
	public AvatarController avatarScript;
	public bool inMethod = true;
	public bool rising = true;
	
	

	public GameObject orbion;


	static bool IsLightWell(GameObject gobj){
		if (gobj.tag == "lightwell") return true;
		return false;
	}
	
	
	//switches to harvested state if we're in range of a lightwell
	void TryHarvest(float range){
		GameObject geyser = Utility.GetClosestWith(transform.position, range, IsLightWell);
		if( geyser == null) return;
		if(inMethod){
			inMethod = false;
			ReturnHasLumen ();
			//Invoke ("ReturnHasLumen", 2);
		}
	}
	
	public bool HoldingLumen(){
		return hasLumen;
	}
	
	// Checks again if the player is still near, then spawn a lumen shard.
	void ReturnHasLumen(){
		inMethod = true;
		GameObject geyser = Utility.GetClosestWith(transform.position, harvestRange, IsLightWell);
		if( geyser == null) {
			Debug.Log ("hasLumen = " + hasLumen);
			return;
		}
		
		if(rising){
			geyser.GetComponent<GiveLumen>().spawnLumen();
		}
		rising = false;
		//avatarScript.rasAnim ();
		//Debug.Log ("Eridan was here");
		//hasLumen = true;
		
	}
	
	public void setRisingTrue(){
		rising = true;
	}
	
	public void setHoldingTrue(){
		hasLumen = true;
		orbion.GetComponent("Halo").GetType().GetProperty("enabled").SetValue(orbion.GetComponent("Halo"), true, null);
		geyser.GetComponent<GiveLumen>().spawnLumen();
		//Debug.Log ("Eridan was here");
		Debug.Log ("hasLumen = " + hasLumen);
	}
	
	
	
	//unloads lumen into the closest generator in range, if any
	void TryUnload(float range){
		GameObject generator = Utility.GetClosestWith(transform.position, range, Utility.GoHasComponent<IsGenerator>);
		if( generator == null) return;
		generator.GetComponent<IsGenerator>().CurrLumen += harvestAmt;
		hasLumen = false;
		orbion.GetComponent("Halo").GetType().GetProperty("enabled").SetValue(orbion.GetComponent("Halo"), false, null);
		avatarScript.rasAnim ();
		avatarScript.SendLightShard ();
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
