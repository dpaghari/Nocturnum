using UnityEngine;
using System.Collections;

//This a simple behaviour which moves the bullet in a straight line
//with constant force/impulse over time.
//It will apply a single damage to whatever it collides and kill itself.

[RequireComponent (typeof (ProjectileController))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (CanMove))]

public class PB_AoeZingBat : ProjectileBehavior {

	public ForceMode MoveType;
	public CanMove MoveScript {get; private set;}
	public CanShoot ShootScript {get; private set;}

	public int Damage;
	public Color EndTrailColor = Color.red;
	public ParticleSystem Trail;

	public GameObject hitEffect;


	public float lifeTime = 2.0F;
	private DumbTimer lifeTimer;
	private Color OriginalTrailColor;



	public override void Initialize(){
		lifeTimer = DumbTimer.New(lifeTime);
		MoveScript = GetComponent<CanMove>();
		ShootScript = GetComponent<CanShoot>();
		Trail = GetComponentInChildren<ParticleSystem>();
		OriginalTrailColor = Trail.startColor;
		return;
	}

	public void Update(){
		if( lifeTimer.Finished()){
			ShootScript.Shoot(transform.position + transform.forward);
			Destroy( this.gameObject);
		}
		Trail.startColor = Color.Lerp(OriginalTrailColor, EndTrailColor, lifeTimer.GetProgress());
		lifeTimer.Update();
	}

	public override void FixedPerform(){
		MoveScript.Move(transform.forward, MoveType);
	}

	public override void Perform(){ return;}



	public override void OnImpactEnter( Collision other){
		Killable KillScript = other.gameObject.GetComponent<Killable>();
		if( KillScript) {
			KillScript.damage(Damage);
			Instantiate(hitEffect, transform.position, new Quaternion());
		}

		ShootScript.Shoot(transform.position + transform.forward);
		Destroy( this.gameObject);

	}

	public override void OnImpactStay( Collision other){ return;}

	public override void OnImpactExit( Collision other){ return;}

}
