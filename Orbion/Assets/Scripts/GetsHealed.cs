using UnityEngine;
using System.Collections;

public class GetsHealed : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other){
		if(other.tag == "MedBay"){
			this.gameObject.GetComponent<Killable>().Heal(1);
		}
	}
}
