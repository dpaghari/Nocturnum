using UnityEngine;
using System.Collections;

public class IsBossWolf : MonoBehaviour {

	public Killable killScript;
	public GameObject spawnerObj;
	public int numWolves;
	private float lowHP;
	private bool isLow;
	public DumbTimer timerScript;
	// Use this for initialization
	void Start () {
		timerScript = DumbTimer.New(5.0f, 1.0f);
		isLow = false;
		numWolves = 3;
		killScript = GetComponent<Killable>();
		lowHP = killScript.baseHP * (0.40f);
	}
	
	// Update is called once per frame
	void Update () {

		if(killScript.currHP <= lowHP){

			isLow = true;
			timerScript.Update();
		}
		if(timerScript.Finished() == true){
			if(isLow){

				float rand = Random.value;
				if(rand > 0.0f && rand < 0.05f){
					Howl();
					timerScript.Reset();
				}

			}
		}
	}

	public void Howl(){
		Vector3 pos = transform.position;

		Debug.Log ("HOWLING");
		spawnerObj = GameObject.Find("spawner_prefab");
		for(int i = 0;i < numWolves;i++) {
			spawnerObj.GetComponent<CanSpawnUpdate>().makeMelee(pos);
			Debug.Log("Spawning Wolf");
		}
	
	
	}

}
