using UnityEngine;
using System.Collections;

//Behavior that chases after the player and shoots at them
public class AB_Aggressive : AiBehavior {

	public CanMove moveScript { get; protected set;}
	public CanShoot shootScript { get; protected set;}

	public Rigidbody CurrTarget;
	public float AtkRange;

	override public void OnBehaviorEnter(){
		moveScript = GetComponent<CanMove>();
		shootScript = GetComponent<CanShoot>();
		CurrTarget = FindTarget();
	}
	
	override public void OnBehaviorExit(){return;}
	
	override public void FixedUpdateAB(){
		float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
		if (distToTarget >= AtkRange){
			transform.forward = new Vector3(CurrTarget.position.x, CurrTarget.position.y, CurrTarget.position.z);
			moveScript.Move(CurrTarget.position - rigidbody.position);

		}
	}
	
	override public void UpdateAB(){
		float distToTarget = Vector3.Distance(rigidbody.position, CurrTarget.position);
		if (distToTarget < AtkRange){
			animation.CrossFade("WolfAttack");
			shootScript.Shoot(CurrTarget.position);
		}
	}

	public Rigidbody FindTarget(){
		return GameManager.Player.rigidbody;
	}
}
