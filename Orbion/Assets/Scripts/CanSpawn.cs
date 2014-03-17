using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanSpawn : MonoBehaviour {
	/* Handles Enemy Spawning
	*/
	
	// respawnTime - How many seconds for each individual enemy to spawn
	public float respawnTime;
	//limit of how much fog can be on the screen at once
	public int respawnLimit = 40;
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
	
	private int movementCase;
	//actual border 140
	private float mapBorder = 130.0F;

	public float spawnMovement;

	// Set spawn locations and levels
	void Start(){
		//Debug.Log(this.gameObject.transform.position);
		spawnMovement = 80.0F;
		pos = this.gameObject.transform.position;
		pos.y = 2.0F;
		if(pos.x >= 0.0F && pos.z >= 0.0F){
			movementCase = 1;
		} else if(pos.x >= 0.0F && pos.z < 0.0F){
			movementCase = 2;
		} else if(pos.x < 0.0F && pos.z >= 0.0F){
			movementCase = 3;
		} else {
			movementCase = 4;
		}
		//Debug.Log("case " + movementCase);
		//Debug.Log(pos); 		
	}
	
	// M turns wave on and off
	void FixedUpdate(){
		Mtog ();
		if (waveStatus && MetricManager.getFogCount < respawnLimit) {
			
			if(respawnCounter >= respawnTime){
				pos = this.gameObject.transform.position;
				makeFog (pos);
				spawnMove();
				respawnCounter = 0.0F;
			} else {
				respawnCounter += Time.deltaTime;
			}
		}
	}
	
	void Update(){
	}//end of Update
	
	private void spawnMove(){
		Vector3 temp = transform.position;
		//Debug.Log(temp);
		switch (movementCase)
		{
		case 1:
			//Debug.Log("Case 1");
			temp.z -= spawnMovement;
			if(temp.z <= mapBorder * -1.0F) temp.z = mapBorder * -1.0F;
			transform.position = temp;
			if(temp.z <= mapBorder * -1.0F){
				movementCase = 2;
			}
			break;
		case 2:
			//Debug.Log("Case 2");
			temp.x -= spawnMovement;
			if(temp.x <= mapBorder * -1.0F) temp.x = mapBorder * -1.0F;
			transform.position = temp;
			if(temp.x <= mapBorder * -1.0F){
				movementCase = 3;
			}
			break;
		case 3:
			//Debug.Log("Case 3");
			temp.z += spawnMovement;
			if(temp.z >= mapBorder) temp.z = mapBorder;
			transform.position = temp;
			if(temp.z >= mapBorder){
				movementCase = 4;
			}
			break;
		case 4:
			//Debug.Log("Case 4");
			temp.x += spawnMovement;
			if(temp.x >= mapBorder) temp.x = mapBorder;
			transform.position = temp;
			if(temp.x >= mapBorder){
				movementCase = 1;
			}
			break;
		}
	}

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