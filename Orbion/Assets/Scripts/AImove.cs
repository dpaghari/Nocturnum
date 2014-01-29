using UnityEngine;
using System.Collections;

public class AImove : MonoBehaviour {
	public float speed = 6.0F;
	//public float jumpSpeed = 8.0F;
	//public float gravity = 20.0F;
	//private Vector3 moveDirection = Vector3.zero;
	private float distance = 0;
	public GameObject target;
	//public float smoothTime = 0.3F;
	//public float xOffset = 1.0F;
	//public float yOffset = 1.0F;
	//private var thisTransform : Transform;
	//private var velocity : Vector2;

	/*public void callShoot(Vector3 targ){											// call Shoot() from CanShoot component with target vec3
		GetComponent<CanShoot>().Shoot(targ);
	}
	*/
	void Update() {
		this.target = GameObject.Find ("player_prefab");
		distance = Vector3.Distance(transform.position,target.transform.position);
		if (distance > 5.0) {
			Vector3 targ = target.transform.position;
			Vector3 direction = targ - transform.position;
			direction.Normalize ();
			transform.position += direction * speed * Time.deltaTime;
		} else if (distance <= 5.0) {
			//callShoot(this.target.transform.position);
			GetComponent<CanShoot>().Shoot(this.target.transform.position);
		}
		/*
			Ray ray = this.transform.position;
			RaycastHit hit = target.position;
			// Casts the ray and get the first game object hit
			Physics.Raycast(ray, hit);
			Vector3 adjustY = Vector3.zero;
			adjustY.y += 1;
			Vector3 bullet_dir = hit.point + adjustY - transform.position;
			bullet_dir = bullet_dir.normalized;

			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
		*/	
		
		//moveDirection.y -= gravity * Time.deltaTime;
		//controller.Move(moveDirection * Time.deltaTime);
		
		//this.transform.position = Vector3.MoveTowards (this.transform.position, target.transform.position, .03F);
		//this.transform.LookAt (target.transform);
		//this.transform.position = target.transform.position;
		//this.transform.rotation = target.transform.rotation;
		//transform.position = Vector3.Lerp(this.transform.position, target.transform.position, Time.deltaTime * 10);
	}
	
	void FixedUpdate() {
		//this.transform.position = Vector3.MoveTowards (this.transform.position, target.transform.position, .03F);
		//this.transform.LookAt (target.transform);
		/*
		Ray ray = Camera.main.ScreenPointToRay(target.rigidbody.transform.position);
		RaycastHit hit;
		// Casts the ray and get the first game object hit
		Physics.Raycast(ray, out hit);
		Vector3 adjustY = Vector3.zero;
		adjustY.y += 1;
		Vector3 enemy_dir = hit.point + adjustY - transform.position;
		rigidbody.velocity = enemy_dir;
		*/
		
		
	}
}