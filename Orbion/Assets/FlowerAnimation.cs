// PURPOSE:  Script that is in charge of the Raflessia Snare Plant.  It starts as Inactive, and upon getting hit by a bullet(player or enemies) it becomes active.
// Upon activation, the plant will change it's collider to be a trigger allowing objects to pass through the collider and effectively triggering a snaring effect
// which simply locks the object in the flower's position and releases them after a short duration.

using UnityEngine;
using System.Collections;

public class FlowerAnimation : MonoBehaviour {

	private bool isActive;
	private bool isHolding;
	private DumbTimer timerScript;
	// Use this for initialization
	void Start () {
		timerScript = DumbTimer.New(3.0f, 1.0f);				// Duration of Raflessia Snare
		isActive = false;
		isHolding = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(isActive){

			timerScript.Update();

		}



	}

	void OnTriggerEnter(Collider other){


		if(!isActive){
			if(other.gameObject.tag == "playerBullet" || other.gameObject.tag == "enemy_bullet"){
		
				animation.Play("RaflessiaOpen");
				StartCoroutine(WaitAndCallback(animation["RaflessiaOpen"].length));
				Destroy(other.gameObject);
		
			}
		}
	}

	void OnTriggerStay(Collider other){



		if(isActive){
			if(other.tag == "Player" && isHolding == false){
				//other.transform.position = transform.position;
				//GameManager.KeysEnabled = false;
				other.GetComponent<CanMove>().MoveScale -= 1;
				animation.Play("RaflessiaClose");
				other.animation.Play("Idle");
				other.transform.position = transform.position;

				isHolding = true;
			}
			else if(other.GetComponent<IsEnemy>() != null && isHolding == false){
				//other.transform.position = transform.position;
				other.GetComponent<CanMove>().MoveScale -= 1;
				//other.animation.Play("Idle");
				animation.Play("RaflessiaClose");
				other.transform.position = transform.position;

				isHolding = true;

			}
		
			if(timerScript.Finished() == true){
				isHolding = false;

				if(other.GetComponent<IsEnemy>() !=  null || other.tag == "Player")
				other.GetComponent<CanMove>().MoveScale += 1;

				isActive = false;
				//collider.isTrigger = false;
				animation.Play("RaflessiaOpen");
				animation.PlayQueued("RaflessiaClose");
				timerScript.Reset();
			}
		}

	}

	IEnumerator WaitAndCallback(float waitTime){
		yield return new WaitForSeconds(waitTime); 
		//collider.isTrigger = true;
		isActive = true;
		animation.Play("RaflessiaActive");
	}

}
