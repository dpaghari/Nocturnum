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
	private Vector2 alignment;
	private Vector2 cohesion;
	private Vector2 separation;
	private int neighbours = 0;
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
		//check in radius around you
		//v.x += agent.velocity.x;
		//v.y += agent.velocity.y;
		//neighbors++;		
		//if (neighborCount == 0) return v;
		//v.x /= neighbors;
		//v.y /= neighbors;
		//v.normalize(1);
		//return v;

		return new Vector2(0.0F,0.0F);
	}
	public Vector2 computeCohesion(){
		//check in radius around you
		//v.x += agent.x;
		//v.y += agent.y;
		//neighbors++;		
		//if (neighborCount == 0) return v;
		//v.x /= neighbors;
		//v.y /= neighbors;
		//v = new Point(v.x - x, v.y - y);
		//v.normalize(1);
		//return v;

		return new Vector2(0.0F,0.0F);
	}
	public Vector2 computeSeparation(){
		//check in radius around you
		//v.x += agent.x - x;
		//v.y += agent.y - y;
		//neighbors++;		
		//if (neighborCount == 0) return v;
		//v.x /= neighbors;
		//v.y /= neighbors;
		//v.x *= -1.0F;
		//v.y *= -1.0F;
		//v.normalize(1);
		//return v;

		return new Vector2(0.0F,0.0F);
	}
	
	public void flockTime(){
	
		alignment = computeAlignment();
		cohesion = computeCohesion();
		separation = computeSeparation();
		
		vel.x += alignment.x + cohesion.x + separation.x;
		vel.y += alignment.y + cohesion.y + separation.y;
		//vel = vel.Normalize() * enemySpeed;
	
	}


	protected virtual void FixedUpdate () {
		if(CurrTarget != null){
			float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
			if(distToTarget > AtkRange){
				//transform.LookAt(CurrTarget.transform);
				Vector3 lookPosition = new Vector3(CurrTarget.position.x, transform.position.y, CurrTarget.position.z);
				transform.rotation = Quaternion.LookRotation(transform.position - lookPosition);
				moveScript.Move(CurrTarget.position - rigidbody.position);

				flockTime();
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
