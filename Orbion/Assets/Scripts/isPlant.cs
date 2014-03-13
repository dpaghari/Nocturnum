using UnityEngine;
using System.Collections;

public class isPlant : MonoBehaviour {

	public bool isLit;
	public float fadeTimer;
	public float fadeCooldown;

	// Use this for initialization
	void Start () {
		isLit = false;
		fadeCooldown = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {

		playAnimation(isLit);

	}

	void playAnimation (bool isLit){

		if(isLit){
			if(GameManager.AvatarContr.lightconeObj.light.enabled == true)
			animation.CrossFade("open");
		}
		else{
			fadeTimer += Time.deltaTime;

			if(fadeTimer > fadeCooldown){
				animation.CrossFade("close");
				fadeTimer = 0.0f;
			}
		}
	}

}
