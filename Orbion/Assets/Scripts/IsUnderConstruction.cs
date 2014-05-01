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

	public GameObject vfx1;
	public GameObject vfx2;

	//Used to determine if we're in light
	private bool lit = false;

	private Rigidbody clone;

	//How tall the construction pillar should be at max
	private float heightScale = 5;

	//Scales how fast we rotate when changing build progress
	private int rotationSpeed = 20;

	//on some occasions, the construction will think it's not lit when built on light
	//so we give it an extra time delay before we make it expire
	private float lightExpireDelay = 0.1f;

	//whether or not construction will continue if it is in the darkness
	public bool canBuildOutOfLight = false;



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
		Instantiate(vfx1, transform.position, Quaternion.identity);
		constructionCountdown = totalConstruction;
	}

	//returns true if there is another building too close to this one
	bool IsBuildingNearby(){
		GameObject closestBuilding = Utility.GetClosestWith(transform.position, 10, IsBuilding);

		if (closestBuilding == null)
			return false;


		float minimumDistance;
		if(closestBuilding.GetComponent<Buildable>() != null){
			minimumDistance = closestBuilding.GetComponent<Buildable>().contactRadius + toBuild.GetComponent<Buildable>().contactRadius;
		}else{
			minimumDistance = closestBuilding.GetComponent<IsUnderConstruction>().toBuild.GetComponent<Buildable>().contactRadius + toBuild.GetComponent<Buildable>().contactRadius;
		}
		float actualDistance = Vector2.Distance (new Vector2(closestBuilding.transform.position.x, closestBuilding.transform.position.z), new Vector2(transform.position.x, transform.position.z));
//		Debug.Log (actualDistance);
//		Debug.Log (minimumDistance);

		if (actualDistance < minimumDistance) {
			return true;
		}
		return false;
	}

	//Returns true if given object is a building
	public bool IsBuilding(GameObject theBuilding){
		if(theBuilding.GetComponent<Buildable>() == null && theBuilding.GetComponent<IsUnderConstruction>() == null) 
			return false;
		if (theBuilding == gameObject)
			return false;
		
		return true;
	}


	void Update () {
		if((lit || canBuildOutOfLight) && !IsBuildingNearby()){

			if(constructionCountdown <= 0){
				audio.clip = finBuild;
				audio.PlayOneShot(finBuild, 1.0f);
				Vector3 temp = this.transform.position;
				temp.y -= 2;
				Instantiate(vfx2, temp, Quaternion.identity);
				clone = Instantiate(toBuild, temp, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
				if(toBuild.GetComponent<IsGenerator>() != null){
					TechManager.hasGenerator = true;
				}
				if(toBuild.GetComponent<isTurret>() != null){
					TechManager.hasTurret = true;
				}

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
