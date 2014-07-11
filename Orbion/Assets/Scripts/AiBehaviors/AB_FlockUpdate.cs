using UnityEngine;
using System.Collections;


//work in progress
public class AB_FlockUpdate : MonoBehaviour {
	
	//Scripts needed
	public CanMove moveScript { get; protected set;}
	public CanShoot shootScript { get; protected set;}
	public NavMeshAgent meshScript { get; protected set;}
	private IsEnemy enemyScript;
	
	//Rigidbodies	
	private Rigidbody clone;//spawning prefabs
	public Rigidbody pinkBox;//debug
	public Rigidbody CurrTarget;//enemy's current target
	
	public float AtkRange;//enemy attack range
	public float PlayerPriorityRange;//Attack player over buildings
	private float TargetSearchRadius = Mathf.Infinity;//distance to search for a target
	
	//old school timer - when to check for new target
	private float targetCheckTimer = 1.0F;
	private float targetCheckCounter = 0.0F;
	
	//meshpath variables
	private int indexCounter = 1;//index used for corners array
	private NavMeshPath meshPath;//used to calculate path between enemy and target
	
	
	//loads scripts, finds target, calculates path to target
	protected virtual void Start () {
		//Debug.Log("START OF BEHAVIOR");
		//loading scripts
		moveScript = GetComponent<CanMove>();
		shootScript = GetComponent<CanShoot>();
		meshScript = GetComponent<NavMeshAgent>();
		enemyScript = GetComponent<IsEnemy>();
		
		meshPath = new NavMeshPath();//used to calculate path between enemy and target
		calculatePath();
		
	}
	
	
	protected virtual void FixedUpdate () {
		if(CurrTarget != null){
			float distToTarget = distanceToCurrentTarget();
			
			//move towards target if its outside the attack range
			if(distToTarget > AtkRange){
				//Debug.Log ("outside attack range");
			

				//no midpoints to traverse and close to player go straight to them
				if(meshPath.corners.Length < 3 && distanceToCurrentTarget() <= 10.5f){
					Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
					transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
					moveScript.Move(CurrTarget.position - rigidbody.position);

					//Debug.Log ("close to player");
				} else {
					
					//increment index in corner array to move to next location
					if(indexCounter < meshPath.corners.Length-1){
						if(distanceToTarget(meshPath.corners[indexCounter]) < 0.5F && indexCounter < meshPath.corners.Length-2){
							indexCounter++;
						}
					}
					//Debug.Log ("far from player or there is an obstacle to navigate");
						
					
					if(indexCounter < meshPath.corners.Length){
						Vector3 lookPosition = new Vector3(meshPath.corners[indexCounter].x, transform.position.y, meshPath.corners[1].z);
						transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
						moveScript.Move(meshPath.corners[indexCounter] - rigidbody.position);
					
					} 
					/*else {
						Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
						transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
						moveScript.Move(CurrTarget.position - rigidbody.position);
					}*/				
				
				//movement animation
					if(enemyScript.enemyType == EnemyType.luminotoad){
						animation.CrossFade("Luminotoad_Hop");
					} else if(enemyScript.enemyType == EnemyType.alpha_wolf){
						if(!animation.IsPlaying("WolfHowl")){
							animation.CrossFade("WolfRunCycle");
						}
					} else if(enemyScript.enemyType == EnemyType.wolf){
						animation.CrossFade("WolfRunCycle");
					} else if(enemyScript.enemyType == EnemyType.zingbat){
						animation.CrossFade("ZingBatGlide");
					} else if(enemyScript.enemyType == EnemyType.luminosaur){
						animation.CrossFade("LuminosaurWalk");
					}
				
				}
			}
		}
	}
	
	
	protected virtual void Update () {
		//Timer to get a new target
		if(targetCheckCounter > targetCheckTimer){
			calculatePath();
			targetCheckCounter = 0.0F;
		} else {
			targetCheckCounter += Time.deltaTime;
		}
		
		//--Attack target if in range and not on cooldown--//
		if(CurrTarget != null){
			float distToTarget = distanceToCurrentTarget();
			if (distToTarget <= AtkRange){
				//Debug.Log ("inside attack range");
				
				float rand = Random.value;
				
				//--enemy attack and animation--//
				if( shootScript.FinishCooldown()){
					if(enemyScript.enemyType == EnemyType.luminotoad){
						animation.CrossFade("Luminotoad_Bomb");
						this.GetComponent<Killable>().explode();
					} else if(enemyScript.enemyType == EnemyType.luminosaur){
						animation.CrossFade("LuminosaurChomp");
						if(rand > 0.0F && rand <= 0.2F){
							this.GetComponent<CanShoot>().stun();
						}
					} else if(enemyScript.enemyType == EnemyType.alpha_wolf){
						animation.CrossFade("WolfAttack");
						if(rand > 0.0F && rand <= 0.2F){
							this.GetComponent<CanShoot>().stun();
						}
					} else if(enemyScript.enemyType == EnemyType.wolf){
						animation.CrossFade("WolfAttack");
					} else if(enemyScript.enemyType == EnemyType.zingbat){
						animation.CrossFade("ZingBatAttack");
					}
					//--end of--enemy attack and animation--//
					
				} else {
					//movement animation
					//switch order of units
					if( animation.isPlaying == false){
						if(enemyScript.enemyType == EnemyType.luminosaur){
							animation.CrossFade("LuminosaurWalk");	
						} else if(enemyScript.enemyType == EnemyType.luminotoad){
							animation.CrossFade("Luminotoad_Hop");
						} else if(enemyScript.enemyType == EnemyType.alpha_wolf){
							animation.CrossFade("WolfRunCycle");
						} else if(enemyScript.enemyType == EnemyType.wolf){
							animation.CrossFade("WolfRunCycle");
						} else if(enemyScript.enemyType == EnemyType.zingbat){
							animation.CrossFade("ZingBatGlide");
						}
					}
				}
				
				//enemy rotation to look at target
				Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
				transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
				if(enemyScript.enemyType != EnemyType.luminotoad)
					shootScript.Shoot(lookPosition);
			}
		}
	}
	
