using UnityEngine;
using System.Collections;

//This a simple behaviour which moves the bullet in a straight line
//with constant force/impulse over time.
//It will apply a single damage to whatever it collides and kill itself.

[RequireComponent (typeof (ProjectileController))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
//[RequireComponent (typeof (CanMove))]

public class PB_Melee : ProjectileBehavior {
	
	//public ForceMode MoveType;
	//public float MoveForce;
	//public CanMove MoveScript;
	public float Damage = 20.0f;
	public int searingLevel;
	public GameObject dot;
	public GameObject hitEffect;
	private GameObject clone;
	
	private float lifeTime = 0.5F;
	private float lifeCounter = 0.0F;

	public void Update(){
		if(lifeCounter > lifeTime){
			Destroy(this.gameObject);
		} else {
			lifeCounter += Time.deltaTime;
		}
	}
	
	public override void FixedPerform(){
		//MoveScript.Move(transform.forward, MoveType);
		
	}
	
	public override void Perform(){ return;}
	
	
	
	public override void OnImpactEnter( Collision other){
		PlayerHealth KillScript = other.gameObject.GetComponent<PlayerHealth>();
		if( KillScript) {
			KillScript.damage(Damage);
			clone = Instantiate(hitEffect, transform.position, new Quaternion()) as GameObject;
		}
		
		//drop a DOT on target if searing is > 0
		if(searingLevel > 0 && other.gameObject.tag == "Enemy"){
			clone = Instantiate(dot, other.transform.position, new Quaternion()) as GameObject;
			clone.GetComponent<IsSearingShot>().target = other.gameObject.GetComponent<Rigidbody>();
		}
		
		foreach( Transform child in transform){
			if(child.gameObject.tag == "playerBullet"){
				Destroy(child.gameObject);
			}
			else{
				child.gameObject.GetComponent<CFX_AutoDestructShuriken>().OnlyDeactivate = false;
			}
		}
		
		transform.DetachChildren();
		Destroy (gameObject);
	}
	
	public override void OnImpactStay( Collision other){ return;}
	
	public override void OnImpactExit( Collision other){ return;}
	
}
