using UnityEngine;
using System.Collections;

public class isPoisonPlant : MonoBehaviour {
	
	public bool isLit;
	public bool isActive;
	public bool ballExists;
	public float fadeTimer;
	public float fadeCooldown;
	public float activeCounter;
	public float activeThresh;
	public Rigidbody poisonBall;
	private Rigidbody clone;
	public isPoisonBall poisonScript;



	// Use this for initialization
	void Start () {
		ballExists = false;
		isActive = false;
		isLit = false;
		fadeCooldown = 10.0f;
		fadeTimer = 0.0f;
		activeCounter = 0.0f;
		activeThresh = 100.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(clone != null)
		Debug.Log (clone.GetComponent<isPoisonBall>().popped);


		playAnimation(isLit);

	}
	
	void playAnimation (bool isLit){

		if(isLit){
			fadeTimer += Time.deltaTime;
			if(fadeTimer > fadeCooldown){
				animation.CrossFade("RaflessiaClose");
			    fadeTimer = 0.0f;
				isLit = false;
			}
		}
		if(clone != null){
			if(clone.GetComponent<isPoisonBall>().popped){
				ballExists = false;
				clone.GetComponent<isPoisonBall>().destroySelf();
			}
		}
		if(isActive){
			Vector3 temp = transform.position;
			temp.y += 2;
			//Debug.Log("inPlant:" + ballExists);
			if(!ballExists){

				clone = Instantiate(poisonBall, temp, Quaternion.identity) as Rigidbody;
				ballExists = true;
				//Debug.Log ("Creating a poisonball");
			}

			isActive = false;


		}

	}
}