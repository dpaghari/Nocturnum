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
			this.gameObject.GetComponent<Rigidbody>().AddForce((collide.gameObject.transform.position - this.transform.position).normalized * 3);
		}
	}

	void OnCollisionEnter(Collision collide){
		if(collide.gameObject.tag == "Player"){
			ResManager.AddLumen(lightValue);
			Destroy (this.gameObject);
		}
	}
}
