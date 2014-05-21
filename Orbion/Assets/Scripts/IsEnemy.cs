using UnityEngine;
using System.Collections;

public enum EnemyType{
	none = 0,
	wolf,
	alpha_wolf,
	luminotoad,
	luminosaur,
	zingbat
}


public class IsEnemy : MonoBehaviour {

	public EnemyType enemyType;
	
	void Start(){
		if(gameObject.name != "base_enemy_prefab"){
			//MetricManager.AddEnemies(1);
		}
	}
	//Dummy script to be attached to all enemies so that other scripts can check if a gameobject is an enemy or not.
}
