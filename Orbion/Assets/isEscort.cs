using UnityEngine;
using System.Collections;


//This is a very basic AI controller that just runs one behavior.

//Derive more advanced controllers from this class.

//AiControllers should contain logic to manage behaviors
public class isEscort : MonoBehaviour {
	
	public CanMove moveScript { get; protected set;}
	
	public Rigidbody CurrTarget;

	public float AtkRange;

	
	private float TargetSearchRadius = Mathf.Infinity;
	
	//private IsEnemy enemyScript;
	private float targetCheckTimer = 1.0F;
	private float targetCheckCounter = 0.0F;
	public float PlayerPriorityRange;
	
	//Given that there are buildings and the player in range,
	//always attack the player if they're within PlayerPriorityRange
	//public float PlayerPriorityRange;
	
	protected virtual void Start () {
		moveScript = GetComponent<CanMove>();
	
		//CurrTarget = FindTarget(TargetSearchRadius);
	}


	void OnTriggerEnter(Collider other){
		if(other.tag == "Player")
			TechManager.foundSC = true;
		if(other.GetComponent<isLunaShip>() != null){
			TechManager.transportedSC = true;
			other.collider.enabled = false;

		}
	}

	void OnTriggerStay(Collider other){

		if(other.tag == "Player"){
			if(CurrTarget != null){
				float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
				if(distToTarget > AtkRange){
					Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
					transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
					moveScript.Move(CurrTarget.position - rigidbody.position);
				}
			}
		}

	}
	
	
	protected virtual void FixedUpdate () {
	
	}
	
	
	
	protected virtual void Update () {
		if(targetCheckCounter > targetCheckTimer){
			//CurrTarget = FindTarget(TargetSearchRadius);
			//meshScript.SetDestination(CurrTarget.position);
			targetCheckCounter = 0.0F;
		} else {
			targetCheckCounter += Time.deltaTime;
		}
		if(CurrTarget != null){
				float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);

				
				
				Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
				transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);

			
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
		//Debug.Log("FINDTARGET");
		
		//If player is within the priority range, no need to search further
		Rigidbody player = GetPlayerInRange( range);
		if(player !=null && PlayerInRange( PlayerPriorityRange)){
			//Debug.Log("return player priority");
			return FindPlayer();
		}
		
		
		//Rigidbody building = FindNearestBuilding( range);
		//Check for closest generator
		GameObject GObuilding = Utility.GetClosestWith(transform.position, Mathf.Infinity, Utility.GoHasComponent<IsGenerator>);
		Rigidbody building;
		//if no generator check for closest building
		if(GObuilding != null){ 
			building = GObuilding.rigidbody;
			//Debug.Log("return generator");
			return building;
		} else {
			//building = FindNearestBuilding (range);
			//Debug.Log("return nearest building");
			//return building;
			//Debug.Log("return player");
			return FindPlayer();		
		}
		
		//If we can't find a player or building, return the other
		//If we can't find either, this returns null
		
		/*
		if(player == null){ 
			Debug.Log("return building 3");
			return building;
		}
		if(building == null){
			Debug.Log("return player 2");
		 return player;
		}

		return FindPlayer();
		
		
		//If we have both a player(not withing the priority range) and a building,
		//target the closest one. If they are the same distance, target player.
		Rigidbody closestTarget = player;
		float distToPlayer = Vector3.Distance(rigidbody.position, player.position);
		float distToBuilding = Vector3.Distance(rigidbody.position, building.position);
		if(distToBuilding < distToPlayer) closestTarget = building;
		return closestTarget;
		*/
	}
	
	
}
