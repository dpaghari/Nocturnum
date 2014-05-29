using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ProjectileController))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (CanMove))]

public class PB_TurretBullet : PB_FollowTarget {

	public int hitTimes = 3;
	private int hitsLeft;

	public float hitDelay = 0.25f;
	private DumbTimer hitDelayTimer;

	public GameObject targetRef;
	
	
	
	
	public override void Initialize(){
		base.Initialize();
		hitDelayTimer = DumbTimer.New( hitDelay);
		hitsLeft = hitTimes;
		targetRef = target;
	}
	
	
	
	public override void FixedPerform(){
		base.FixedPerform();
	}
	
	public override void Perform(){
		base.Perform();

		if( target == null && hitDelayTimer.Finished())
			target = targetRef;

		hitDelayTimer.Update();
	}
	
	
	
	public override void OnImpactEnter( Collision other){ return;}
	
	public override void OnImpactStay( Collision other){
		
		if( target != null){
			Killable KillScript = other.gameObject.GetComponent<Killable>();
			if( KillScript) {
				KillScript.damage(Damage);
				hitsLeft--;
				target = null;
				hitDelayTimer.Reset();
				if( hitEffect != null)
					Instantiate(hitEffect, transform.position, new Quaternion());
			}
			
			
			if( hitsLeft <= 0)
				Destroy (gameObject);
		}
		
	}
	
	public override void OnImpactExit( Collision other){ return;}
	
}
