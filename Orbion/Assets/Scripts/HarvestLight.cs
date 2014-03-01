using UnityEngine;
using System.Collections;

public class HarvestLight : MonoBehaviour {

	public bool hasLumen = false;
	private bool lit = false;
	// Use this for initialization
	void Start () {
		Debug.Log(hasLumen);
	}
	
	// Update is called once per frame
	void Update () {
	
		if(lit) {
			if(Input.GetMouseButtonDown(1) && hasLumen == false){
			hasLumen = true;
			Debug.Log (hasLumen);
			}
		}


	}

	void OnTriggerStay(Collider other){
		if(other.tag == "lightwell") 
			lit = true;
	}
}
