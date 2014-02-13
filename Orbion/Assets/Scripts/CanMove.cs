using UnityEngine;
using System.Collections;


public class CanMove : MonoBehaviour {
	public float Force = 6.0F;
	public float MaxSpeed = 20;
	public float MoveScale = 1.0f;
	
	protected float ForceScale;
	protected float MaxSpeedScale;

	void Update() {
	}

	void LateUpdate() {

	}

	public void Move(Vector3 dir, ForceMode mode = ForceMode.Force){
		rigidbody.AddForce(dir * Force * MoveScale, mode);

		if( rigidbody.velocity.magnitude > MaxSpeed * MoveScale)
			rigidbody.velocity = rigidbody.velocity.normalized * MaxSpeed * MoveScale;
					
	}

}