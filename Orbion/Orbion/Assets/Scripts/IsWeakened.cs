using UnityEngine;
using System.Collections;

public class IsWeakened : MonoBehaviour {

	public AIController enemyStat;

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("GAY");
		if(other.tag == "lightsource")
		GetComponent<AIController>().isWeakened = true;

	}
	void OnTriggerStay(Collider other)
	{
		Debug.Log ("WEINER");
		if(other.tag == "lightsource")
		GetComponent<AIController>().isWeakened = true;
	}
	void OnTriggerExit(Collider other)
	{
		Debug.Log ("LOL");
		if(other.tag == "lightsource")
		GetComponent<AIController>().isWeakened = false;
	}
}
