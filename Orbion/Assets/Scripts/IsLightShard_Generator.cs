using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class IsLightShard_Generator : MonoBehaviour {

	//How much this light shard is worth when the player collects it
	//public int lightValue = 10;
	public AudioClip collectSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider collide){
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
		}
	}
}
