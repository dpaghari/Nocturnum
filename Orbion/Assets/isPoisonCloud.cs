using UnityEngine;
using System.Collections;

public class isPoisonCloud : MonoBehaviour {

	public int cloudDmg = 20;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other){
		//Debug.Log ("colliding with things");
		if(other.gameObject.GetComponent<Killable>() != null){
			//Debug.Log ("colliding with killablestuff");
			other.gameObject.GetComponent<Killable>().damage(cloudDmg);
			Destroy (gameObject);
		}

	}
}
