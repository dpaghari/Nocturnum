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
	public Rigidbody bossBatEnemy;
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

	private float[] enemySpawnPercents;

	private int level = 0;

	void Start(){
		//20, 25, 75
		startTime = 20.0f;
		waveTime = 25.0f;
		levelTime = 75.0f;
		startScript = DumbTimer.New (startTime, 1.0f);
		waveScript = DumbTimer.New (waveTime, 1.0f);
		levelScript = DumbTimer.New (levelTime, 1.0f);
		FindAllSpawners(Mathf.Infinity);
		enemySpawnPercents = new float[8];
		setEnemyPercents();	
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
			healthIncreaseCounter = 0;
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
			spawnLocations[i] = spawn.transform.position;
			i++;			
		}		
	}

	public void setEnemyPercents(){		
		enemySpawnPercents[0] = 0.8f;
		enemySpawnPercents[1] = 0.2f;	
		enemySpawnPercents[2] = 0.0f;	
		enemySpawnPercents[3] = 0.0f;	
		enemySpawnPercents[4] = 0.0f;	
		enemySpawnPercents[5] = 0.0f;
		enemySpawnPercents[6] = 0.0f;
		enemySpawnPercents[7] = 0.0f;
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
		}			

		if(waveScript != null && waveStatus){
			waveScript.Update ();
		}

		if(levelScript != null && waveStatus){
			levelScript.Update ();
		}

		if (levelScript.Finished() == true) {
			numSpawn++;
			level++;
			if(level == 1){
				enemySpawnPercents[0] -= 0.2f;
				enemySpawnPercents[1] += 0.1f;
				enemySpawnPercents[2] += 0.1f;
			} else if(level == 2){
				enemySpawnPercents[0] -= 0.1f;
				//enemySpawnPercents[1] += 0.1f;
				//enemySpawnPercents[2] += 0.1f;
				enemySpawnPercents[3] += 0.1f;					
			} else if(level == 3){
				enemySpawnPercents[0] -= 0.1f;
				enemySpawnPercents[1] -= 0.1f;
				enemySpawnPercents[2] += 0.1f;
				enemySpawnPercents[4] += 0.1f;						
			} else if(level == 4){
				enemySpawnPercents[0] -= 0.1f;
				//enemySpawnPercents[1] -= 0.1f;
				//enemySpawnPercents[3] += 0.1f;
				enemySpawnPercents[5] += 0.1f;		
			} else if(level == 8){
				enemySpawnPercents[0] -= 0.1f;
				enemySpawnPercents[2] -= 0.1f;
				enemySpawnPercents[4] += 0.1f;
				enemySpawnPercents[6] += 0.1f;		
			} else if(level == 15){
				enemySpawnPercents[0] -= 0.1f;
				enemySpawnPercents[1] -= 0.1f;
				enemySpawnPercents[6] += 0.1f;
				enemySpawnPercents[7] += 0.1f;		
			}
			//Debug.Log ("wave leveled up");
			//setWaveTime(waveTime -= 1.0f);
			
			waveScript.MaxTime = waveTime -= 1.0f;
			if(waveTime < 20.0f){
				waveScript.MaxTime = 20.0f;
			}
			levelScript.MaxTime = levelTime -= 3.0f;
			if(levelTime < 60.0f){
				levelScript.MaxTime = 60.0f;
			}
			
			levelScript.Reset();
			
		}
		if (waveScript.Finished() == true) {
			spawnWave();
			waveScript.Reset();	
		}
	}

	public void spawnWave(){
		//Debug.Log("wave");
		MetricManager.AddWaves(1);
		float rand = Random.value;
		Vector3 tempVec;
		int randIndex = (int)Mathf.Ceil(rand * spawnLocations.Length) - 1;
		if(randIndex == -1) randIndex = 0;
		tempVec = spawnLocations[randIndex];

		int j;
		for(int i = 0; i < numSpawn; i++){
			rand = Random.value;
			j = 0;			
			while(j < enemySpawnPercents.Length){
				if(enemySpawnPercents[j] == 0.0f){
					j++;
				} else if(rand < enemySpawnPercents[j]){
					spawn (tempVec, j);
					j += 100;
				} else {
					rand -= enemySpawnPercents[j];
					j++;
				}
			}
			MetricManager.AddEnemies(1);
		}
		healthIncrease += healthIncreaseCounter;
	}

	public void spawn(Vector3 _vec, int i){
		if(i == 0){
			makeMelee (_vec);
		} else if(i == 1){
			makeFastMelee (_vec);
		} else if(i == 2){
			makeRanged (_vec);
		} else if(i == 3){
			makeMultiRanged (_vec);
		} else if(i == 4){
			makeLuminotoad (_vec);
		} else if(i == 5){
			makeLuminosaur (_vec);
		} else if(i == 6){
			spawnBoss (_vec);
		} else if(i == 7){
			makeBatBoss (_vec);
		}
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
		clone.GetComponent<Killable>().increaseHealth(healthIncrease * 2);
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
		clone.GetComponent<Killable>().increaseHealth((int)(healthIncrease * 1.5f));
	}
	public void spawnBoss(Vector3 _vec){
		clone = Instantiate (bossEnemy, _vec, Quaternion.identity) as Rigidbody;
		clone.GetComponent<Killable>().increaseHealth(healthIncrease * 3);
	}
	public void makeBatBoss(Vector3 _vec){
		clone = Instantiate (bossBatEnemy, _vec, Quaternion.identity) as Rigidbody;
		clone.GetComponent<Killable>().increaseHealth(healthIncrease * 4);
	}

}