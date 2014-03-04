using UnityEngine;
using System.Collections;

public class IsLightCone : MonoBehaviour {

	public float pushForce;
	public ForceMode pushForceMode = ForceMode.Impulse;
	public float SuitEnergy;
	public float MaxSuitEnergy = 10.0f;
	private float useRate = 30.0f;
	private float useCooldown = 0.0f;

	// Use this for initialization
	void Start () {
		SuitEnergy = MaxSuitEnergy;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(SuitEnergy);


		/*  If the player has the flashlight on and they still have some SuitEnergy
		 *  Reduce the SuitEnergy by Time.deltaTime
		 * 
		 */
		if(gameObject.light.enabled == true && SuitEnergy > 0){
		useCooldown += 1.0f;
			if(useCooldown >= useRate){
				SuitEnergy -= Time.deltaTime * 2;
				useCooldown = 0.0f;

			}
		}



		// If the player runs out of energy but still had the flashlight on, turn off the flashlight
		if(gameObject.light.enabled == true && SuitEnergy <= 0){

			gameObject.light.enabled = !gameObject.light.enabled;
		}
	}

	void OnTriggerStay ( Collider other){
		if ( gameObject.light.enabled == false) return;

		CanMove moveScript = other.GetComponent<CanMove>();

		if (moveScript != null){
			Vector3 pushDir = (other.rigidbody.position - rigidbody.position).normalized;
			other.rigidbody.AddForce(pushDir * pushForce, pushForceMode);
		}
	}


	//  If the player enters the collider of a Generator
	//  it refills their suitenergy
	void OnTriggerEnter (Collider other){
		if(other.tag == "Generator")
			SuitEnergy = MaxSuitEnergy;
	}
}
