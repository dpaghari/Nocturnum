using UnityEngine;
using System.Collections;

public class MetricManager : Singleton<MetricManager> {
	
	protected MetricManager() {} // guarantee this will be always a singleton only - can't use the constructor!
	
	private int _shotsFired = 0;
	private int _enemiesKilled = 0;
	private int _totalEnemies = 0;

	public static void AddShots(int amt){ Instance._shotsFired += amt;}
	public static int getShotsFired{ get { return Instance._shotsFired;}}

	public static void AddEnemiesKilled(int amt){ Instance._enemiesKilled += amt;}
	public static int getEnemiesKilled{ get { return Instance._enemiesKilled;}}

	public static void AddEnemies(int amt){ 
		Instance._totalEnemies += amt;
		if(Instance._totalEnemies < 0){
			Instance._totalEnemies = 0;
		}
	}
	public static int getEnemies{ get { return Instance._totalEnemies;}}


	public static void Reset(){
		Instance._shotsFired = 0;
		Instance._enemiesKilled = 0;
		Instance._totalEnemies = 0;
	}
	/*
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	*/
}
