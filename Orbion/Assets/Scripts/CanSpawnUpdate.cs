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
	private int summonLimit = 10;
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
		public int numSpawn;
		
		public LevelInfo(float _timer, float _spawnTimer, int _numSpawn){
			timer = _timer;
			spawnTimer = _spawnTimer;
			numSpawn = _numSpawn;
		}
	}

	public LevelInfo[] levels = new LevelInfo[totalLevels];

	void Start(){

		addLevel (0.75F, 30.0F, 2); addLevel (2.0F, 25.0F, 3); addLevel (4.0F, 20.0F, 4);
		addLevel (8.0F, 20.0F, 5); addLevel (12.0F, 15.0F, 6); 
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

	private void addLevel(float _timer, float _spawnTimer, int _numSpawn){
		if(levelInit < totalLevels){
			levels[levelInit] = new LevelInfo(_timer, _spawnTimer, _numSpawn);
			levelInit++;
		}
	}

	public void runLevel(Vector3 _vec){
		for(int i = 0; i < levels[currentLevel-1].numSpawn; i++){
			float rand = Random.value;

			//Debug.Log (MetricManager.getEnemies);
			if(MetricManager.getEnemies < summonLimit){
				if(rand > 0.0 && rand < 0.5){
					makeMelee (testVec);
				} else {
					makeRanged (testVec);
				}
				MetricManager.AddEnemies(1);
			}
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