using UnityEngine;
using System.Collections;

public class GrenadeEffect : MonoBehaviour {
	public float pushForce;
	public ForceMode pushForceMode = ForceMode.Impulse;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Enemy" || other.tag == "EnemyRanged"){
			Killable killScript = other.GetComponent<Killable>();
			if(killScript) killScript.damage(20);
		}
	}


	void OnTriggerStay(Collider other){

		CanMove moveScript = other.GetComponent<CanMove>();
		Killable killScript = other.GetComponent<Killable>();

		if(killScript != null && moveScript != null){
			if(other.tag == "Enemy" || other.tag == "EnemyRanged"){

				other.transform.position = transform.position;
			}
		}
	}
}