using UnityEngine;
using System.Collections;

public class IsBossWolf : MonoBehaviour {

	public Killable killScript;
	public GameObject spawnerObj;
	public int numWolves;
	private float lowHP;
	private bool isLow;
	public DumbTimer timerScript;
	private int howlNum;
	private int howlMax;
	public AudioClip howlSound;
	// Use this for initialization
	void Start () {
		howlNum = 0;
		howlMax = 2;
		timerScript = DumbTimer.New(7.0f, 1.0f);					// Howl Cooldown
		isLow = false;
		numWolves = 3;
		killScript = GetComponent<Killable>();
		lowHP = killScript.baseHP * (0.40f);
	}
	
	// Update is called once per frame
	void Update () {
		timerScript.Update();
		if(killScript.currHP <= lowHP && howlNum < howlMax){
			if(timerScript.Finished()){
				Howl ();
			}
		}
			


	}

	public void Howl(){
		audio.PlayOneShot(howlSound);
		Vector3 pos = transform.position;


		spawnerObj = GameObject.Find("spawner_prefab");
		for(int i = 0;i < numWolves;i++) {
			float rand = Random.value;

			if(rand < 0.5f){
				spawnerObj.GetComponent<CanSpawnUpdate>().makeMelee(pos);
			}
			else{
				spawnerObj.GetComponent<CanSpawnUpdate>().makeFastMelee(pos);
			}
			howlNum++;
			timerScript.Reset();

			Debug.Log("Spawning Wolf");
		}
	
	
	}

}
