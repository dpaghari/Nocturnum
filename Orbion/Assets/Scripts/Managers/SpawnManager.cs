using UnityEngine;
using System.Collections;

public class SpawnManager : Singleton<SpawnManager> {
	
	protected SpawnManager() {} // guarantee this will be always a singleton only - can't use the constructor!
/*
	public GameObject fuck;
	public static Rigidbody meleeEnemy;
	public static Rigidbody rangedEnemy;
	public static Rigidbody bossEnemy;
	private Rigidbody clone;
	
	public static void makeMelee(Vector3 _vec){
		clone = Instantiate (meleeEnemy, _vec, Quaternion.identity) as Rigidbody;
	}
	
	public static void makeRanged(Vector3 _vec){
		clone = Instantiate (rangedEnemy, _vec, Quaternion.identity) as Rigidbody;
	}
	
	public static void spawnBoss(Vector3 _vec){
		clone = Instantiate (bossEnemy, _vec, Quaternion.identity) as Rigidbody;
	}

*/
}
