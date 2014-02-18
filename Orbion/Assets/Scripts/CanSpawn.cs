using UnityEngine;
using System.Collections;

public class CanSpawn : MonoBehaviour {
	/* Handles Enemy Spawning
		M to turn it off and on
	*/
	// respawnTime - How many seconds until it spawn
	private float respawnTime = 10.0F;
	private float respawnCounter = 0.0F;
	// waveStart determines when we start spawning
	private float waveStart = 30.0F;
	private float waveCounter = 0.0F;
	
	private bool waveOnStatus = false;
	
	private Rigidbody clone;
	private Rigidbody clone2;

	private Vector3 vec;
	public Rigidbody meleeEnemy;
	public Rigidbody rangedEnemy;
	
	public int totalEnemies = 0;
	//turn wave on or off
	public void waveOff(){waveOnStatus = false; respawnCounter = 0.0F;}
	public void waveOn(){waveOnStatus = true;}
	
	
	// Set location
	void Start(){
		vec = this.transform.position;
		this.renderer.material.color = Color.blue;
	}
	
	// M turns wave on and off
	void FixedUpdate(){
		
		if( Input.GetKey(KeyCode.M)){
			if(waveOnStatus){
				waveOff();
				Debug.Log("turn off wave");
			} else {
				waveOn();
				Debug.Log("turn on wave");
			}
		}
	}
	
	// If wave status on spit out enemies
	void Update(){
		
		//Too many enemies stop spawning
		if(totalEnemies > 7){
			waveOff ();
			totalEnemies = 0;
		}
		
		if(waveCounter > waveStart){
			waveOn();
			waveCounter = 0.0F;
		} else if(!waveOnStatus){
			waveCounter += Time.deltaTime;
		}

		if(waveOnStatus){
			if(respawnCounter > respawnTime){
				clone = Instantiate(meleeEnemy, vec, Quaternion.identity) as Rigidbody;
				clone2 = Instantiate(rangedEnemy, vec, Quaternion.identity) as Rigidbody;
				totalEnemies += 2;
				respawnCounter = 0.0F;
			} else {
				respawnCounter += Time.deltaTime;
			}
		}
	}

}