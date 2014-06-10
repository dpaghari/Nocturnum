using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanSpawnUpdate : MonoBehaviour {

	//health scaling
	private int healthIncrease = 0;
	private int healthIncreaseCounter;

	//when to turn on waves
	public DumbTimer startScript;
	public float startTime;
	//time between waves
	public DumbTimer waveScript;
	public float waveTime;
	//when to update numspawned and
	public DumbTimer levelScript;
	public float levelTime;
	
	//enemy spawning prefabs
	public Rigidbody meleeEnemy;
	public Rigidbody meleeFastEnemy;
	public Rigidbody luminosaurEnemy;
	public Rigidbody luminotoadEnemy;
	public Rigidbody rangedEnemy;
	public Rigidbody rangedMultiEnemy;
	public Rigidbody bossEnemy;
	private Rigidbody clone;
	//boss stuff
	private bool bossSummon = false;
	public Vector3 bossVec = new Vector3(150.0F,0.0F,-140.0F);
	//turn wave on and off
	public bool waveStatus = false;
	//1-easy 2-med 3-hard
	public int difficulty;
	//number of enemies to be spawned
	private int numSpawn = 2;
	//holds vector locations of all spawn_location prefabs in the scene
	private Vector3[] spawnLocations;

	void Start(){

		startTime = 10.0f;
		waveTime = 20.0f;
		levelTime = 20.0f;
		startScript = DumbTimer.New (startTime, 1.0f);
		waveScript = DumbTimer.New (waveTime, 1.0f);
		levelScript = DumbTimer.New (levelTime, 1.0f);
		FindAllSpawners(Mathf.Infinity);	
		setDifficulty ();	
	}
	
	void FixedUpdate(){
	}

	public void setWaveTime(float f){
		waveTime = f;
	}

	public void setDifficulty(){
		difficulty = GameManager.GameDifficulty;
		if(difficulty == 1){
			healthIncreaseCounter = 1;
			numSpawn-=1;
		} else if(difficulty == 2){
			healthIncreaseCounter = 3;	
		} else if(difficulty == 3){
			healthIncreaseCounter = 5;
			numSpawn+=1;
		}
	}

	public void FindAllSpawners(float searchRadius){
		IsSpawn[] spawns = FindObjectsOfType(typeof(IsSpawn)) as IsSpawn[];
		spawnLocations = new Vector3[spawns.Length];
		int i = 0;
		foreach (IsSpawn spawn in spawns) {
			numSpawn++;
			spawnLocations[i] = spawn.transform.position;
			i++;			
		}		
	}
	
	
	void Update(){
		if(!bossSummon && TechManager.hasWolves){
			bossSummon = true;
			spawnBoss (bossVec);
		}

		if(startScript != null){
			startScript.Update ();
		}
		if(startScript.Finished() == true){
			waveStatus = true;
			//Debug.Log ("waves started");
		}			

		if(waveScript != null && waveStatus){
			waveScript.Update ();
		}

		if (waveScript.Finished() == true) {
			spawnWave();
			Debug.Log ("spawn wave");
			waveScript.Reset();	
		}

		if(levelScript != null && waveStatus){
			levelScript.Update ();
		}

		if (levelScript.Finished() == true) {
			numSpawn++;
			Debug.Log ("wave leveled up");
			//setWaveTime(waveTime -= 1.0f);
			waveScript.MaxTime = waveTime -= 10.0f;
			if(waveTime < 1.0f){
				waveScript.MaxTime = 5.0f;
			}
			levelScript.Reset();	
		}
	}

	public void spawnWave(){
		MetricManager.AddWaves(1);
		float rand = Random.value;
		Vector3 tempVec;
		int randIndex = (int)Mathf.Ceil(rand * spawnLocations.Length) - 1;
		if(randIndex == -1) randIndex = 0;
		tempVec = spawnLocations[randIndex];

		for(int i = 0; i < numSpawn; i++){
			rand = Random.value;
				if(rand > 0.0 && rand < 0.3){
					makeMelee(tempVec);
					//SpawnManager.makeMelee(tempVec);
				} else if(rand >= 0.3 && rand < 0.5){
					makeRanged(tempVec);
					//SpawnManager.makeRanged(tempVec);
				} else if(rand >= 0.5 && rand < 0.7){
					makeFastMelee(tempVec);
				} else if(rand >= 0.7 && rand < 0.8){
					makeLuminosaur(tempVec);
				} else if(rand >= 0.8 && rand < 0.9){
					makeLuminotoad(tempVec);
				} else {
					makeMultiRanged(tempVec);
				}
				MetricManager.AddEnemies(1);
			}
		healthIncrease += healthIncreaseCounter;
	}

	public void makeMelee(Vector3 _vec){
		clone = Instantiate (meleeEnemy, _vec, Quaternion.identity) as Rigidbody;
		clone.GetComponent<Killable>().increaseHealth(healthIncrease);
	}
	public void makeFastMelee(Vector3 _vec){
		clone = Instantiate (meleeFastEnemy, _vec, Quaternion.identity) as Rigidbody;
		clone.GetComponent<Killable>().increaseHealth(healthIncrease);
	}
	public void makeLuminosaur(Vector3 _vec){
		clone = Instantiate (luminosaurEnemy, _vec, Quaternion.identity) as Rigidbody;
		clone.GetComponent<Killable>().increaseHealth(healthIncrease);
	}
	public void makeLuminotoad(Vector3 _vec){
		clone = Instantiate (luminotoadEnemy, _vec, Quaternion.identity) as Rigidbody;
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