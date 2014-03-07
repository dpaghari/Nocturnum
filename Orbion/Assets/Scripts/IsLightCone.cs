using UnityEngine;
using System.Collections;

public class IsLightCone : MonoBehaviour {

	public float pushForce;
	public ForceMode pushForceMode = ForceMode.Impulse;
	public float SuitEnergy;
	public float MaxSuitEnergy = 10.0f;
	private float useRate = 30.0f;
	private float useCooldown = 0.0f;

	//deals with spacing out the damage
	public int Damage;
	private int counter;
	private bool lightHit = false;
	public float lightCooldown;
	private float lightCounter = 0.0F;

	// Use this for initialization
	void Start () {
		SuitEnergy = MaxSuitEnergy;
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(SuitEnergy);


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

	void FixedUpdate(){
		if (lightHit) {
			lightCounter += Time.deltaTime;
			if(lightCounter >= lightCooldown){
				lightHit = false;
				lightCounter = 0.0F;
			}
		}
	}

	void OnTriggerStay ( Collider other){
		if ( gameObject.light.enabled == false) return;

			CanMove moveScript = other.GetComponent<CanMove>();
			Killable killScript = other.GetComponent<Killable>();
			if (killScript != null && !lightHit){
				Vector3 pushDir = (other.rigidbody.position - rigidbody.position).normalized;
				other.rigidbody.AddForce(pushDir * pushForce, pushForceMode);
				lightHit = true;
				killScript.damage(Damage);
				//Debug.Log("attack #: " + ++counter);
			}
		

		if(other.tag == "Generator")
			SuitEnergy = MaxSuitEnergy;
	}


	//  If the player enters the collider of a Generator
	//  it refills their suitenergy
	void OnTriggerEnter (Collider other){
		if(other.tag == "Generator")
			SuitEnergy = MaxSuitEnergy;
	}
}

