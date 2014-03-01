using UnityEngine;
using System.Collections;

public class IsLightCone : MonoBehaviour {

	public float pushForce;
	public ForceMode pushForceMode = ForceMode.Impulse;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay ( Collider other){
		CanMove moveScript = other.GetComponent<CanMove>();
		
		if (moveScript != null){
			Vector3 pushDir = (other.rigidbody.position - rigidbody.position).normalized;
			other.rigidbody.AddForce(pushDir * pushForce, pushForceMode);
		}
	}
}
