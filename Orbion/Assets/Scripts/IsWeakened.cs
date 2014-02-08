using UnityEngine;
using System.Collections;

public class IsWeakened : MonoBehaviour {

	public AIController enemyStat;

	void OnTriggerEnter(Collider other)
	{

		/*if(other.tag == "lightsource")
		GetComponent<AIController>().isWeakened = true;
		*/
	}
	void OnTriggerStay(Collider other)
	{

		if(other.tag == "lightsource")
		GetComponent<AIController>().isWeakened = true;
	}
	void OnTriggerExit(Collider other)
	{
		//Debug.Log("Lols");
		//if(other.tag == "lightsource")
		GetComponent<AIController>().isWeakened = false;
	}
}
