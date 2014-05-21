using UnityEngine;
using System.Collections;

//Behavior that chases after the player and shoots at them
public class AB_TargetAll : AiBehavior {
	
	public CanMove moveScript { get; protected set;}
	public CanShoot shootScript { get; protected set;}
	public NavMeshAgent meshScript { get; protected set;}
	
	public float AtkRange;
	
	public Rigidbody CurrTarget;
	
	public float TargetSearchRadius;
	
	//Given that there are buildings and the player in range,
	//always attack the player if they're within PlayerPriorityRange
	public float PriorityRange;
	private IsEnemy enemyScript;
	
	override public void OnBehaviorEnter(){
		//Debug.Log("Entering chase");
		moveScript = GetComponent<CanMove>();
		shootScript = GetComponent<CanShoot>();
		meshScript = GetComponent<NavMeshAgent>();
		enemyScript = GetComponent<IsEnemy>();
		
		CurrTarget = FindTarget(TargetSearchRadius);
	}
	
	override public void OnBehaviorExit(){return;}
	
	
	override public void FixedUpdateAB(){
		if(CurrTarget != null){
			float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
			if(distToTarget > AtkRange){
				//transform.LookAt(CurrTarget.transform);
				Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
				transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
				moveScript.Move(CurrTarget.position - rigidbody.position);
				//meshScript.SetDestination(CurrTarget.position);
				if(enemyScript.enemyType == EnemyType.luminotoad)
					animation.CrossFade("Luminotoad_Hop");
				
				if(enemyScript.enemyType == EnemyType.alpha_wolf)
					animation.CrossFade("WolfRunCycle");
				
				if(enemyScript.enemyType == EnemyType.wolf)
					animation.CrossFade("WolfRunCycle");
				
				if(enemyScript.enemyType == EnemyType.zingbat)
					animation.CrossFade("ZingBatGlide");
				if(enemyScript.enemyType == EnemyType.luminosaur)
					animation.CrossFade("LuminosaurWalk");


				/*
				if(this.tag == "Enemy")
					animation.CrossFade("WolfRunCycle");
				
				if(this.tag == "EnemyRanged")
					animation.CrossFade("bat_fly");
				*/
			}
		}
	}
	
	
	override public void UpdateAB(){
		CurrTarget = FindTarget(TargetSearchRadius);
		if(CurrTarget != null){
			float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
			if (distToTarget < AtkRange){
				Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
				transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
			}
		}
	}
	
	//Not implemented
	public Rigidbody FindNearestTarget(float searchRadius){
		//return GameManager.Player.rigidbody;
		Collider[] hitColliders = Physics.OverlapSphere(rigidbody.position, searchRadius);
		Rigidbody closestTarget = null;
		float closestTargetDist = Mathf.Infinity;
		for (int i=0; i < hitColliders.Length; i++) {
			if( hitColliders[i].gameObject.tag == "Player" ||
				hitColliders[i].gameObject.tag == "Enemy" ||
				hitColliders[i].gameObject.tag == "EnemyRanged"){
				float distToAi = Vector3.Distance(rigidbody.position, hitColliders[i].rigidbody.position);
				if (distToAi < closestTargetDist){
					closestTarget = hitColliders[i].rigidbody;
					closestTargetDist = distToAi;
				}
			}
		}
		return closestTarget;
	}
	
	public Rigidbody FindTarget(float range){
		Rigidbody closestTarget = FindNearestTarget(TargetSearchRadius);
		return closestTarget;
	}
	
	
}
