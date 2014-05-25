using UnityEngine;
using System.Collections;

public class isTurret : MonoBehaviour {
	
			

	public CanShoot shootScript;
	public Rigidbody target;

	void Start () {
				
	}
			

	void Update () {

		if(target != null && shootScript.FinishCooldown() )
			shootScript.ShootTarget(target.gameObject);

	}

	void OnTriggerEnter(Collider other){

		if(other.GetComponent<IsEnemy>() != null){
			target = other.rigidbody;
		}
			
				
	}
	void OnTriggerStay(Collider other){
		
		if(other.GetComponent<IsEnemy>() != null){
			target = other.rigidbody;
		}

		
	}
}