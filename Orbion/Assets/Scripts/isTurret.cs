//Purpose: Turret targetting and shooting

using UnityEngine;
using System.Collections;

public class isTurret : MonoBehaviour {

	
			
	public GameObject TurretRing;


	public Rigidbody target;

	private CanShoot shootScript;

	void Start () {
		shootScript = GetComponent<CanShoot>();
		ResManager.AddTurr(1);	
	}
			

	void Update () {
		if(target != null && shootScript.FinishCooldown() )
			shootScript.ShootTarget(target.gameObject);
	}
	void FixedUpdate(){
		TurretRing.transform.Rotate(new Vector3(0, 0, 1));

	}

	//Only switches target if its curret target is out of range / dead
	//and a new target is withing range
	void UpdateTarget(Collider potentialTarget) {
		if( target != null) return;

		IsEnemy enemyScript = potentialTarget.GetComponent<IsEnemy>();
		if( enemyScript != null && enemyScript.enemyType != EnemyType.none)
			target = potentialTarget.rigidbody;
	}


	void OnTriggerEnter(Collider other){
		UpdateTarget( other);			
	}
	void OnTriggerStay(Collider other){
		UpdateTarget( other);	
	}

	//Removing target when out of range, otherwise it could have infinite range
	void OnTriggerExit(Collider other){
		if(other.rigidbody == target) target = null;
	}

}