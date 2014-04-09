using UnityEngine;
using System.Collections;

public class GrenadeEffect : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
	Killable killScript = other.GetComponent<Killable>();
		if(killScript) killScript.damage(30);

	}


	void OnTriggerStay(Collider other){

		if(other.tag == "Enemy" || other.tag == "EnemyRanged"){
			other.transform.position = transform.position;


		}
	}
}