﻿using UnityEngine;
using System.Collections;

public class LunaAnimation : MonoBehaviour {


	public CanShootReload shootScript;
	public hasOverdrive overdriveScript;


	// Use this for initialization
	void Start () {
	
	}
	
	bool MoveKeyDown(){
		//if(!GameManager.AvatarContr.isPaused)
		return Input.GetKeyDown( KeyCode.W) || Input.GetKeyDown( KeyCode.S) || Input.GetKeyDown( KeyCode.A) || Input.GetKeyDown( KeyCode.D);
	}

	bool MoveKeyUp(){
		//if(!GameManager.AvatarContr.isPaused)
		return Input.GetKeyUp( KeyCode.W) || Input.GetKeyUp( KeyCode.S) || Input.GetKeyUp( KeyCode.A) || Input.GetKeyUp( KeyCode.D);
	}

	bool MoveKeyStay(){
		//if(!GameManager.AvatarContr.isPaused)
		return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||  Input.GetKey(KeyCode.S) ||  Input.GetKey(KeyCode.D);
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
					//if(!GetComponent<AvatarController>().isPaused)
					newRotation += Vector3.forward;
					
						//animation.CrossFade("Run");
				}
				if (Input.GetKey(KeyCode.S)){
					//if(!GetComponent<AvatarController>().isPaused)
					newRotation += Vector3.back;
					
						//animation.CrossFade("Run");

				}
				
				if (Input.GetKey(KeyCode.A)){
					//if(!GetComponent<AvatarController>().isPaused)
					newRotation += Vector3.left;

						//animation.CrossFade("Run");

				}
				if (Input.GetKey(KeyCode.D)){
					//if(!GetComponent<AvatarController>().isPaused)
					newRotation += Vector3.right;

						//animation.CrossFade("Run");

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
					if(!animation.IsPlaying("ShootRun"))
						animation.Play("Run");
			}
			if(MoveKeyStay() && Input.GetMouseButton(0)){
					animation.Play("ShootRun");
			}
			//only change our position if there is an update to it
			if (newRotation != Vector3.zero) transform.forward = newRotation;
			}


			if (! MoveKeyStay() && ! Input.GetMouseButton(0)){
				if(!animation.IsPlaying("Groundpunch"))
				animation.CrossFade("Idle");
		 	}
		}

	
	}
}
