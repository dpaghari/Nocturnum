using UnityEngine;
using System.Collections;

public class FlowerAnimation : MonoBehaviour {

	private bool isActive;
	private bool isHolding;
	private DumbTimer timerScript;
	// Use this for initialization
	void Start () {
		timerScript = DumbTimer.New(3.0f, 1.0f);
		isActive = false;
		isHolding = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(isHolding){

			timerScript.Update();

		}



	}

	void OnCollisionEnter(Collision other){


		if(!isActive){
			if(other.gameObject.tag == "playerBullet" || other.gameObject.tag == "enemy_bullet"){
		
				animation.Play("RaflessiaOpen");
				StartCoroutine(WaitAndCallback(animation["RaflessiaOpen"].length));
		
			}
		}
	}

	void OnTriggerStay(Collider other){



		if(isActive){
			if(other.tag == "Player" && isHolding == false){
				//other.transform.position = transform.position;
				GameManager.KeysEnabled = false;
				animation.Play("RaflessiaClose");
				isHolding = true;
			}
			if(other.GetComponent<IsEnemy>() != null && isHolding == false){
				//other.transform.position = transform.position;
				other.GetComponent<CanMove>().MoveScale -= 1;
				animation.Play("RaflessiaClose");
				isHolding = true;

			}
			if(isHolding){

				other.transform.position = transform.position;
			}

			if(timerScript.Finished() == true){
				isHolding = false;
				if(other.tag == "Player"){
					GameManager.KeysEnabled = true;

				}
				if(other.GetComponent<IsEnemy>() != null){
					other.GetComponent<CanMove>().MoveScale += 1;
				}
				isActive = false;
				collider.isTrigger = false;
				animation.Play("RaflessiaOpen");
				timerScript.Reset();
			}
		}

	}

	IEnumerator WaitAndCallback(float waitTime){
		yield return new WaitForSeconds(waitTime); 
		collider.isTrigger = true;
		isActive = true;
		animation.Play("RaflessiaActive");
	}

}
