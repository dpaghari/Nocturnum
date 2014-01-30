using UnityEngine;
using System.Collections;

public class Killable : MonoBehaviour {
	public int currHP; 
	public int baseHP = 30;
	public GameObject deathTarget;
	// Use this for initialization
	void Start () {
		currHP = baseHP;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Obj: " + this.gameObject.name + "CurrHP = " + currHP);
	}
	
	public void damage (int dmg) {
		currHP -= dmg;

		if (currHP <= 0) {
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