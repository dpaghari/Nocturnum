using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanSpawn : MonoBehaviour {
	/* Handles Enemy Spawning
	 * LevelInfo contains how many units are spawned, how many times it gets repeated
	 * and when it starts
	 * SpawnInfo contains the location vector of the spawn points that can be turned on and off
	*/
	
	//================================//
	// level data
	public class LevelInfo{
		public int numLevel;
		public int numSpawn;
		public int timesToRun;
		public float spawnTime;
		
		public LevelInfo(int _numLevel, int _numSpawn, int _timesToRun, float _spawnTime){
			numLevel = _numLevel;
			numSpawn = _numSpawn;
			timesToRun = _timesToRun;
			spawnTime = _spawnTime;
		}
	}
	//================================//

	//================================//
	// spawn location data
	public class SpawnInfo {
		public int nodeID;
		public string nodeName;
		public bool status;
		public float  nodeX;	
		public float  nodeY;	
		public float  nodeZ;
		
		public SpawnInfo(int _nodeID, string _nodeName, bool _status, float _nodeX,
		                 float _nodeY, float _nodeZ){
			nodeID = _nodeID;
			nodeName = _nodeName;
			status = _status;
			nodeX = _nodeX;
			nodeY = _nodeY;
			nodeZ = _nodeZ;
		}
		
		public void turnOn(){status = true;}
		public void turnOff(){status = false;}
	}
	//================================//
	//holds each location
	public List<SpawnInfo> spawnLocations = new List<SpawnInfo> ();
	//holds each level
	public LevelInfo[] levels = new LevelInfo[totalLevels];
	
	// respawnTime - How many seconds for each individual enemy to spawn
	public float respawnTime = 1.0F;
	private float respawnCounter = 0.0F;
	private float waveCounter = 0.0F;
	
	private bool stopWave = false;
	private bool waveStatus = false;
	private bool unitToggle = true;
	
	private int numLocations = 0;
	private int enemySpawns = 0;
	private int enemyCap;
	
	private Rigidbody clone;
	
	private int spawnCount = 0;
	private int spawnLoop;
	private int totalEnemies = 0;
	private static int totalLevels = 5;
	private int levelCount = 0;
	private int levelItor = 0;
	public Rigidbody meleeEnemy;
	public Rigidbody rangedEnemy;
	
	//turn wave on/off/pause/resume
	public void waveResume(){stopWave = false; Debug.Log ("wave-resummed");}
	public void waveOff(){stopWave = true; Debug.Log ("wave-stopped");}
	public void wavePaused(){waveStatus = false;}//Debug.Log ("wave-paused");}
	public void waveOn(){waveStatus = true;}//Debug.Log ("waveOn" + levelItor);}
	
	
	// Set spawn locations and levels
	void Start(){
		//spawner at the location vector (35,1,35)
		addSpawner (35.0F, 1.0F, 35.0F); addSpawner (-35.0F, 1.0F, -35.0F); addSpawner (-55.0F, 1.0F, 15.0F);
		SpawnInfo initial = spawnLocations.Find(x => x.nodeName.EndsWith("1")); initial.turnOn ();

		//level 1 spawn 2 enemies, repeat once, every 30 seconds
		addLevel (1, 2, 2, 25.0F); addLevel (2, 2, 2, 30.0F); addLevel (3, 2, 2, 30.0F); 
		addLevel (4, 2, 2, 25.0F); addLevel (5, 2, 2, 20.0F); //addLevel (6, 2, 2, 30.0F);
		enemyCap = levels [levelItor].numSpawn; spawnLoop = levels [levelItor].timesToRun;
		
	}
	
	// M turns wave on and off
	void FixedUpdate(){
		Mtog ();
	}
	
	void Update(){
		
		//if wave on begin spawning
		if (waveStatus && !stopWave) {
			//wait for respawnTime
			if (respawnCounter > respawnTime) {
				//for all spawn locations
				foreach (SpawnInfo nInf in spawnLocations) {
					//if spawner is turned on
					if (nInf.status) {
						Vector3 vect = new Vector3 (nInf.nodeX, nInf.nodeY, nInf.nodeZ);
						//unitToggles between melee and ranged

						/*
						if (unitToggle) {
							makeMelee (vect);
						} else {
							makeRanged (vect);
						}
						*/
						makeMelee (vect); //just making one enemy for now

					}
					totalEnemies += 1;
				};
				tog ();
				respawnCounter = 0.0F;
				enemySpawns += 1;
				//turn off wave and start up next one
				if(enemySpawns >= levels[levelItor].numSpawn){
					wavePaused ();
					enemySpawns = 0;
					nextLevel();
				}
				return;
			} else {
				respawnCounter += Time.deltaTime;
			}
			//if the wave is off
		} else {
			//turn on wave
			if(waveCounter > levels[levelItor].spawnTime){
				waveOn ();
				waveCounter = 0.0F;
			} else {
				waveCounter += Time.deltaTime;
			}
			
			
		}
		
		
	}//end of Update
	
	
	private void nextLevel(){
		if(spawnCount >= spawnLoop-1){
			spawnCount = 0;
			if(levelItor < totalLevels-1){

				levelItor++;
				enemyCap = levels [levelItor].numSpawn;
				spawnLoop = levels [levelItor].timesToRun;
				//Debug.Log(levelItor);

				if(levelItor+1 == 2){
					SpawnInfo found = spawnLocations.Find(x => x.nodeName.EndsWith("2"));
					found.turnOn();
				}
				if(levelItor+1 == 3){
					SpawnInfo found = spawnLocations.Find(x => x.nodeName.EndsWith("3"));
					found.turnOn ();
				}

			}
		} else {
			spawnCount++;
		}
	}
	
	private void addSpawner(float _x, float _y, float _z){
		numLocations++;
		SpawnInfo ni = new SpawnInfo (numLocations, "spawner" + numLocations, false, _x, _y, _z);
		spawnLocations.Add (ni);
	}
	
	private void tog(){
		if (unitToggle) {
			unitToggle = false;
		} else {
			unitToggle = true;
		}
	}
	
	private void Mtog(){
		if( Input.GetKey(KeyCode.M)){
			if(!stopWave){
				waveOff();
			} else {
				waveResume();
			}
		}
	}
	
	private void makeMelee(Vector3 vec){
		clone = Instantiate (meleeEnemy, vec, Quaternion.identity) as Rigidbody;
		clone.GetComponent<AB_Aggressive> ().TargetSearchRadius = Mathf.Infinity;
	}
	private void makeRanged(Vector3 vec){
		clone = Instantiate (rangedEnemy, vec, Quaternion.identity) as Rigidbody;
		clone.GetComponent<AB_Aggressive> ().TargetSearchRadius = Mathf.Infinity;
	}
	
	private void addLevel(int nLevel, int nSpawn, int tToRun, float sTime){
		if(levelCount < totalLevels){
			levels[levelCount] = new LevelInfo(nLevel, nSpawn, tToRun, sTime);
			levelCount++;
		}
	}
}