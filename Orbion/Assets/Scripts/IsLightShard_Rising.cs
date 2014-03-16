using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class IsLightShard_Rising : MonoBehaviour {
	
	public GameObject lumenShard;
	public float range = 2f;
	public float secondRange = 11f;
	public bool isNear = true;
	//How much this light shard is worth when the player collects it
	//public int lightValue = 10;
	//public AudioClip collectSound;
	
	// Use this for initialization
	void Start () {
	}
	
	
	
	// Update is called once per frame
	void Update () {
		GameObject closestGen = Utility.GetClosestWith (transform.position, secondRange, IsLightWell);
		GameObject playerFound = Utility.GetClosestWith(closestGen.transform.position, range, IsAvatar);
		
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||  Input.GetKey(KeyCode.S) ||  Input.GetKey(KeyCode.D)){
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			isNear = false;
		} else if(playerFound != null && Input.GetMouseButtonDown(1)){
			isNear = true;
		} else if(isNear){
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.088F, transform.position.z);
			if(transform.position.y >= 10 && lumenShard != null){
				Debug.Log ("yo");
				Vector3 temp = transform.position;
				Instantiate (lumenShard, temp, this.transform.rotation);
				Destroy (gameObject);
			}
		}
	}
	
	static bool IsAvatar(GameObject gobj){
		if (gobj.tag == "Player") return true;
		return false;
	}
	
	static bool IsLightWell(GameObject gobj){
		if (gobj.tag == "lightwell") return true;
		return false;
	}
	/*void OnTriggerEnter(Collider collide){
		if(collide.tag == "Generator")
		audio.PlayOneShot(collectSound, 1.0f);
	}
	void OnTriggerStay(Collider collide){
		if(collide.tag == "Generator"){
			//audio.PlayOneShot(collectSound);

			//audio.Play();
			//this.gameObject.GetComponent<Rigidbody>().AddForce((collide.gameObject.transform.position - this.transform.position).normalized * 3);
			Vector3 targ = collide.transform.position;
			Vector3 direction = targ - transform.position;
			direction.Normalize ();
			transform.position += direction * 15.0F * Time.deltaTime;
		}
	}

	void OnCollisionEnter(Collision collide){
		if(collide.gameObject.tag == "Generator"){
			//audio.clip = collectSound;

			//audio.PlayOneShot(collectSound, 1.0f);
			//ResManager.AddLumen(lightValue);
			Destroy (gameObject);
		}*/
}