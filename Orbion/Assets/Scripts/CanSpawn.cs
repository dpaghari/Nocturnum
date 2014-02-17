using UnityEngine;
using System.Collections;

public class CanSpawn : MonoBehaviour {
	/* Handles Enemy Spawning
		M to turn it off N to turn it on
	*/
	// respawnTime - How many seconds until it spawn
	private float respawnTime = 5.0F;
	private float respawnCounter = 0.0F;
	// waveIncrease - How many seconds until wave gets stronger
	// not implemented
	private float waveIncrease = 40.0F;
	private float waveCounter = 0.0F;
	
	private bool waveOnStatus = true;
	
	private Rigidbody clone;
	private Rigidbody clone2;

	private Vector3 vec;
	public Rigidbody meleeEnemy;
	public Rigidbody rangedEnemy;
	
	//turn wave on or off
	public void waveOff(){waveOnStatus = false; respawnCounter = 0.0F;}
	public void waveOn(){waveOnStatus = true;}
	
	
	// Set location
	void Start(){
		vec = this.transform.position;
		this.renderer.material.color = Color.blue;
	}
	
	// M turns wave on N turns it off
	void FixedUpdate(){
		
		if( Input.GetKey(KeyCode.M)){waveOff();}
		
		if( Input.GetKey(KeyCode.N)){waveOn();}
		
	}
	
	// If wave status on spit out enemies
	void Update(){
		
		if(waveOnStatus){
			if(respawnCounter > respawnTime){
				clone = Instantiate(meleeEnemy, vec, Quaternion.identity) as Rigidbody;
				clone2 = Instantiate(rangedEnemy, vec, Quaternion.identity) as Rigidbody;
				respawnCounter = 0.0F;
			} else {
				respawnCounter += Time.deltaTime;
			}
		}
	}
	
	
	
	
}
