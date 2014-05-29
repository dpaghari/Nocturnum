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
		float rand = Random.value;
		if(rand > 0.0f && rand < 0.05f){
			if(killScript.currHP <= lowHP && killScript.currHP >= lowHP / 2){
				Howl ();
			}
			if(killScript.currHP <= lowHP / 2){
				Howl ();
			}
		}

	}

	public void Howl(){
		Vector3 pos = transform.position;


		float rand = Random.value;
		spawnerObj = GameObject.Find("spawner_prefab");
		for(int i = 0;i < numWolves;i++) {

			if(rand > 0.0f && rand < 0.5f){
				spawnerObj.GetComponent<CanSpawnUpdate>().makeMelee(pos);
			}
			if(rand > 0.5f && rand < 1.0f){
				spawnerObj.GetComponent<CanSpawnUpdate>().makeFastMelee(pos);
			}

		//	Debug.Log("Spawning Wolf");
		}
	
	
	}

}
