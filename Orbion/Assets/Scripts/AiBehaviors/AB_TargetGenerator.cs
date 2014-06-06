using UnityEngine;
using System.Collections;


//This is a very basic AI controller that just runs one behavior.

//Derive more advanced controllers from this class.

//AiControllers should contain logic to manage behaviors

//Priority - Player in range -> generator -> other buildings
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

	private int indexCounter = 1;
	
	//Given that there are buildings and the player in range,
	//always attack the player if they're within PlayerPriorityRange
	public float PlayerPriorityRange;
	
	protected virtual void Start () {
		//Debug.Log("START OF BEHAVIOR");
		moveScript = GetComponent<CanMove>();
		shootScript = GetComponent<CanShoot>();
		meshScript = GetComponent<NavMeshAgent>();
		enemyScript = GetComponent<IsEnemy>();
		meshPath = new NavMeshPath();

		CurrTarget = FindTarget(TargetSearchRadius);	
		if(CurrTarget != null && meshScript != null){
			meshScript.CalculatePath(CurrTarget.position, meshPath);
			indexCounter = 1;
			//showPinkCubes();
		}		

	}
	
	protected virtual void FixedUpdate () {
		if(CurrTarget != null){
			float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
			if(distToTarget > AtkRange){
					

				if(meshPath.corners.Length < 3){
					Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
					transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
					moveScript.Move(CurrTarget.position - rigidbody.position);
				} else if(meshPath.corners.Length >= 3){

					if(distanceToTarget(meshPath.corners[indexCounter]) < 0.5F && indexCounter < meshPath.corners.Length-1){
						indexCounter++;
					}
					if(indexCounter < meshPath.corners.Length){
						Vector3 lookPosition = new Vector3(meshPath.corners[indexCounter].x, transform.position.y, meshPath.corners[1].z);
						transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
						moveScript.Move(meshPath.corners[indexCounter] - rigidbody.position);
					} else {
						Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
						transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
						moveScript.Move(CurrTarget.position - rigidbody.position);
					}				
				}
				
				//animation
				if(enemyScript.enemyType == EnemyType.luminotoad)
					animation.CrossFade("Luminotoad_Hop");
				
				if(enemyScript.enemyType == EnemyType.alpha_wolf){
					if(!animation.IsPlaying("WolfHowl"))
					animation.CrossFade("WolfRunCycle");
				}
				
				if(enemyScript.enemyType == EnemyType.wolf)
					animation.CrossFade("WolfRunCycle");
				
				if(enemyScript.enemyType == EnemyType.zingbat)
					animation.CrossFade("ZingBatGlide");
				if(enemyScript.enemyType == EnemyType.luminosaur)
					animation.CrossFade("LuminosaurWalk");

			}
		}
	}
	
	
	protected virtual void Update () {
		//Timer to get a new target
		if(targetCheckCounter > targetCheckTimer){
			CurrTarget = FindTarget(TargetSearchRadius);
			if(CurrTarget != null && meshScript != null){
				meshScript.CalculatePath(CurrTarget.position, meshPath);
				indexCounter = 1;
				//showPinkCubes();
			}
			targetCheckCounter = 0.0F;
		} else {
			targetCheckCounter += Time.deltaTime;
		}
		//Attack target if in range and not on cooldown
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
						if(rand > 0.0F && rand <= 0.2F){
							this.GetComponent<CanShoot>().stun();
						}
					}
					if(enemyScript.enemyType == EnemyType.alpha_wolf){
						animation.CrossFade("WolfAttack");
						if(rand > 0.0F && rand <= 0.2F){
							this.GetComponent<CanShoot>().stun();
						}
					}
					
					if(enemyScript.enemyType == EnemyType.wolf)
						animation.CrossFade("WolfAttack");
					
					if(enemyScript.enemyType == EnemyType.zingbat)
						animation.CrossFade("ZingBatAttack");

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
	
	
	//Returns nearest prefab that contains Buildable script
	public Rigidbody FindNearestBuilding(float searchRadius){
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
	
	//Test navmesh corner locations
	private void showPinkCubes(){
		int i = 1;
		if(meshPath != null){

			//Debug.Log (meshPath.corners.Length);

			while (i < meshPath.corners.Length) {
				Vector3 currentCorner = meshPath.corners[i];
				//Debug.Log (meshPath.corners[i]);
				clone = Instantiate (pinkBox, currentCorner, Quaternion.identity) as Rigidbody;
				i++;
			}
		}
	}

	private float distanceToTarget(Vector3 vec){
	
		float deltaX = this.transform.position.x - vec.x;
		float deltaZ = this.transform.position.z - vec.z;
		
		return Mathf.Sqrt(deltaX * deltaX + deltaZ * deltaZ);

	}
	
	//Finds a target within the range
	//
	//If the player is in the PlayerPriorityRange, returns the player
	//otherwise, the target is the closest generator
	//Returns null if nothing is in range
	public Rigidbody FindTarget(float range){
		//If player is within the priority range, no need to search further
		Rigidbody player = GetPlayerInRange( range);
		if(player !=null && PlayerInRange( PlayerPriorityRange)){
		 	return FindPlayer();
		}
		//Check for closest generator
		//GameObject GObuilding = Utility.GetClosestWith(transform.position, Mathf.Infinity, Utility.GoHasComponent<IsGenerator>);
		GameObject GObuilding = Utility.GetClosestWith(transform.position, Mathf.Infinity, Utility.GoHasComponent<Buildable>);
		
		Rigidbody building;
		//if no generator return player
		if(GObuilding != null){ 
			building = GObuilding.rigidbody;
			return building;
		} else {
			return FindPlayer();			
		}	
	}	
	
}
