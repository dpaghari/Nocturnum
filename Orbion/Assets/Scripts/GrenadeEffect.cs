using UnityEngine;
using System.Collections;

public class GrenadeEffect : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerStay(Collider other){

		if(other.tag == "Enemy" || other.tag == "EnemyRanged")
			other.transform.position = transform.position;





	}
}