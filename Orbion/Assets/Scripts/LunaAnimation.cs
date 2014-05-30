using UnityEngine;
using System.Collections;

public class LunaAnimation : MonoBehaviour {


	public CanShootReload shootScript;
	public hasOverdrive overdriveScript;


	// Use this for initialization
	void Start () {
	
	}
	
	bool MoveKeyDown(){

		return Input.GetKeyDown( KeyCode.W) || Input.GetKeyDown( KeyCode.S) || Input.GetKeyDown( KeyCode.A) || Input.GetKeyDown( KeyCode.D);
	}

	bool MoveKeyUp(){

		return Input.GetKeyUp( KeyCode.W) || Input.GetKeyUp( KeyCode.S) || Input.GetKeyUp( KeyCode.A) || Input.GetKeyUp( KeyCode.D);
	}

	bool MoveKeyStay(){

		return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||  Input.GetKey(KeyCode.S) ||  Input.GetKey(KeyCode.D);
	}

	IEnumerator WaitAndIdle(float waitTime){
		yield return new WaitForSeconds(waitTime); 
		animation.CrossFade("Idle");
		

	}

	IEnumerator WaitAndRun(float waitTime){
		yield return new WaitForSeconds(waitTime); 
		animation.CrossFade("Run");


	}

	// Update is called once per frame
	void Update () {

		if(GameManager.KeysEnabled){
			//Want to update it if a key was let go else it won't 
			//change a diagonal if one of the keys were let go
			if ( MoveKeyDown() || MoveKeyUp() || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)){
			Vector3 newRotation = Vector3.zero;

			if(!GameManager.AvatarContr.isPaused){

				if (Input.GetKey(KeyCode.W)){
					
						if(!GameManager.AvatarContr.isDashing)
						newRotation += Vector3.forward;
					

				}
				if (Input.GetKey(KeyCode.S)){
					
						if(!GameManager.AvatarContr.isDashing)
						newRotation += Vector3.back;
					


				}
				
				if (Input.GetKey(KeyCode.A)){
					
						if(!GameManager.AvatarContr.isDashing)
						newRotation += Vector3.left;



				}
				if (Input.GetKey(KeyCode.D)){
					
						if(!GameManager.AvatarContr.isDashing)
						newRotation += Vector3.right;



				}
			}

			if (Input.GetMouseButton(0)){
				
				//animation.CrossFade("Shooting");
				if(!GetComponent<AvatarController>().isPaused){
				Vector3 temp;
				temp = Utility.GetMouseWorldPos(transform.position.y) - transform.position;
				newRotation = temp;
				}
			}


			if (MoveKeyStay()){
					if(animation.IsPlaying("Dash")){

						StartCoroutine(WaitAndRun(animation["Dash"].length)); 
						GameManager.KeysEnabled = true;

						//animation.CrossFade("Run");
					}
					else if(animation.IsPlaying("ShootRun")){
						StartCoroutine(WaitAndRun(animation["ShootRun"].length));
						//animation.CrossFade("Run");
					}
					else
						animation.CrossFade("Run");

			}

			if(MoveKeyStay() && Input.GetMouseButton(0)){
					//if(!animation.isPlaying)
					animation.CrossFade("ShootRun");
			}
			//only change our position if there is an update to it
			if (newRotation != Vector3.zero) transform.forward = newRotation;
			}


			if (! MoveKeyStay() && ! Input.GetMouseButton(0)){

				if(animation.IsPlaying("Groundpunch")){
					StartCoroutine(WaitAndIdle(animation["Groundpunch"].length));
				}
				else if(animation.IsPlaying("Dash")){
					StartCoroutine(WaitAndIdle(animation["Dash"].length));
					GameManager.KeysEnabled = true;

					
				}
				else
					animation.CrossFade("Idle");
		 	}
		}

	
	}
}
