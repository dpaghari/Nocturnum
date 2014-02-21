using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class IsUnderConstruction : MonoBehaviour {

	//Time remaining until building is done
	float constructionCountdown;

	//Total time in seconds to build
	public float totalConstruction = 60;

	public AudioClip finBuild;

	//The building that we want to create when finished construction timer
	public Rigidbody toBuild;

	//Used to determine if we're in light
	private bool lit = false;

	private Rigidbody clone;

	//
	private float heightScale = 5;

	//Scales how fast we rotate when changing build progress
	private int rotationSpeed = 20;

	//on some occasions, the construction will think it's not lit when built on light
	//so we give it an extra time delay before we make it expire
	private float lightExpireDelay = 0.1f;



	//Rotates and scales the object when we increase/decrease construction timer.
	//Extra rotation ratio allows us to increase/decrease the speed we rotate.
	//Used to make bullet building have different feedback than normal time.
	void ChangeBuildProgess( float counterTimeChange, float rotationScale = 1f){
		constructionCountdown += counterTimeChange;

		float yScale =  heightScale *(constructionCountdown / totalConstruction);
		Vector3 newScale = transform.localScale;
		newScale.y = yScale;
		transform.localScale = newScale;

		float rotationAmount = rotationScale * rotationSpeed* Time.deltaTime;
		Quaternion newRotation = Quaternion.Euler(0, rotationAmount , 0);
		newRotation *= transform.localRotation;
		transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.time);
	}



	// Use this for initialization
	void Start () {
		constructionCountdown = totalConstruction;
	}
	



	void Update () {
		if(lit){
			if(constructionCountdown <= 0){

				clone = Instantiate(toBuild, this.transform.position, Quaternion.identity) as Rigidbody;
				audio.PlayOneShot(finBuild, 1.0f);
				Destroy(this.gameObject);
			}

			ChangeBuildProgess( -Time.deltaTime);
		}
		else{
			if(constructionCountdown > totalConstruction + lightExpireDelay){
				ResManager.AddLumen(toBuild.gameObject.GetComponent<Buildable>().cost);
				ResManager.RmUsedEnergy(toBuild.gameObject.GetComponent<Buildable>().energyCost);
				Destroy(this.gameObject);
			}
			ChangeBuildProgess( Time.deltaTime);
		}

	}


	void OnTriggerStay(Collider other){
		if(other.tag == "lightsource") lit = true;
	}


	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "playerBullet") ChangeBuildProgess( -1f, 10f);
	}
}
