//Purpose: Regulates the progress of a building under construction

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class IsUnderConstruction : MonoBehaviour {

	//Special effects for building
	public GameObject vfx1;
	public GameObject vfx2;

	//public AudioClip finBuild;
	public AudioClip errBuild;

	//The Y-scale of the construction pillar at 0% completion
	public float heightScale = 5;
	
	//How fast we rotate when naturally changing build progress
	public float rotationSpeed = 20f;

	//Scales how fast we rotate when changing build progress from a bullet
	public float bulletRotationSpeed = 10f;

	//How  much construction time a player's bullet reduces when hitting
	public float BulletTimeReduction = 1/10f;



	//These three are set in CanBuild when prefab is made
	//The building that we want to create when finished construction timer
	public Rigidbody toBuild {get; set;}
	//Total time in seconds to build
	public float totalConstruction {get; set;}
	//Whether or not construction will continue if it is in the darkness
	public bool canBuildOutOfLight {get; set;}



	//Time remaining until building is done
	float constructionCountdown;

	//Used to determine if we're in light
	private bool lit = false;

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

	//building complete
	void CreateBuilding(){
		//Placement of building is slightly offsetted up to
		//   prevent buildings from being built in the floor
		Vector3 temp = this.transform.position;
		temp.y -= 2;
		Rigidbody clone = Instantiate(toBuild, temp, Quaternion.identity) as Rigidbody;
		
		//audio.clip = finBuild;
		//audio.PlayOneShot(finBuild, 1.0f);

		Destroy(this.gameObject);	
	}


	//Gives back the player resources
	void RefundBuilding(){
		Buildable buildInfo = toBuild.gameObject.GetComponent<Buildable>();
		ResManager.AddLumen(buildInfo.lumenCost);
		ResManager.AddEnergy(buildInfo.energyCost);
		Destroy(this.gameObject);	
	}


	// Use this for initialization
	void Start () {
		//Instantiate(vfx1, transform.position, Quaternion.identity);
		constructionCountdown = totalConstruction;
	}

	

	void Update () {
		if( lit || canBuildOutOfLight){
			if(constructionCountdown <= 0){
				CreateBuilding();
			}
			ChangeBuildProgess( -Time.deltaTime);
		}
		else{
			if(constructionCountdown > totalConstruction + lightExpireDelay){
				RefundBuilding();
			}
			ChangeBuildProgess( Time.deltaTime);
		}
	}


	void OnTriggerStay(Collider other){
		if(other.tag == "lightsource") lit = true;
	}


	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "playerBullet") ChangeBuildProgess( -BulletTimeReduction, bulletRotationSpeed);
		if(other.gameObject.GetComponent<IsUnderConstruction>() != null) Physics.IgnoreCollision( this.collider, other.collider); 
	}
}
