using UnityEngine;
using System.Collections;

public class MetricManager : Singleton<MetricManager> {
	
	protected MetricManager() {} // guarantee this will be always a singleton only - can't use the constructor!
	
	protected int[] TotalBuildings;
	

	private int _shotsFired = 0;
	private int _enemiesKilled = 0;
	private int _totalEnemies = 0;
	private int _totalWaves = 0;
	private int _enemiesHit = 0;
	private int _buildingsHit = 0;
	private int _buildingsHealed = 0;
	private float _completionTime = 0.0f;
	private float _totalScore = 0.0f;

	public static float getScore{ get { return Instance._totalScore;}}

	public static void setCompletionTime(float amt){ Instance._completionTime = amt;}
	public static float getCompletionTime{ get { return Instance._completionTime;}}

	public static void AddBuildingsHealed(int amt){ Instance._buildingsHealed += amt;}
	public static int getBuildingsHealed{ get { return Instance._buildingsHealed;}}

	public static void AddBuildingsHit(int amt){ Instance._buildingsHit += amt;}
	public static int getBuildingsHit{ get { return Instance._buildingsHit;}}

	public static void AddEnemiesHit(int amt){ Instance._enemiesHit += amt;}
	public static int getEnemiesHit{ get { return Instance._enemiesHit;}}

	public static void AddShots(int amt){ Instance._shotsFired += amt;}
	public static int getShotsFired{ get { return Instance._shotsFired;}}

	public static void AddWaves(int amt){ Instance._totalWaves += amt;}
	public static int getTotalWaves{ get { return Instance._totalWaves;}}

	public static void AddEnemiesKilled(int amt){ Instance._enemiesKilled += amt;}
	public static int getEnemiesKilled{ get { return Instance._enemiesKilled;}}

	



	public static void AddEnemies(int amt){ 
		Instance._totalEnemies += amt;
		if(Instance._totalEnemies < 0){
			Instance._totalEnemies = 0;
		}
	}
	public static int getEnemies{ get { return Instance._totalEnemies;}}

	private static float getAccuracy(){
		//Debug.Log("enemies hit " + getEnemiesHit);
		//Debug.Log("buildings hit " + getBuildingsHit);
		//Debug.Log("shots fired " + getShotsFired);
		//return (float)((getEnemiesHit+getBuildingsHit) / getShotsFired);
		int totalHits = getEnemiesHit + getBuildingsHit;
		//Debug.Log("total hits " + totalHits);
		float accuracy = (float)totalHits / (float)getShotsFired;
		//Debug.Log("accuracy " + accuracy);
		return accuracy;
		//return 1.0f;
		//return getEnemiesHit; 
	}

	public static void calculateScore(){
		Instance._totalScore = 0.0f;
		float part1 = getAccuracy() * 33.3f;
		float part2;
		if(Instance._completionTime < 600.0f){	
			part2 = 33.3f;
		} else {
			part2 = 10.0f;
		}
		float part3 = 33.3f - (TechManager.GetTotalBuildingsDestroyed() * 2);
		if(part3 < 0.0f) part3 = 0.0f;

		Instance._totalScore = part1 + part2 + part3 + 1.0f;
		
	}	

	public static void Reset(){
		Instance._shotsFired = 0;
		Instance._enemiesKilled = 0;
		Instance._totalEnemies = 0;
		Instance._totalWaves = 0;
		Instance._enemiesHit = 0;
		Instance._completionTime = 0.0f;
		Instance._buildingsHit = 0;
		Instance._buildingsHealed = 0;
		Instance._totalScore = 0.0f;
	}
	/*
	// Use this for initialization
	void Start () {
		
	}
	*/
	// Update is called once per frame
	void Update () {
		//Debug.Log("buildings destroyed " + TechManager.GetTotalBuildingsDestroyed());
		//Debug.Log("buildings built " + TechManager.GetTotalBuildings());
		//Debug.Log("builds alive " + TechManager.GetNumBuilding);
		
		//float f = getAccuracy();
		//Debug.Log(Time.time);
		//Debug.Log("accuracy " + getAccuracy());
		
	}

	
}