	public Rigidbody FindPlayerRigidBody(){
		return GameManager.Player.rigidbody;
	}
	
	
	public bool PlayerInRange(float range){
		float distToTarget = Vector3.Distance( FindPlayerRigidBody().position, rigidbody.position);
		return (distToTarget <= range); 
	}
	
	
	//returns the player if they are in range, else returns null
	public Rigidbody GetPlayerInRange(float range){
		if (PlayerInRange(range)) return FindPlayerRigidBody();
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
	
	//Returns nearest prefab that contains Buildable script
	
	private void calculatePath(){
		//Debug.Log ("Calculate Path");
		CurrTarget = FindTarget(TargetSearchRadius);
		//calculate path between enemy and current target
		if(CurrTarget != null && meshScript != null){
			//meshScript.CalculatePath(CurrTarget.transform.position, meshPath);
			//showPinkCubes();


			indexCounter = 1;
			Collider[] hitColliders = Physics.OverlapSphere(rigidbody.position, 20.0f);
			Rigidbody enemy = null;
			float offsetX = 0.0f; float offsetZ = 0.0f; int enemyCounter = 0;
			float enemyDist = Mathf.Infinity;
			for (int i=0; i < hitColliders.Length; i++) {
				if( hitColliders[i].GetComponent<IsEnemy>() == null) continue;
				float tempX = hitColliders[i].rigidbody.position.x - transform.position.x;
				float tempZ = hitColliders[i].rigidbody.position.z - transform.position.z;
				if(tempX > 0) {
					offsetX += 20 - tempX;
				} else {
					offsetX -= 20 + tempX;
				}
				if(tempZ > 0) {
					offsetZ += 20 - tempZ;
				} else {
					offsetZ -= 20 + tempZ;
				}
				enemyCounter++;
				//Debug.Log("ENEMY IS NEAR");
			}
			if(enemyCounter != 0){
				offsetX /= enemyCounter;
				offsetZ /= enemyCounter;
				
				
				offsetX *= -1;
				offsetZ *= -1;
				
				//Normalize
				//float length = Mathf.Sqrt(offsetX * offsetX + offsetZ * offsetZ);				
				//offsetX /= length;
				//offsetZ /= length;				
				//offsetX *= 5;
				//offsetZ *= 5;
				
				//Debug.Log("offset x " + offsetX);
				//Debug.Log("offset z " + offsetZ);
				
				Vector3 bootyButtCheeks;
				bootyButtCheeks.x = CurrTarget.transform.position.x + offsetX;
				bootyButtCheeks.y = 0.0f;
				bootyButtCheeks.z = CurrTarget.transform.position.z + offsetZ;
				
				//Debug.Log ("BOOTY BUTT CHEEKS " + bootyButtCheeks.x + " " + CurrTarget.position.x);	
				
				meshScript.CalculatePath(bootyButtCheeks, meshPath);
				showPinkCubes();
			} else {
				meshScript.CalculatePath(CurrTarget.transform.position, meshPath);
				showPinkCubes();
			}
			
			
		}
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
	//check null before calling
	private float distanceToCurrentTarget(){
		float deltaX = this.transform.position.x - CurrTarget.transform.position.x;
		float deltaZ = this.transform.position.z - CurrTarget.transform.position.z;
		return Mathf.Sqrt(deltaX * deltaX + deltaZ * deltaZ);
	}
	
	//no idea yet
	public Rigidbody FindTarget(float range){
		
		/*
		GameObject GObuilding = Utility.GetClosestWith(transform.position, Mathf.Infinity, Utility.GoHasComponent<Buildable>);
		Rigidbody building;
		if(GObuilding != null){ 
			building = GObuilding.rigidbody;
		} else {
			return FindPlayerRigidBody();
		}
		if(GameManager.Player == null){
			return building;
		}

		if(distanceToTarget(GObuilding.transform.position) <= (PlayerPriorityRange / 2) 
		&& distanceToTarget(GameManager.Player.gameObject.transform.position) > (PlayerPriorityRange / 2)){
			return building;
		}
		if(distanceToTarget(GameManager.Player.gameObject.transform.position) > PlayerPriorityRange){
			return building;
		} else {
			return FindPlayerRigidBody();
		}	
		*/
		return FindPlayerRigidBody();
		
	}	
	
}
