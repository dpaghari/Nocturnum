using UnityEngine;
using System.Collections;

public class CanSpawn : MonoBehaviour {
	/*
	Handles enemy spawning
	*/
	// respawnTime - How many seconds until it spawns
	private float respawnTime = 5.0F;
	private float respawnCounter = 0.0F;
	private Rigidbody clone;
	private Vector3 vec;
	public Rigidbody enemySpawn;

	// Set location
	void Start () {
		vec = this.transform.position;
		//Debug.Log (this.transform.position);

		this.renderer.material.color = Color.blue;
		//vec.Set(-6.0F, 1.0F, -30.0F);
	}
	// After respawnTime make an enemy at set location
	void Update () {
		if(respawnCounter > respawnTime){
			clone = Instantiate(enemySpawn, vec, Quaternion.identity) as Rigidbody;
			respawnCounter = 0.0F;
		} else {
			respawnCounter += Time.deltaTime;
		}
	}
}
