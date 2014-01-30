using UnityEngine;
using System.Collections;

public class CanShoot : MonoBehaviour {

	//the object to shoot
	public Rigidbody bullet;

	//the speed at which to shoot the object
	public float bullet_speed = 5.0F;	

	//the cooldown in seconds between shots
	public float firingRate = 1F;	



	//used to keep track of our shooting cooldown
	protected float firingTimer = 0.0F;
	
	//holds a reference to the bullet that will be made
	private Rigidbody clone;
	
	protected virtual void Start () {
		firingTimer = firingRate;
	}

	// Update is called once per frame
	protected virtual void Update () {
		//firingTimer++;								
		//Shoot();

		//adding by Time.deltaTime otherwise our firerate is bullets/frame instead of bullets/second
		if ( firingTimer <= firingRate) firingTimer += Time.deltaTime;
		
	}


	public virtual void Shoot(Vector3 targ){													// creates an instance of bullet_prefab and shoots it towards mouse cursor

													
		if( FinishCooldown()){
			/*Ray ray = Camera.main.ScreenPointToRay(targ);
			RaycastHit hit;
			// Casts the ray and get the first game object hit
			Physics.Raycast(ray, out hit);
			*/
			Vector3 adjustY = Vector3.zero;
			//adjustY.y += 1;

			Vector3 bullet_dir = targ + adjustY - transform.position;
			bullet_dir = bullet_dir.normalized;
			clone = Instantiate(bullet, transform.position + bullet_dir * 2, Quaternion.LookRotation(targ + adjustY - transform.position, Vector3.up)) as Rigidbody;
			clone.rigidbody.velocity = bullet_dir * bullet_speed * 20;
			firingTimer = 0.0f;

				
		}
	}

	public void ResetFiringTimer(){
		firingTimer = 0.0f;
	}

	protected bool FinishCooldown(){
		return firingTimer >= firingRate;
	}

}