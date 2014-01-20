using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour {
	public float speed = 6.0F;
	//public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	public Rigidbody bullet;
	private Rigidbody clone;
	public float bullet_speed = 5.0F;
	public float firingTimer = 0.0F;
	public float firingRate = 20.0F;

	void Update() {
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

		firingTimer++;
		// Mouse1 was pressed, launch a projectile
		if (Input.GetMouseButton(0))
		{	
			if(firingTimer > firingRate){
				Vector3 mouse = Input.mousePosition;
				mouse.z = 1.0f;
				Vector3 mouseworldpos = Camera.main.ScreenToWorldPoint(mouse);
				mouseworldpos.y = transform.position.y;
				Vector3 bullet_dir = mouseworldpos - transform.position;
				bullet_dir = bullet_dir.normalized;
				//Rigidbody clone;
				clone = Instantiate(bullet, transform.position + bullet_dir * 2, Quaternion.LookRotation(mouseworldpos, Vector3.forward)) as Rigidbody;
				clone.rigidbody.transform.rotation = Quaternion.LookRotation(mouseworldpos, Vector3.forward);
				//clone.rigidbody.AddForce(bullet_dir * bullet_speed);
				clone.rigidbody.velocity = bullet_dir * bullet_speed;
				Destroy(clone, 30);
		
				firingTimer = 0.0f;
			}
		}



	

	}
}