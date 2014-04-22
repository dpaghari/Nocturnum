using UnityEngine;
using System.Collections;

//AiControllers should contain logic to manage behaviors
public class AB_Flock : MonoBehaviour {
	
	public CanMove moveScript { get; protected set;}
	public CanShoot shootScript { get; protected set;}
	public NavMeshAgent meshScript { get; protected set;}

	public float AtkRange;
	
	public Rigidbody CurrTarget;
	
	private float TargetSearchRadius = Mathf.Infinity;
	
	private float targetCheckTimer = 1.0F;
	private float targetCheckCounter = 0.0F;
	
	//flocking variables
	public float searchRadius = 5.0F;
	private Vector2 alignment;
	private Vector2 cohesion;
	private Vector2 separation;
	private int neighbors;
	private Vector2 vel = new Vector2(0.0F,0.0F);
	private float enemySpeed = 5.0F;

	//Given that there are buildings and the player in range,
	//always attack the player if they're within PlayerPriorityRange
	public float PlayerPriorityRange;
	
	protected virtual void Start () {
		moveScript = GetComponent<CanMove>();
		shootScript = GetComponent<CanShoot>();
		meshScript = GetComponent<NavMeshAgent>();
		CurrTarget = FindTarget(TargetSearchRadius);
	}
	
	
	public Vector2 computeAlignment(){
		Vector2 alig = new Vector2(0.0F,0.0F); neighbors = 0;

		Collider[] hitColliders = Physics.OverlapSphere(rigidbody.position, searchRadius);
		//float closestEnemyDist = Mathf.Infinity;
		for (int i=0; i < hitColliders.Length; i++) {
			if( hitColliders[i].GetComponent<AB_Flock>() == null) continue;
			alig.x += hitColliders[i].rigidbody.velocity.x;
			alig.y += hitColliders[i].rigidbody.velocity.z;
			neighbors++;			
		}
		if (neighbors == 0) return alig;
		alig.x /= neighbors;
		alig.y /= neighbors;
		alig.Normalize();
		return alig;
	}
	public Vector2 computeCohesion(){
		Vector2 cohes = new Vector2(0.0F,0.0F); neighbors = 0;

		Collider[] hitColliders = Physics.OverlapSphere(rigidbody.position, searchRadius);
		//float closestEnemyDist = Mathf.Infinity;
		for (int i=0; i < hitColliders.Length; i++) {
			if( hitColliders[i].GetComponent<AB_Flock>() == null) continue;
			cohes.x += hitColliders[i].transform.position.x;
			cohes.y += hitColliders[i].transform.position.z;
			neighbors++;			
		}
		if (neighbors == 0) return cohes;
		cohes.x /= neighbors;
		cohes.y /= neighbors;
		cohes.x = cohes.x - transform.position.x;
		cohes.y = cohes.y - transform.position.z;
		cohes.Normalize();

		return cohes;
	}
	public Vector2 computeSeparation(){
		Vector2 separ = new Vector2(0.0F,0.0F); neighbors = 0;

		Collider[] hitColliders = Physics.OverlapSphere(rigidbody.position, searchRadius);
		//float closestEnemyDist = Mathf.Infinity;
		for (int i=0; i < hitColliders.Length; i++) {
			if( hitColliders[i].GetComponent<AB_Flock>() == null) continue;
			separ.x += hitColliders[i].transform.position.x - transform.position.x;
			separ.y += hitColliders[i].transform.position.z - transform.position.z;
			neighbors++;			
		}
		if (neighbors == 0) return separ;
		separ.x /= neighbors;
		separ.y /= neighbors;
		separ.x *= -1.0F;
		separ.y *= -1.0F;
		separ.Normalize();

		return separ;
	}
	
	public Vector3 Flock(){
	
		alignment = computeAlignment();
		cohesion = computeCohesion();
		separation = computeSeparation();
		
		vel.x += alignment.x + cohesion.x + separation.x;
		vel.y += alignment.y + cohesion.y + separation.y;
		vel.Normalize(); 
		vel = vel * enemySpeed;

		return new Vector3(vel.x,0.0F,vel.y);
	
	}

	protected virtual void FixedUpdate () {
		if(CurrTarget != null){
			float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
			if(distToTarget > AtkRange){
				//transform.LookAt(CurrTarget.transform);
				Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
				transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
				
				//moveScript.Move(CurrTarget.position - rigidbody.position);
				moveScript.Move(Flock());
				Debug.Log (Flock ());
				//meshScript.SetDestination(CurrTarget.position);
				
				if(this.tag == "Enemy")
					animation.CrossFade("WolfRunCycle");
				
				if(this.tag == "EnemyRanged")
					animation.CrossFade("ZingBatHover");
			}
		}
	}
	
	
	
	protected virtual void Update () {
		if(targetCheckCounter > targetCheckTimer){
			CurrTarget = FindTarget(TargetSearchRadius);
			targetCheckCounter = 0.0F;
		} else {
			targetCheckCounter += Time.deltaTime;
		}
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
	
	public Rigidbody GetPlayerInRange(float range){
		if (PlayerInRange(range)) return FindPlayer();
		return null; 
	}
	
	public Rigidbody FindTarget(float range){
		
		Rigidbody player = GetPlayerInRange( range);
		if(player !=null && PlayerInRange( PlayerPriorityRange)){
			return FindPlayer();
		}
		
		GameObject GObuilding = Utility.GetClosestWith(transform.position, Mathf.Infinity, Utility.GoHasComponent<IsGenerator>);
		Rigidbody building;
		if(GObuilding != null){ 
			building = GObuilding.rigidbody;
			return building;
		} else {

			return FindPlayer();		
		}
		
	}

}
