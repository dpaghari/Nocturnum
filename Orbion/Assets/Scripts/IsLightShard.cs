using UnityEngine;
using System.Collections;

public class IsLightShard : MonoBehaviour {

	//How much this light shard is worth when the player collects it
	public int lightValue = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider collide){
		if(collide.tag == "Player"){
			//this.gameObject.GetComponent<Rigidbody>().AddForce((collide.gameObject.transform.position - this.transform.position).normalized * 30);
			Vector3 targ = collide.transform.position;
			Vector3 direction = targ - transform.position;
			direction.Normalize ();
			transform.position += direction * 15.0F * Time.deltaTime;
		}
	}

	void OnCollisionEnter(Collision collide){
		if(collide.gameObject.tag == "Player"){
			ResManager.AddLumen(lightValue);
			Destroy (this.gameObject);
		}
	}
}
