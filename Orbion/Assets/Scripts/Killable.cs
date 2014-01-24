using UnityEngine;
using System.Collections;

public class Killable : MonoBehaviour {
	//private double maxHP;
	private double currHP; 
	private double dmg = 5;
	public double baseHP = 10;
	public GameObject deathTarget;
	// Use this for initialization
	void Start () {
		currHP = baseHP;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void damage (double dmg) {
		currHP -= dmg;
		if (currHP <= 0.0) {
			kill();
		}
	}
	
	void kill () {
		//destory object
		//make death object
	}
	
}

