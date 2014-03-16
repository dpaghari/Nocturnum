using UnityEngine;
using System.Collections;

public class MetricManager : Singleton<MetricManager> {
	
	protected MetricManager() {} // guarantee this will be always a singleton only - can't use the constructor!
	
	private int _shotsFired = 0;
	private int _enemiesKilled = 0;

	public static void AddShots(int amt){ Instance._shotsFired += amt;}
	public static int getShotsFired{ get { return Instance._shotsFired;}}

	public static void AddEnemiesKilled(int amt){ Instance._enemiesKilled += amt;}
	public static int getEnemiesKilled{ get { return Instance._enemiesKilled;}}

	private int _totalFog = 0;


	public static void AddFog(int amt){ Instance._totalFog += amt;}
	public static int getFogCount{ get { return Instance._totalFog;}}


	/*
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	*/
}
