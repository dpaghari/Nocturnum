using UnityEngine;
using System.Collections;

public class IsLightSource : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other){
		if(other.tag == "Player")
			other.GetComponentInChildren<IsLightCone>().ResetEnergy();
			//other.GetComponent<IsLightCone>().ResetEnergy();
		

		WeakensInLight weakenScript = other.GetComponent<WeakensInLight>();
		if(weakenScript) weakenScript.Weaken();
	}
}
