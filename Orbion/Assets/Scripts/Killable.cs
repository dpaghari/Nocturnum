using UnityEngine;
using System.Collections;

public class Killable : MonoBehaviour {
	public float currHP; 
	public float baseHP = 10;
	public GameObject deathTarget;
	// Use this for initialization
	void Start () {
		currHP = baseHP;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void damage (float dmg) {
		currHP -= dmg;
		Debug.Log("Obj: " + this.gameObject.name + "CurrHP = " + currHP);
		if (currHP <= 0.0) {
			if(this.gameObject.name == "enemy_prefab"){
				kill();
			} else {
				Debug.Log("Player dead!!");
			}
		}
	}
	
	void kill () {
		//destory object
		Destroy (this.gameObject);
		//make death object
	}
	
}