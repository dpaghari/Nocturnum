using UnityEngine;
using System.Collections;

//Behavior that chases after the player and shoots at them
public class AB_Aggressive : AiBehavior {

	public CanMove moveScript { get; protected set;}
	public CanShoot shootScript { get; protected set;}

	public Rigidbody CurrTarget;
	public float AtkRange;
	//range to go aggressive
	public float SightRange;
	
	private float range;

	override public void OnBehaviorEnter(){
		//Debug.Log("Entering chase");
		moveScript = GetComponent<CanMove>();
		shootScript = GetComponent<CanShoot>();
	
		CurrTarget = FindTarget();
	}
	
	override public void OnBehaviorExit(){return;}
	
	override public void FixedUpdateAB(){
		float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
		if (distToTarget >= AtkRange){
			transform.LookAt(CurrTarget.transform);
			moveScript.Move(CurrTarget.position - rigidbody.position);


			//Vector3 dir = rigidbody.position - CurrTarget.position;
			//transform.forward = dir;

			//rigidbody.MoveRotation(Quaternion.FromToRotation(Vector3.zero, dir));
			//moveScript.Move(CurrTarget.position - rigidbody.position);

		}
	}
	
	override public void UpdateAB(){
		float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
		if (distToTarget < AtkRange){
			animation.CrossFade("WolfAttack");
			shootScript.Shoot(CurrTarget.position);
		}
	}
	
	//Target = player if in range, otherwise Target = nearest building
	public Rigidbody FindTarget(){
		if(PlayerInRange()){ 
			return FindPlayer();
		} else {
			return FindNearestBuilding();
		}
	}

	public Rigidbody FindPlayer(){
		return GameManager.Player.rigidbody;
	}

	//Not implemented
	public Rigidbody FindNearestBuilding(){
		return GameManager.Player.rigidbody;
	}

	public bool PlayerInRange(){
		float distToTarget = Vector3.Distance( FindPlayer().position, rigidbody.position);
		return (distToTarget <= SightRange); 
	}
	
	//Not implemented
	public bool BuildingsExist(){
		return GameObject.Find("Generator");
	}
	
}
