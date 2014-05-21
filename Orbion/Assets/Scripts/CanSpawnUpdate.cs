using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanSpawnUpdate : MonoBehaviour {

	//Setup how many levels and at what intervals(Timeline)
	private const int totalLevels = 5;
	private int currentLevel = 0;
	private int levelInit = 0;
	private float[] TimeLine = new float[totalLevels];	
	private float currentTimer;
	private float counter = 0.0F;
	private float currentInterval;
	private float intervalCounter = 0.0F;
	private int summonLimit = 8;
	private int healthIncrease = 0;
	//private Rigidbody clone;
	public Rigidbody meleeEnemy;
	public Rigidbody meleeFastEnemy;
	public Rigidbody rangedEnemy;
	public Rigidbody rangedMultiEnemy;
	public Rigidbody bossEnemy;
	private Rigidbody clone;

	private bool bossSummon = false;

	public Vector3 Vec1 = new Vector3(150.0F,0.0F,-140.0F);
	public Vector3 Vec2 = new Vector3(200.0F,0.0F,50.0F);
	public Vector3 Vec3 = new Vector3(40.0F,0.0F,160.0F);
	
	public Vector3 bossVec = new Vector3(150.0F,0.0F,-140.0F);

	//1-easy 2-med 3-hard
	public int difficulty = 1;
	
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
		//Debug.Log(difficulty);//0.75 2.0 4.0 8.0 12.0
		addLevel (0.75F, 25.0F, 3); addLevel (2.0F, 20.0F, 4); addLevel (4.0F, 20.0F, 5);
		addLevel (8.0F, 20.0F, 6); addLevel (12.0F, 15.0F, 7); 
		TimeLine[0] = levels[0].timer;
		for(int i = 1; i < totalLevels; i++){
			TimeLine[i] = levels[i].timer - levels[i-1].timer;
		}
		//TimeLine[totalLevels-1] = 60.0F;
		for(int i = 0; i < totalLevels; i++){
			//Debug.Log("timeline" + i + ": " + TimeLine[i]);
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
		if(currentLevel > 0 && currentLevel <= totalLevels){
			if (intervalCounter > currentInterval){
				runLevel();
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
			//Debug.Log ("Start Level: " + currentLevel);
			runLevel();
			if(currentLevel == totalLevels){
				currentTimer = 3600.0F;
			} else {
				currentTimer = TimeLine[currentLevel] * 60.0F;
			}
			if(difficulty == 1){
				currentInterval = levels[currentLevel-1].spawnTimer + 5.0F;		
			} else if(difficulty == 2){
				currentInterval = levels[currentLevel-1].spawnTimer;			
			} else {
				currentInterval = levels[currentLevel-1].spawnTimer - 5.0F;
			}
			//Debug.Log("Spawn interval: " + currentInterval); 
			counter = 0.0F;
			intervalCounter = 0.0F;
		}
	}
	private void addLevel(float _timer, float _spawnTimer, int _numSpawn){
		if(levelInit < totalLevels){
			levels[levelInit] = new LevelInfo(_timer, _spawnTimer, _numSpawn);
			levelInit++;
			//Debug.Log("added level: " + levelInit + " timer : " + _timer + " spawntimer : " + _spawnTimer + " num spawned: " + _numSpawn);
			
		}
	}
	public void runLevel(){
		//Debug.Log("Spawn Wave " + currentLevel);
		float rand = Random.value;
		Vector3 tempVec;
		if(rand > 0.0 && rand < 0.33){
			tempVec = Vec1;
		} else if(rand >= 0.33 && rand < 0.66){
			tempVec = Vec2;
		} else {
			tempVec = Vec3;
		}
		int numSpawn;

		if(difficulty == 1){
			numSpawn = levels[currentLevel-1].numSpawn-1;
		} else if(difficulty == 2){
			numSpawn = levels[currentLevel-1].numSpawn;
		} else {
			numSpawn = levels[currentLevel-1].numSpawn+1;
		}
		//Debug.Log(numSpawn); 
		for(int i = 0; i < numSpawn; i++){
			rand = Random.value;
			//Debug.Log (MetricManager.getEnemies);
			if(MetricManager.getEnemies < summonLimit){
				if(rand > 0.0 && rand < 0.3){
					makeMelee(tempVec);
					//SpawnManager.makeMelee(tempVec);
				} else if(rand >= 0.3 && rand < 0.7){
					makeRanged(tempVec);
					//SpawnManager.makeRanged(tempVec);
				} else if(rand >= 0.7 && rand < 0.9){
					makeFastMelee(tempVec);
				} else {
					makeMultiRanged(tempVec);
				}
				MetricManager.AddEnemies(1);
			}
		}
		healthIncrease += 5;
	}
	public void makeMelee(Vector3 _vec){
		clone = Instantiate (meleeEnemy, _vec, Quaternion.identity) as Rigidbody;
		clone.GetComponent<Killable>().increaseHealth(healthIncrease);
	}
	public void makeFastMelee(Vector3 _vec){
		clone = Instantiate (meleeFastEnemy, _vec, Quaternion.identity) as Rigidbody;
		clone.GetComponent<Killable>().increaseHealth(healthIncrease);
	}
	public void makeRanged(Vector3 _vec){
		clone = Instantiate (rangedEnemy, _vec, Quaternion.identity) as Rigidbody;
		clone.GetComponent<Killable>().increaseHealth(healthIncrease);
	}
	public void makeMultiRanged(Vector3 _vec){
		clone = Instantiate (rangedMultiEnemy, _vec, Quaternion.identity) as Rigidbody;
		clone.GetComponent<Killable>().increaseHealth(healthIncrease);
	}
	public void spawnBoss(Vector3 _vec){
		clone = Instantiate (bossEnemy, _vec, Quaternion.identity) as Rigidbody;
		clone.GetComponent<Killable>().increaseHealth(healthIncrease);
	}

}