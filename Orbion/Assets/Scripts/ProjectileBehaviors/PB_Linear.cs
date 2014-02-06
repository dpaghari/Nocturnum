using UnityEngine;
using System.Collections;

//This a simple behaviour which moves the bullet in a straight line
//with constant force/impulse over time.
//It will apply a single damage to whatever it collides and kill itself.

[RequireComponent (typeof (ProjectileController))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (CanMove))]

public class PB_Linear : ProjectileBehavior {

	public ForceMode MoveType;
	//public float MoveForce;
	public CanMove MoveScript;
	public int Damage;



	public override void FixedPerform(){
		MoveScript.Move(transform.forward, MoveType);
		
	}

	public override void Perform(){ return;}
	
	

	public override void OnImpactEnter( Collision other){
		Killable KillScript = other.gameObject.GetComponent<Killable>();
		if( KillScript) KillScript.damage(Damage);
		Destroy (gameObject);
	}

	public override void OnImpactStay( Collision other){ return;}

	public override void OnImpactExit( Collision other){ return;}

}
