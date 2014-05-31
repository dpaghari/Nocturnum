using UnityEngine;
using System.Collections;


public class CanMove : MonoBehaviour {

	public float Force = 6.0F;
	public float InitForce;
	public float MaxSpeed = 20;
	public float MoveScale = 1.0f;
	public float MaxForce = 66.0F;
	protected float ForceScale;
	protected float MaxSpeedScale;

	public float getForce(){
		return Force;
	}
	
	void Start(){
	
		InitForce = Force;
	}


	void Update() {

		if(Force >= MaxForce){
			Force = InitForce;
		}
	}

	void LateUpdate() {

	}

	public void Move(Vector3 dir, ForceMode mode = ForceMode.Force){
		float adjustedMoveScale = MoveScale;
		if( adjustedMoveScale < 0) adjustedMoveScale = 0;

		rigidbody.AddForce(dir * Force * adjustedMoveScale, mode);

		if( rigidbody.velocity.magnitude > MaxSpeed * adjustedMoveScale)
			rigidbody.velocity = rigidbody.velocity.normalized * MaxSpeed * adjustedMoveScale;
					
	}

	public void TurnLeft(Vector3 up, ForceMode mode = ForceMode.Force){
		rigidbody.AddTorque(up * 5, mode);
	}

	public void TurnRight(Vector3 up, ForceMode mode = ForceMode.Force){
		rigidbody.AddTorque(up * -5, mode);
	}

}