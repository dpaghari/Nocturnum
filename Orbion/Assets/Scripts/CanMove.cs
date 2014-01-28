using UnityEngine;
using System.Collections;


public class CanMove : MonoBehaviour {
	public float Force = 6.0F;
	public float MaxSpeed;
	//public float gravity = 20.0F;
	//private Vector3 moveDirection = Vector3.zero;

	void Update() {
		/*
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		*/
	}

	void LateUpdate() {

	}

	public void Move(Vector3 dir, ForceMode mode = ForceMode.Force){
		rigidbody.AddRelativeForce(dir * Force, mode);

		if( rigidbody.velocity.magnitude > MaxSpeed)
			rigidbody.velocity = rigidbody.velocity.normalized * MaxSpeed;
					
	}

}