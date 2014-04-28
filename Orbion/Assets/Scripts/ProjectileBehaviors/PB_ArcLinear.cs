using UnityEngine;
using System.Collections;


[RequireComponent (typeof (ProjectileController))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
//[RequireComponent (typeof (CanMove))]




public class PB_ArcLinear : ProjectileBehavior {
	
	public ForceMode MoveType;
	//public float MoveForce;
	public CanMove MoveScript;
	
	
	public int Damage;
	public int searingLevel;
	//public int homingLevel;
	//public int ricochetLevel;
	public GameObject dot;
	public GameObject hitEffect;
	private GameObject clone;
	
	private GameObject lastHitTarget;
	
	public GameObject target;
	
	public float lifeTime = 2.0F;
	private float lifeCounter = 0.0F;
	private Vector3 targetPos;
	
	public void Update(){
		if(lifeCounter > lifeTime){
			Destroy(this.gameObject);
		} else {
			lifeCounter += Time.deltaTime;
		}
	}


	//super hard coded initial velocity, should actully use physics equation later
	public override void Initialize(){
		targetPos = Utility.GetMouseWorldPos();
		Vector3 distToTarget = targetPos - transform.position;
		distToTarget.y = 0;
		MoveScript.Move( transform.forward * distToTarget.magnitude/15f, ForceMode.VelocityChange);
		return;
	}
	
	public override void FixedPerform(){
		//MoveScript.Move(transform.forward, MoveType);
		
	}
	
	public override void Perform(){ return;}
	
	
	public bool IsEnemy(GameObject enemy){
		if(enemy.GetComponent<IsEnemy>() == null) return false;
		if (enemy == lastHitTarget)
			return false;
		
		return true;
	}
	
	
	public override void OnImpactEnter( Collision other){
		Killable KillScript = other.gameObject.GetComponent<Killable>();
		if( KillScript) {
			if(other.gameObject.GetComponent<Buildable>() == null ){
				KillScript.damage(Damage);
				clone = Instantiate(hitEffect, transform.position, new Quaternion()) as GameObject;
			}
		}
		

		
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyRanged") {
			target = GameObject.Find("player_prefab");
			target.GetComponent<hasOverdrive>().overdriveCount += 1.0f;
			//ebug.Log(target.GetComponent<hasOverdrive>().overdriveCount);
		}

	}
	
	public override void OnImpactStay( Collision other){ return;}
	
	public override void OnImpactExit( Collision other){ return;}
	
}
