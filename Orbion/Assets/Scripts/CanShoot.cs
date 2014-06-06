//Purpose: Functionality for shooting objects at other objects

using UnityEngine;
using System.Collections;

public class CanShoot : MonoBehaviour {

	//the object to shoot
	public Rigidbody bullet;

	//the speed at which to shoot the object
	public float bullet_speed = 200.0F;	

	//the cooldown in seconds between shots
	public float firingRate = 1F;

	//the angular spread (deg), if we're shooting more than one bullet
	public float spreadAngle = 45f;

	//number of bullets per shot
	public int numOfBulletShot = 1;

	//positional offset of attack
	public float projectileStartPosition = 1.0F;	

	public AudioClip enemyShotSound;

	//used to keep track of our shooting cooldown
	//protected float firingTimer = 0.0F;
	public DumbTimer firingTimer;

	// Variable bullet spawn height for diff users
	public Vector3 bulletHeight;

	// effect to play when you shoot
	public GameObject shootEffect;

	//public WeakensInLight weakenScript;

	//stun timer variables
	private DumbTimer stunTimer;
	public float stunInterval = 0.1F;
	private bool stunFinished = false;


	
	//Public wrappers to simplify external calls
	public void SetFiringTimer(float ratio) { firingTimer.SetProgress( ratio);}
	public void ResetFiringTimer() { firingTimer.Reset(); }
	public bool FinishCooldown() { return firingTimer.Finished(); }
	public void SetFiringRate( float newFireRate) { firingTimer.MaxTime = newFireRate;}


	
	protected virtual void Start () {
		//weakenScript = GetComponent<WeakensInLight>();
		firingTimer = DumbTimer.New( firingRate);
		firingTimer.SetProgress(1.0f);
	}


	// Update is called once per frame
	protected virtual void Update () {

		if(stunTimer != null && !stunFinished){
			if(stunTimer.Finished()){
				Debug.Log("STUN DONE!!");
				stunFinished = true;
				//stunTimer.Reset();
				GameManager.KeysEnabled = true;
				//Debug.Log("STUN DONE!!");
				
			}
			else{
				stunTimer.Update();
			}
		}

		firingTimer.Update();
		
	}


	//shoots a bullet from object's position with an angle of dir
	public virtual void ShootDir ( Vector3 dir){
		if( FinishCooldown()){
			dir.Normalize();

			Vector3 temp = transform.position;
			temp.y += bulletHeight.y;
			Rigidbody clone = Instantiate(bullet, temp + dir * projectileStartPosition, Quaternion.LookRotation(dir, Vector3.down)) as Rigidbody;
		
			if(tag == "Enemy" || tag == "EnemyRanged"){
				//audio.clip = enemyShotSound;
				float rand = Random.value;

				if(rand > 0.0 && rand < 0.20){
					if(enemyShotSound != null){
					audio.PlayOneShot(enemyShotSound,0.3f);
					}
				
				}
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
			firingTimer.Reset();
			if( shootEffect)
				clone = Instantiate(shootEffect, temp + dir * 2, Quaternion.AngleAxis(-90, Vector3.forward)) as Rigidbody;
		}
	}


	//Why is stun code in CanShoot?
	public virtual void stun(){
	//stun the player
		GameManager.KeysEnabled = false;
		
		if(stunTimer == null){
			//stunTimer.Reset();
			stunTimer = DumbTimer.New( stunInterval);
			Debug.Log("STUN!!");
		} else if(stunFinished){
			stunTimer.Reset();
			stunFinished = false;
			Debug.Log("STUN!!");
		}
		
		
	}


	//Shoots multiple bullets at position target, evenly split through the spread(deg)
	//The center of the spread is target
	public virtual void Scattershot( Vector3 target, int numberOfShots, float spread){
		if( FinishCooldown()){
			Vector3 dir = target - transform.position;

			if( numberOfShots == 1 || (tag == "Player" && TechManager.GetNumBuilding(Tech.ballistics) == 0)){
				ShootDir( dir);
				return;
			}

			Vector3 leftBound = Quaternion.Euler( 0, -spread/2, 0) * dir;
			float bulletGap = spread / ( numberOfShots - 1);

			//numberOfShots - 1 makes us shoot from the left side of the spread bound to the right
			//but if we have a 360deg angle, both bounds of the spread are at the same place,
			//causing 2 bullets to fire at the same spot
			if( spread == 360) bulletGap = spread / ( numberOfShots);

			for ( int i = 0; i < numberOfShots; i++){
				float angleOffset = i * bulletGap;
				Vector3 BulDir = Quaternion.Euler( 0, angleOffset, 0) * leftBound ;
				SetFiringTimer( 1.0f);
				ShootDir( BulDir);
			}


		}
	}


	//default shooting method
	public virtual void Shoot(Vector3 target){
		Scattershot (target, numOfBulletShot, spreadAngle);
	}

	
	//special shooting method for seeking bullets that keep track of a game object
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

		ResetFiringTimer();
	}


}