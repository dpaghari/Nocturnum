using UnityEngine;
using System.Collections;

public class IsEnemy : MonoBehaviour {
	
	void Start(){
		if(gameObject.name != "base_enemy_prefab"){
			MetricManager.AddEnemies(1);
		}
	}

	public void explode(){
		this.GetComponent<Killable>().kill();
	}
	//Dummy script to be attached to all enemies so that other scripts can check if a gameobject is an enemy or not.
}
