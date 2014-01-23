using UnityEngine;
using System.Collections;

public class AImove : MonoBehaviour {
	public float speed = 6.0F;
	//public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	
	void Update() {

		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		
		moveDirection.y -= gravity * Time.deltaTime;
	}
}