using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {


	public float corruption = 0.0f;
	private float corruptLimit = 100.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(corruption > 0.0f){
			//Debug.Log (corruption);
			corruption -= Time.deltaTime;

		}
	
	}

	// Updates HP based on damage taken, calls kill() on dead objects
	public void damage (float dmg) {

		corruption += dmg;

		if (corruption >= corruptLimit) kill();
		if(gameObject.GetComponent<IsDamagedEffect>() != null){
			gameObject.GetComponent<IsDamagedEffect>().addDamage();
		}
	}

	// Kills enemy or player
	void kill () {
			animation.CrossFade("Dead");
			ResManager.Reset();
			TechManager.Reset();
			Application.LoadLevel("scene1");

	}

	void OnTriggerStay(Collider other){

		if(other.tag == "Generator"){
			corruption = 0.0f;
			//Debug.Log (canDeposit);
		}
	
	}
}
