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
		WeakensInLight weakenScript = other.GetComponent<WeakensInLight>();
		if(weakenScript) weakenScript.Weaken();

		Buildable buildableScript = other.GetComponent<Buildable> ();
		if (buildableScript)
						buildableScript.power ();
	}
}
