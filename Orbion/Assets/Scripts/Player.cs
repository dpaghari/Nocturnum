using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour {
	public float speed = 6.0F;
	//public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	public Rigidbody bullet;
	private Rigidbody clone;
	public Rigidbody building;
	private Rigidbody buildingClone;
	public float bullet_speed = 5.0F;
	public float firingTimer = 0.0F;
	public float firingRate = 20.0F;
	public int canBuild = 0;

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
			if(firingTimer > firingRate && canBuild == 0){
				/*Vector3 mouse = Input.mousePosition;
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
				Destroy(clone, 30);*/
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
			}else if(canBuild == 1){
				/*Vector3 mouse = Input.mousePosition;
				mouse.z = 10.0f;
				Vector3 mouseworldpos = Camera.main.ScreenToWorldPoint(mouse);
				mouseworldpos.y = transform.position.y;
				Vector3 building_dir = mouseworldpos - transform.position;
				buildingClone = Instantiate(building, mouseworldpos + building_dir, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
				*/
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				// Casts the ray and get the first game object hit
				Physics.Raycast(ray, out hit);
				Vector3 adjustY = Vector3.zero;
				adjustY.y += 10;
				buildingClone = Instantiate(building, hit.point + adjustY, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
				canBuild = 0;
				firingTimer = 0.0f;
			}
		}

		if (Input.GetKeyDown(KeyCode.B)){
			canBuild = 1;
		}


	

	}
}