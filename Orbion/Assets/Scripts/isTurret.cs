using UnityEngine;
using System.Collections;

public class isTurret : MonoBehaviour {
	
			

	public CanShoot shootScript;
	public Rigidbody target;

	public float keepTargetTime = 0.75f;
	private DumbTimer keepTargetTimer;

	void Start () {
		ResManager.AddTurr(1);	
		keepTargetTimer = DumbTimer.New( keepTargetTime);
		keepTargetTimer.SetProgress(0);
	}
			

	void Update () {

		if( keepTargetTimer.Finished())
			target = null;

		if(target != null && shootScript.FinishCooldown() )
			shootScript.ShootTarget(target.gameObject);

		keepTargetTimer.Update();
	}

	void UpdateTarget(Rigidbody potentialTarget) {
		if( potentialTarget == target)
			keepTargetTimer.Reset();

		if( target != null) return;

		IsEnemy enemyScript = potentialTarget.GetComponent<IsEnemy>();
		if( enemyScript != null && enemyScript.enemyType != EnemyType.none)
			target = potentialTarget;
	}


	void OnTriggerEnter(Collider other){
		if( other.rigidbody != null) UpdateTarget( other.rigidbody);			
	}
	void OnTriggerStay(Collider other){
		if( other.rigidbody != null) UpdateTarget( other.rigidbody);	
	}
}