using UnityEngine;
using System.Collections;

public class isPoisonPlant : MonoBehaviour {
	
	public bool isLit;
	public bool isActive;
	public float fadeTimer;
	public float fadeCooldown;
	public float activeCounter;
	public float activeThresh;
	
	// Use this for initialization
	void Start () {
		isLit = false;
		fadeCooldown = 10.0f;
		fadeTimer = 0.0f;
		activeCounter = 0.0f;
		activeThresh = 100.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		playAnimation(isLit);
			
			
	}
	
	void playAnimation (bool isLit){
		/* Checks if the player is shining a light at the plant or not
 		 * If it is it increments a counter which then activates when the threshold is reached
 		 * forcing the animation of the plant to open.
 		 * If the player no longer has the light on the plant, it will start a timer that and when its
 		 * cooldown is reached the plant becomes inactive and forces a close animation
 		 */
			
		/*
		if(isLit){					
				//if(GameManager.AvatarContr.lightconeObj.light.enabled == true)
					//animation.CrossFade("open");
					activeCounter += Time.deltaTime * 30;
					if(activeCounter > activeThresh){
					animation.CrossFade("RaflessiaOpen");
					isActive = true;
					isLit = false;
					}
			}
			*/
			if(isLit){
				fadeTimer += Time.deltaTime;
				//Debug.Log(isLit);

					if(fadeTimer > fadeCooldown){
						animation.CrossFade("RaflessiaClose");
						fadeTimer = 0.0f;
						isLit = false;
						//Debug.Log (isLit);
	
					}
	
			
			}
	}
}