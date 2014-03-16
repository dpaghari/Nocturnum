using UnityEngine;
using System.Collections;

public class IsLightCone : MonoBehaviour {

	public Corruptable corruptType = Corruptable.none;


	public AudioClip flashOn;
	public AudioClip flashOff;
	public AudioClip hum;


	public float pushForce;
	public ForceMode pushForceMode = ForceMode.Impulse;
	public float SuitEnergy;
	public float MaxSuitEnergy = 1.0f;
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

		if(!gameObject.light.enabled && Input.GetMouseButtonDown(0)){
			audio.PlayOneShot(flashOn, 1.0f);

		}
		if(gameObject.light.enabled && Input.GetMouseButtonUp(0)){
			audio.PlayOneShot(flashOff, 1.0f);
			
		}
		/*if(gameObject.light.enabled && Input.GetMouseButton(0)){
			audio.PlayOneShot(hum, 1.0f);
			
		}
*/





		/*  If the player has the flashlight on and they still have some SuitEnergy
		 *  Reduce the SuitEnergy by Time.deltaTime
		 * 
		 */
		if(gameObject.light.enabled == true && SuitEnergy > 0){
		useCooldown += 1.0f;
			if(useCooldown >= useRate){
				SuitEnergy -= Time.deltaTime * 20;
				useCooldown = 0.0f;

			}
		}



		// If the player runs out of energy but still had the flashlight on, turn off the flashlight
		if(gameObject.light.enabled == true && SuitEnergy == 0){

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
		//no light or its on cd return
		if (!gameObject.light.enabled || lightHit) return;

		CanMove moveScript = other.GetComponent<CanMove>();
		Killable killScript = other.GetComponent<Killable>();
		Corruption corruptScript = other.GetComponent<Corruption>();
		
		//target an enemy do damage
		if ( moveScript != null){
			Vector3 pushDir = Utility.FindDirNoY(rigidbody.position, other.rigidbody.position);
			other.rigidbody.AddForce(pushDir * pushForce, pushForceMode);
			lightHit = true;
		}
		
		//target a generator remove corruption
		if(corruptScript){
			Debug.Log("attack generator #: " + ++counter);
			corruptScript.corrupt(Damage);
			lightHit = true;
		}
	
	}


	//  If the player enters the collider of a Generator
	//  it refills their suitenergy
	void OnTriggerEnter (Collider other){
		//Debug.Log(other);

		if(other.tag == "Plant")
			other.GetComponent<isPlant>().isLit = true;
		
	}
	void OnTriggerExit(Collider other){
		if(other.tag == "Plant")
			other.GetComponent<isPlant>().isLit = false;
	}

	public void ResetEnergy(){
				SuitEnergy = MaxSuitEnergy;
	}
}

