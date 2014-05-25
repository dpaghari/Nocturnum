using UnityEngine;
using System.Collections;

//Behavior that chases after the player and shoots at them
public class AB_TargetPlayer : AiBehavior {

	public CanMove moveScript { get; protected set;}
	public CanShoot shootScript { get; protected set;}
	public NavMeshAgent meshScript { get; protected set;}

	public float AtkRange;

	public Rigidbody CurrTarget;

	public float TargetSearchRadius;

	//Given that there are buildings and the player in range,
	//always attack the player if they're within PlayerPriorityRange
	public float PlayerPriorityRange;

	private IsEnemy enemyScript;
	private float targetCheckTimer = 1.0F;
	private float targetCheckCounter = 0.0F;


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
				//moveScript.Move (meshScript.nextPosition*-1);

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
		if(targetCheckCounter > targetCheckTimer){
			CurrTarget = FindTarget(TargetSearchRadius);
			targetCheckCounter = 0.0F;
			//meshScript.SetDestination(CurrTarget.position);
		} else {
			targetCheckCounter += Time.deltaTime;
		}
		if(CurrTarget != null){
			float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
			if (distToTarget < AtkRange){
				if(enemyScript.enemyType == EnemyType.luminotoad){
					animation.CrossFade("Luminotoad_Bomb");
					this.GetComponent<Killable>().explode();
				}
				
				if(enemyScript.enemyType == EnemyType.alpha_wolf)
					animation.CrossFade("WolfAttack");
				
				if(enemyScript.enemyType == EnemyType.wolf)
					animation.CrossFade("WolfAttack");
				
				if(enemyScript.enemyType == EnemyType.zingbat)
					animation.CrossFade("ZingBatAttack");
				if(enemyScript.enemyType == EnemyType.luminosaur)
					animation.CrossFade("ZingBatChomp");


				/*
				if(this.tag == "Enemy"){
				animation.CrossFade("WolfAttack");
				}
				*/
				Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
				transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
				if(enemyScript.enemyType != EnemyType.luminotoad)
				shootScript.Shoot(lookPosition);
			}
		}
	}
	

	public Rigidbody FindPlayer(){
		return GameManager.Player.rigidbody;
	}


	public bool PlayerInRange(float range){
		float distToTarget = Vector3.Distance( FindPlayer().position, rigidbody.position);
		return (distToTarget <= range); 
	}
	

	//returns the player if they are in range, else returns null
	public Rigidbody GetPlayerInRange(float range){
		if (PlayerInRange(range)) return FindPlayer();
		return null; 
	}


	//Not implemented
	public Rigidbody FindNearestBuilding(float searchRadius){
		GameObject closestBuilding = Utility.GetClosestWith(rigidbody.position, searchRadius, null, Utility.Building_PLM);
		if( closestBuilding != null)
			return closestBuilding.rigidbody;

		return null;
	}


	//Finds a target within the range
	//
	//If the player is in the PlayerPriorityRange, returns the player
	//otherwise, the target is the closest building/player 
	//Returns null if nothing is in range
	public Rigidbody FindTarget(float range){

		//If player is within the priority range, no need to search further
		Rigidbody player = GetPlayerInRange( range);
		
		if(player !=null && PlayerInRange( PlayerPriorityRange)) return player;
		

		Rigidbody building = FindNearestBuilding( range);
		
		//If we can't find a player or building, return the other
		//If we can't find either, this returns null
		if(player == null) return building;
		if(building == null) return player;
		

		//If we have both a player(not withing the priority range) and a building,
		//target the closest one. If they are the same distance, target player.
		Rigidbody closestTarget = player;
		float distToPlayer = Vector3.Distance(rigidbody.position, player.position);
		float distToBuilding = Vector3.Distance(rigidbody.position, building.position);
		if(distToBuilding < distToPlayer) closestTarget = building;
		return closestTarget;
	}
	
	
}
