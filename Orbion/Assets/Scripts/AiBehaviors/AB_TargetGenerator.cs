using UnityEngine;
using System.Collections;


//This is a very basic AI controller that just runs one behavior.

//Derive more advanced controllers from this class.

//AiControllers should contain logic to manage behaviors
public class AB_TargetGenerator : MonoBehaviour {
	
	public CanMove moveScript { get; protected set;}
	public CanShoot shootScript { get; protected set;}
	public NavMeshAgent meshScript { get; protected set;}
	
	private NavMeshPath meshPath;
	private Rigidbody clone;
	public Rigidbody pinkBox;
	

	public float AtkRange;
	
	public Rigidbody CurrTarget;
	
	private float TargetSearchRadius = Mathf.Infinity;

	private IsEnemy enemyScript;
	private float targetCheckTimer = 1.0F;
	private float targetCheckCounter = 0.0F;
	private int counter = 1;
	private int cornerIndex = 1;
	
	//Given that there are buildings and the player in range,
	//always attack the player if they're within PlayerPriorityRange
	public float PlayerPriorityRange;
	
	protected virtual void Start () {
		moveScript = GetComponent<CanMove>();
		shootScript = GetComponent<CanShoot>();
		meshScript = GetComponent<NavMeshAgent>();
		enemyScript = GetComponent<IsEnemy>();

		meshPath = new NavMeshPath();
		//Debug.Log("START OF BEHAVIOR");
		CurrTarget = FindTarget(TargetSearchRadius);
		//Debug.Log("Get target #" + counter); counter++;
		
		//meshScript.SetDestination(CurrTarget.position);
		
		if(CurrTarget != null && meshScript != null){
			meshScript.CalculatePath(CurrTarget.position, meshPath);
		}
		/*
		int i = 1;
		if(meshPath != null){
			while (i < meshPath.corners.Length) {
				Vector3 currentCorner = meshPath.corners[i];
				Debug.Log (meshPath.corners[i]);
				clone = Instantiate (pinkBox, currentCorner, Quaternion.identity) as Rigidbody;
				i++;
			}
		}
		*/
		

	}
	
	protected virtual void FixedUpdate () {
		if(CurrTarget != null){
			float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
			if(distToTarget > AtkRange){
				//transform.LookAt(CurrTarget.transform);
				//Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
				//transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);

				if(meshPath.corners.Length > 0){
					//closeToCorner(meshPath.corners[cornerIndex]);
					//moveScript.Move(meshPath.corners[cornerIndex]);
				}

				//Debug.Log("# corners " + meshPath.corners.Length);
					

				if(meshPath.corners.Length < 3){
					Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
					transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
					moveScript.Move(CurrTarget.position - rigidbody.position);
					//Debug.Log("Speed " + this.GetComponent<CanMove>().getForce());
					//Debug.Log("FUCK");
				} else if(meshPath.corners.Length >= 3){
					Vector3 lookPosition = new Vector3(meshPath.corners[1].x, transform.position.y, meshPath.corners[1].z);
					transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
					//closeToCorner(meshPath.corners[cornerIndex]);
					//moveScript.Move(meshPath.corners[cornerIndex] - rigidbody.position);
					moveScript.Move(meshPath.corners[1] - rigidbody.position);
					//Debug.Log("Speed " + this.GetComponent<CanMove>().getForce());
					
				}
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

			}
		}
	}
	
	private void closeToCorner(Vector3 vec){
		float distance = Vector3.Distance (vec, this.transform.position);
		if(distance < 1){
			cornerIndex++;
		}
	}
	
	protected virtual void Update () {
		if(targetCheckCounter > targetCheckTimer){
			//Debug.Log("Get target #" + counter); counter++;
			CurrTarget = FindTarget(TargetSearchRadius);
			if(CurrTarget != null){
				meshScript.CalculatePath(CurrTarget.position, meshPath);
				cornerIndex = 1;
			}
			/*
			int i = 1;
			if(meshPath != null){
				while (i < meshPath.corners.Length) {
					Vector3 currentCorner = meshPath.corners[i];
					Debug.Log (meshPath.corners[i]);
					clone = Instantiate (pinkBox, currentCorner, Quaternion.identity) as Rigidbody;
					i++;
				}
			}
			*/
			//meshScript.SetDestination(CurrTarget.position);
			targetCheckCounter = 0.0F;
		} else {
			targetCheckCounter += Time.deltaTime;
		}
		if(CurrTarget != null){
			float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
			if (distToTarget <= AtkRange){
				float rand = Random.value;

				if( shootScript.FinishCooldown()){
					if(enemyScript.enemyType == EnemyType.luminotoad){
						animation.CrossFade("Luminotoad_Bomb");
						this.GetComponent<Killable>().explode();
					}
					if(enemyScript.enemyType == EnemyType.luminosaur){
						animation.CrossFade("LuminosaurChomp");
						if(rand > 0.0F && rand <= 1.0F){
							//this.GetComponent<CanShoot>().stun();
						}
					}
					if(enemyScript.enemyType == EnemyType.alpha_wolf){
						animation.CrossFade("WolfAttack");
						if(rand > 0.0F && rand <= 1.0F){
							//this.GetComponent<CanShoot>().stun();
						}
					}
					
					if(enemyScript.enemyType == EnemyType.wolf)
						animation.CrossFade("WolfAttack");
					
					if(enemyScript.enemyType == EnemyType.zingbat)
						animation.CrossFade("ZingBatAttack");


					/*
					if(this.tag == "Enemy")
						animation.CrossFade("WolfAttack");
					if(this.tag == "EnemyRanged")
						animation.CrossFade("ZingBatAttack");
					*/
				}
				
				else{

					if( animation.isPlaying == false){
						if(enemyScript.enemyType == EnemyType.luminosaur)
							animation.CrossFade("LuminosaurWalk");	
						if(enemyScript.enemyType == EnemyType.luminotoad)
							animation.CrossFade("Luminotoad_Hop");
						
						if(enemyScript.enemyType == EnemyType.alpha_wolf)
							animation.CrossFade("WolfRunCycle");
						
						if(enemyScript.enemyType == EnemyType.wolf)
							animation.CrossFade("WolfRunCycle");
						
						if(enemyScript.enemyType == EnemyType.zingbat)
							animation.CrossFade("ZingBatGlide");
					}

					/*
					if(this.tag == "Enemy")
						animation.CrossFade("WolfRunCycle");
					if(this.tag == "EnemyRanged")
						animation.CrossFade("ZingBatGlide");
					*/

				}
				

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
