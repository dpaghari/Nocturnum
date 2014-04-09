using UnityEngine;
using System.Collections;


//This is a very basic AI controller that just runs one behavior.

//Derive more advanced controllers from this class.

//AiControllers should contain logic to manage behaviors
public class AB_GeneratorTarget : MonoBehaviour {
	
	public CanMove moveScript { get; protected set;}
	public CanShoot shootScript { get; protected set;}
	public NavMeshAgent meshScript { get; protected set;}
	
	public float AtkRange;
	
	public Rigidbody CurrTarget;
	
	private float TargetSearchRadius = Mathf.Infinity;
	
	//Given that there are buildings and the player in range,
	//always attack the player if they're within PlayerPriorityRange
	public float PlayerPriorityRange;
	
	protected virtual void Start () {
		moveScript = GetComponent<CanMove>();
		shootScript = GetComponent<CanShoot>();
		meshScript = GetComponent<NavMeshAgent>();
		
		CurrTarget = FindTarget(TargetSearchRadius);
	}
	
	
	protected virtual void FixedUpdate () {
		if(CurrTarget != null){
			float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
			if(distToTarget > AtkRange){
				//transform.LookAt(CurrTarget.transform);
				Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
				transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
				moveScript.Move(CurrTarget.position - rigidbody.position);
				//meshScript.SetDestination(CurrTarget.position);
				
				if(this.tag == "Enemy")
					animation.CrossFade("WolfRunCycle");
				
				if(this.tag == "EnemyRanged")
					animation.CrossFade("bat_fly");
			}
		}
	}
	
	
	
	protected virtual void Update () {
		CurrTarget = FindTarget(TargetSearchRadius);
		if(CurrTarget != null){
			float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
			if (distToTarget < AtkRange){
				if(this.tag == "Enemy"){
					animation.CrossFade("WolfAttack");
				}
				Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
				transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
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
		//return GameManager.Player.rigidbody;
		Collider[] hitColliders = Physics.OverlapSphere(rigidbody.position, searchRadius);
		Rigidbody closestBuilding = null;
		float closestBuildingDist = Mathf.Infinity;
		for (int i=0; i < hitColliders.Length; i++) {
			if( hitColliders[i].GetComponent<Buildable>() == null) continue;
			
			float distToAi = Vector3.Distance(rigidbody.position, hitColliders[i].rigidbody.position);
			if (distToAi < closestBuildingDist){
				closestBuilding = hitColliders[i].rigidbody;
				closestBuildingDist = distToAi;
			}
		}
		return closestBuilding;
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
		
		
		//Rigidbody building = FindNearestBuilding( range);
		//Check for closest generator
		GameObject GObuilding = Utility.GetClosestWith(transform.position, Mathf.Infinity, Utility.GoHasComponent<IsGenerator>);
		Rigidbody building;
		//if no generator check for closest building
		if(GObuilding != null){ 
			building = GObuilding.rigidbody;
			return building;
		} else {
			building = FindNearestBuilding (range);
			return building;		
		}
		
		//If we can't find a player or building, return the other
		//If we can't find either, this returns null
		if(player == null) return building;
		if(building == null) return player;

		//Debug.Log("wtf");
		return FindPlayer();
		
		
		//If we have both a player(not withing the priority range) and a building,
		//target the closest one. If they are the same distance, target player.
		Rigidbody closestTarget = player;
		float distToPlayer = Vector3.Distance(rigidbody.position, player.position);
		float distToBuilding = Vector3.Distance(rigidbody.position, building.position);
		if(distToBuilding < distToPlayer) closestTarget = building;
		return closestTarget;
	}
	
	
}
