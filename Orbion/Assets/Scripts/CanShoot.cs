using UnityEngine;
using System.Collections;

public class CanShoot : MonoBehaviour {

	//the object to shoot
	public Rigidbody bullet;

	//the speed at which to shoot the object
	public float bullet_speed = 200.0F;	

	//the cooldown in seconds between shots
	public float firingRate = 1F;	

	public AudioClip enemyShotSound;

	//used to keep track of our shooting cooldown
	protected float firingTimer = 0.0F;
	
	//holds a reference to the bullet that will be made
	private Rigidbody clone;




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
			if(tag == "Enemy"){
				audio.clip = enemyShotSound;
				audio.PlayOneShot(enemyShotSound,1);
			}
			clone = Instantiate(bullet, transform.position + dir * 2, Quaternion.LookRotation(dir, Vector3.up)) as Rigidbody;
			firingTimer = 0.0f;
		}
	}

	//shoots a bullet from the object's position to the target point: targ
	public virtual void Shoot(Vector3 targ){
	
		ShootDir( targ - transform.position );
	}




}