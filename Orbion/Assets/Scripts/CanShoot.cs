using UnityEngine;
using System.Collections;

public class CanShoot : MonoBehaviour {

	public Rigidbody bullet;										// takes in bullet_prefab
	private Rigidbody clone;
	public float bullet_speed = 5.0F;								// Default Bullet Speed
	public float firingTimer = 0.0F;								// Counter for the next shot
	public float firingRate = 20.0F;								// Cooldown for each shot

	
	// Update is called once per frame
	void Update () {
		firingTimer++;
									
		//Shoot();


	}

	public void Shoot(){													// creates an instance of bullet_prefab and shoots it towards mouse cursor

													
		if(firingTimer > firingRate){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			// Casts the ray and get the first game object hit
			Physics.Raycast(ray, out hit);
			Vector3 adjustY = Vector3.zero;
			adjustY.y += 1;
			Vector3 bullet_dir = hit.point + adjustY - transform.position;
			bullet_dir = bullet_dir.normalized;
			clone = Instantiate(bullet, transform.position + bullet_dir * 2, Quaternion.LookRotation(hit.point + adjustY - transform.position, Vector3.up)) as Rigidbody;
			clone.rigidbody.velocity = bullet_dir * bullet_speed * 20;
			firingTimer = 0.0f;
			GetComponent<CanShootReload>().currentAmmo--;
				
		}
	}
}