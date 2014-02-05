using UnityEngine;
using System.Collections;

public class Killable : MonoBehaviour {
	/*
	Gives prefabs HP, they can take damage, and die
	*/
	//currHP tracks health, baseHP is given unless changed in the prefab
	public int currHP = 100; 
	public int baseHP = 100;
	public GameObject deathTarget;

	// Set HP to default
	void Start () {
		currHP = baseHP;
	}
	void Update () {
		//Debug.Log("Obj: " + this.gameObject.name + "CurrHP = " + currHP);
	}
	// Updates HP based on damage taken, calls kill() on dead objects
	public void damage (int dmg) {
		currHP -= dmg;
		if (currHP <= 0) {
			kill();
		}
	}
	// Kills enemy or player
	void kill () {
		//destory object
		if(this.gameObject.tag == "Enemy"){
		Destroy (this.gameObject);
		Instantiate (deathTarget, this.transform.position, this.transform.rotation);
		}
		if(this.gameObject.tag == "Player"){
			ResManager.Reset();
			TechManager.Reset();
			Application.LoadLevel("scene1");
		}
		//make death object
	}
}