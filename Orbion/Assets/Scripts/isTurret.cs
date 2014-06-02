using UnityEngine;
using System.Collections;

public class isTurret : MonoBehaviour {
	
			
	public GameObject TurretRing;
	public CanShoot shootScript;
	public Rigidbody target;

	void Start () {
		ResManager.AddTurr(1);	
	}
			

	void Update () {
		if(target != null && shootScript.FinishCooldown() )
			shootScript.ShootTarget(target.gameObject);
	}
	void FixedUpdate(){
		TurretRing.transform.Rotate(new Vector3(0, 0, 1));

	}

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
	void OnTriggerExit(Collider other){
		if(other.rigidbody == target) target = null;
	}

}