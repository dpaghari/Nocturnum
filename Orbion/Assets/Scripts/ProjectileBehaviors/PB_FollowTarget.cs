using UnityEngine;
using System.Collections;

//This a simple behaviour which moves the bullet in a straight line
//with constant force/impulse over time.
//It will apply a single damage to whatever it collides and kill itself.

[RequireComponent (typeof (ProjectileController))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (CanMove))]

public class PB_FollowTarget : ProjectileBehavior {
	
	public ForceMode MoveType;
	//public float MoveForce;
	public CanMove MoveScript;
	public int Damage;
	public float lifeTime = 2.0F;
	public GameObject hitEffect;

	public GameObject target;

	private DumbTimer lifeTimer;
	

	
	
	public override void Initialize(){
		lifeTimer = DumbTimer.New( lifeTime);
	}
	

	
	public override void FixedPerform(){
		Vector3 dir = transform.forward;
		if(target)
			dir = target.transform.position - transform.position;
		MoveScript.Move(dir, MoveType);
	}
	
	public override void Perform(){
		if( target) 
			transform.LookAt( target.transform);

		if( lifeTimer.Finished())
			GameObject.Destroy(this.gameObject);

		lifeTimer.Update();
	}
	
	
	
	public override void OnImpactEnter( Collision other){
		Killable KillScript = other.gameObject.GetComponent<Killable>();
		if( KillScript) {
			KillScript.damage(Damage);
			if( hitEffect != null)
				Instantiate(hitEffect, transform.position, new Quaternion());
		}
		Destroy (gameObject);
	}
	
	public override void OnImpactStay( Collision other){ return;}
	
	public override void OnImpactExit( Collision other){ return;}
	
}
