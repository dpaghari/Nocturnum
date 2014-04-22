using UnityEngine;
using System.Collections;

public class FlowerAnimation : MonoBehaviour {


	private bool gettingHit = false;
	private bool isHit = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//if(!gettingHit)
		//	animation.CrossFade("RaflessiaPoseLib");
	
	}

	void OnCollisionEnter(Collision other){

		if(other.gameObject.tag == "playerBullet"){

			//gettingHit = true;
			animation.CrossFade("RaflessiaOpen");
			GetComponent<isPoisonPlant>().isLit = true;
			//animation.CrossFade("RaflessiaOpen");

		}


	}

}
