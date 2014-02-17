﻿using UnityEngine;
using System.Collections;

public class AbsorbBullet : MonoBehaviour {

	public CanShoot shootScript;
	public Rigidbody shield;
	public Rigidbody returnBullet;
	private Rigidbody clone;
	private Rigidbody clone2;
	public Vector3 dir;
	public int bulletCount = 0;

	private bool fullCharge;
	private bool isOut;
	private float timer = 0.0f;
	private float cooldown = 2.0f;

	// Use this for initialization
	void Start () {
		isOut = false;
		fullCharge = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (bulletCount);
		if(isOut){
			if(clone != null){
			clone.transform.position = transform.position + dir * 2;
			}
			timer += Time.deltaTime;
		}
			if(timer > cooldown){
			if(clone != null){
			Destroy (clone.gameObject);
			}
			isOut = false;
			timer = 0.0f;
		}
	
	}
	public void AbsorbShot(Vector3 target){
		dir = target - transform.position;
		dir.Normalize();


		if(bulletCount < 5){

			if( shootScript.FinishCooldown() && !isOut){
				
				clone = Instantiate(shield, transform.position + dir * 2, Quaternion.LookRotation(dir, Vector3.up)) as Rigidbody;
				//clone.transform.position = transform.position + dir * 2;
				shootScript.ResetFiringTimer();
				isOut = true;

				
				
			}

		}
		else{


			if( shootScript.FinishCooldown() && !isOut){
				
				clone2 = Instantiate(returnBullet, transform.position + dir * 2, Quaternion.LookRotation(dir, Vector3.up)) as Rigidbody;

				shootScript.ResetFiringTimer();
				isOut = true;
				bulletCount = 0;

			}

		}
		
	}


	
}