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
	

	public int Damage = 1;
	
	public GameObject hitEffect;
	private GameObject clone;
	
	private float lifeTime = 0.5F;
	private float lifeCounter = 0.0F;

	
	public override void Initialize(){return;}
	public override void FixedPerform(){ return;}

	public override void Perform(){
		if(lifeCounter > lifeTime)
			Destroy(this.gameObject);
		else 
			lifeCounter += Time.deltaTime;
	}
	
	
	
	public override void OnImpactEnter( Collision other){
		Killable KillScript = other.gameObject.GetComponent<Killable>();
		if( KillScript) {
			KillScript.damage(Damage);
			//Debug.Log(Damage);
			//Debug.Log(this.gameObject.name);
			clone = Instantiate(hitEffect, transform.position, new Quaternion()) as GameObject;
		}
		GameObject.Destroy(this.gameObject);
	}
	
	public override void OnImpactStay( Collision other){ return;}
	
	public override void OnImpactExit( Collision other){ return;}
	
}
