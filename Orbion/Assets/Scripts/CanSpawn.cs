using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanSpawn : MonoBehaviour {
	/* Handles Enemy Spawning
	*/
	
	// respawnTime - How many seconds for each individual enemy to spawn
	public float respawnTime;
	private float respawnCounter = 0.0F;
	
	//holds fog prefab
	public Rigidbody fogEnemy;
	private Rigidbody clone;
	
	private bool waveStatus = true;	
	//turn wave off/resume
	public void waveResume(){waveStatus = true; Debug.Log ("wave-resummed");}
	public void waveOff(){waveStatus = false; Debug.Log ("wave-stopped");}
	
	//position of spawner
	private Vector3 pos;
	
	// Set spawn locations and levels
	void Start(){
		//Debug.Log(this.gameObject.transform.position);
		pos = this.gameObject.transform.position;
		pos.y = 1.0F;
		//Debug.Log(pos); 		
	}
	
	// M turns wave on and off
	void FixedUpdate(){
		Mtog ();
		if (waveStatus) {
			
			if(respawnCounter >= respawnTime){
				makeFog (pos);
				respawnCounter = 0.0F;
			} else {
				respawnCounter += Time.deltaTime;
			}
		}
	}
	
	void Update(){
	}//end of Update
	
	private void Mtog(){
		if( Input.GetKey(KeyCode.M)){
			if(waveStatus){
				waveOff();
			} else {
				waveResume();
			}
		}
	}
	
	private void makeFog(Vector3 vec){
		//Debug.Log(vec);
		clone = Instantiate (fogEnemy, vec, Quaternion.identity) as Rigidbody;
	}
	
}