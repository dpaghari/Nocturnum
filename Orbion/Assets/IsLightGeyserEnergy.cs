// Purpose:  Script for each LightGeyserEnergy lumen to act like Regular Lumen and move towards the player when they player is within range.


using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class IsLightGeyserEnergy : MonoBehaviour {
	
	//How much this light shard is worth when the player collects it
	public int energyValue = 1;
	public AudioClip collectSound;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider collide){
		if(collide.tag == "Player")
			audio.PlayOneShot(collectSound, 1.0f);
	}
	void OnTriggerStay(Collider collide){
		if(collide.tag == "Player"){
		
			Vector3 targ = collide.transform.position;
			targ.y += 2;
			Vector3 direction = targ - transform.position;
			direction.Normalize ();
			transform.position += direction * 25.0F * Time.deltaTime;
		}
	}
	
	void OnCollisionEnter(Collision collide){
		if(collide.gameObject.tag == "Player"){
		
			ResManager.AddLGEnergy(energyValue);
			Destroy (gameObject);
		}
	}
}
