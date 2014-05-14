using UnityEngine;
using System.Collections;

public class FlowerAnimation : MonoBehaviour {

	public isPoisonPlant plantScript;
	private bool gettingHit = false;
	private bool isHit = false;
	// Use this for initialization
	void Start () {
		plantScript = GetComponent<isPoisonPlant>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision other){

		if(other.gameObject.tag == "playerBullet"){

			if (!animation["RaflessiaOpen"].enabled == true){
			animation.CrossFade("RaflessiaOpen");
			}

			//if(plantScript.isActive == false){
			//plantScript.isLit = true;
			//plantScript.isActive = true;
			//Debug.Log ("yo");
			//}
		}


	}

}
