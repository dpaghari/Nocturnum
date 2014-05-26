using UnityEngine;
using System.Collections;

public class CanShoot : MonoBehaviour {

	//the object to shoot
	public Rigidbody bullet;

	//the speed at which to shoot the object
	public float bullet_speed = 200.0F;	

	//the cooldown in seconds between shots
	public float firingRate = 1F;

	public float spreadAngle = 45f;

	public int numOfBulletShot = 1;

	//range of attack
	public float projectileStartPosition = 1.0F;	

	public AudioClip enemyShotSound;

	//used to keep track of our shooting cooldown
	protected float firingTimer = 0.0F;
	
	//holds a reference to the bullet that will be made
	private Rigidbody clone;

	// Variable bullet spawn height for diff users
	public Vector3 bulletHeight;

	// effect to play when you shoot
	public GameObject shootEffect;

	public WeakensInLight weakenScript;



	//sets the proportion of completion for the firingTimer
	//e.g. 0.5 = 50% finished with cooldown
	public void SetFiringTimer(float ratio){
		firingTimer = firingRate * ratio;
	}
	
	//Sets the FiringTimer to the beginning of the cooldown count
	public void ResetFiringTimer(){
		SetFiringTimer(0);
	}
	
	//returns if we have elapsed the firing cooldown
	public bool FinishCooldown(){
		return firingTimer >= firingRate;
	}



	
	protected virtual void Start () {
		weakenScript = GetComponent<WeakensInLight>();

		//we want to be able to shoot when created
		firingTimer = firingRate;
	}


	// Update is called once per frame
	protected virtual void Update () {
		//adding by Time.deltaTime otherwise our firerate is bullets/frame instead of bullets/second
		if ( firingTimer <= firingRate) firingTimer += Time.deltaTime;
		
	}


	//shoots a bullet from object's position with an angle of dir
	public virtual void ShootDir ( Vector3 dir){
		if( FinishCooldown()){
			dir.Normalize();

			Vector3 temp = transform.position;
			temp.y += bulletHeight.y;
			clone = Instantiate(bullet, temp + dir * projectileStartPosition, Quaternion.LookRotation(dir, Vector3.down)) as Rigidbody;
		
			if(tag == "Enemy" || tag == "EnemyRanged"){
				//audio.clip = enemyShotSound;
				float rand = Random.value;

				if(rand > 0.0 && rand < 0.20)
				audio.PlayOneShot(enemyShotSound,0.3f);
		
				/*
				if(weakenScript.IsWeakened){
					
					if(clone.GetComponent<PB_Linear>() != null) clone.GetComponent<PB_Linear>().Damage = 5;
					if(clone.GetComponent<PB_Melee>() != null) clone.GetComponent<PB_Melee>().Damage = 10;
					//Debug.Log ("Weakened");
				}
				else{
					
					if(clone.GetComponent<PB_Linear>() != null) clone.GetComponent<PB_Linear>().Damage = 10;
					if(clone.GetComponent<PB_Melee>() != null) clone.GetComponent<PB_Melee>().Damage = 15;
					//Debug.Log ("Weaken Faded");
				}
				*/

			}
			firingTimer = 0.0f;
			clone = Instantiate(shootEffect, temp + dir * 2, Quaternion.AngleAxis(-90, Vector3.forward)) as Rigidbody;
		}
	}



	//shoots a bullet from the object's position to the target point: targ
	/*
	public virtual void Shoot(Vector3 targ){
	
		ShootDir( targ - transform.position );
	}
	*/

	public virtual void stun(){
	//stun the player
	}

	public virtual void Scattershot( Vector3 target, int numberOfShots){
		if( FinishCooldown()){
			Vector3 dir = target - transform.position;

			if( numberOfShots == 1){
				ShootDir( dir);
				return;
			}

			Vector3 leftBound = Quaternion.Euler( 0, -spreadAngle/2, 0) * dir;
			float bulletGap = spreadAngle / ( numberOfShots - 1);

			//numberOfShots - 1 makes us shoot from the left side of the spread bound to the right
			//but if we have a 360deg angle, both bounds of the spread are at the same place,
			//causing 2 bullets to fire at the same spot
			if( spreadAngle == 360) bulletGap = spreadAngle / ( numberOfShots);

			for ( int i = 0; i < numberOfShots; i++){
				float angleOffset = i * bulletGap;
				Vector3 BulDir = Quaternion.Euler( 0, angleOffset, 0) * leftBound ;
				SetFiringTimer( 1.0f);
				ShootDir( BulDir);
			}


		}
	}


	public virtual void Shoot(Vector3 target){
		Scattershot (target, numOfBulletShot);
	}

	
	public virtual void ShootTarget( GameObject target){
		Vector3 temp = transform.position;
		temp.y += bulletHeight.y;

		Vector3 dir = (target.transform.position - transform.position).normalized;

		GameObject theBullet = Instantiate(bullet.gameObject, temp + dir * projectileStartPosition, Quaternion.identity) as GameObject;
		PB_FollowTarget followScript;

		if( theBullet){
			followScript = theBullet.GetComponent<PB_FollowTarget>();
			if( followScript) followScript.target = target;
		}

		if( shootEffect)
			Instantiate(shootEffect, temp + dir* 2, Quaternion.AngleAxis(-90, Vector3.forward)) ;

		firingTimer = 0.0f;
	}


}