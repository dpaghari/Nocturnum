using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanSpawnUpdate : MonoBehaviour {

	//Setup how many levels and at what intervals(Timeline)
	private const int totalLevels = 3;
	private int currentLevel = 0;
	private int levelInit = 0;
	private float[] TimeLine = new float[totalLevels];	
	private float currentTimer;
	private float counter = 0.0F;
	private float currentInterval;
	private float intervalCounter = 0.0F;
	//private Rigidbody clone;
	public Rigidbody meleeEnemy;
	public Rigidbody rangedEnemy;
	public Rigidbody bossEnemy;
	private Rigidbody clone;

	private bool bossSummon = false;

	public Vector3 testVec = new Vector3(20.0F,0.0F,-20.0F);
	public Vector3 bossVec = new Vector3(120.0F,0.0F,-120.0F);
	
	public class LevelInfo{
		public float timer;
		public float spawnTimer;
		public int numMeleeSpawn;
		public int numRangedSpawn;
		
		public LevelInfo(float _timer, float _spawnTimer, int _numMeleeSpawn, int _numRangedSpawn){
			timer = _timer;
			spawnTimer = _spawnTimer;
			numMeleeSpawn = _numMeleeSpawn;
			numRangedSpawn = _numRangedSpawn;
		}
	}

	public LevelInfo[] levels = new LevelInfo[totalLevels];

	void Start(){

		addLevel (1.0F, 30.0F, 3, 0); addLevel (5.0F, 20.0F, 3, 2); addLevel (10.0F, 20.0F, 4, 2); 
		TimeLine[0] = levels[0].timer;
		for(int i = totalLevels-1; i > 0; i--){
			
			TimeLine[i] = levels[i].timer - levels[i-1].timer;

		}
		currentTimer = TimeLine[currentLevel] * 60.0F;
	}
	
	void FixedUpdate(){
	}
	
	void Update(){
		if(!bossSummon && TechManager.hasWolves){
			//Debug.Log ("spawn boss");
			bossSummon = true;
			spawnBoss (bossVec);
		}
		
		if(currentLevel > 0){
			if (intervalCounter > currentInterval){
			//	Debug.Log("Run level: " + currentLevel);
				runLevel(testVec);
				intervalCounter = 0.0F;
			} else {
				intervalCounter += Time.deltaTime;
			}
		}
		
		if(currentLevel <= totalLevels){
			if (counter > currentTimer){
				nextLevel();			
			} else {
				counter += Time.deltaTime;
			}
		}

		

	
	}

	//updates timer
	public void nextLevel(){
		if(currentLevel < totalLevels){
			currentLevel++;
//			Debug.Log ("Current Level: " + currentLevel);
			runLevel (testVec);
			if(currentLevel == totalLevels){
				currentTimer = 3600.0F;
			} else {
				currentTimer = TimeLine[currentLevel] * 60.0F;
			}
			currentInterval = levels[currentLevel-1].spawnTimer;
			counter = 0.0F;
			intervalCounter = 0.0F;
		}
	}

	private void addLevel(float _timer, float _spawnTimer, int _numMeleeSpawn, int _numRangedSpawn){
		if(levelInit < totalLevels){
			levels[levelInit] = new LevelInfo(_timer, _spawnTimer, _numMeleeSpawn, _numRangedSpawn);
			levelInit++;
		}
	}

	public void runLevel(Vector3 _vec){
		for(int i = 0; i < levels[currentLevel-1].numMeleeSpawn; i++){
			makeMelee(_vec);
		}
		for(int i = 0; i < levels[currentLevel-1].numRangedSpawn; i++){
			makeRanged(_vec);
		}
	}
	
	public void makeMelee(Vector3 _vec){
		clone = Instantiate (meleeEnemy, _vec, Quaternion.identity) as Rigidbody;
	}
	
	public void makeRanged(Vector3 _vec){
		clone = Instantiate (rangedEnemy, _vec, Quaternion.identity) as Rigidbody;
	}

	public void spawnBoss(Vector3 _vec){
		clone = Instantiate (bossEnemy, _vec, Quaternion.identity) as Rigidbody;
	}

}