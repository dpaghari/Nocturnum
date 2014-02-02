using UnityEngine;
using System.Collections;

public class CanSpawn : MonoBehaviour {

	private int respawnTimer = 0;
	private int resetTime = 300;
	private Rigidbody clone;
	private Vector3 vec;
	public Rigidbody enemySpawn;

	// Use this for initialization
	void Start () {
		vec.Set(-6.0F, 1.0F, -30.0F);
	}
	
	// Update is called once per frame
	void Update () {
		if(respawnTimer > resetTime){
			clone = Instantiate(enemySpawn, vec, Quaternion.identity) as Rigidbody;
			respawnTimer = 0;
		} else {
			respawnTimer++;
		}
	
	}
}
