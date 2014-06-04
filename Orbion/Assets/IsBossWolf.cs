// PURPOSE:  Script that is attached to the Alpha Wolf.  Checks if the Alpha Wolf is below 40% HP and performs a HOWL action which simply instantiates
// a random wolf near the alpha wolf for help.  Maximum of two howls.  

using UnityEngine;
using System.Collections;

public class IsBossWolf : MonoBehaviour {

	public Killable killScript;
	public GameObject spawnerObj;
	public int numWolves;
	private float lowHP;
	private bool isLow;
	public DumbTimer timerScript;
	public DumbTimer timer2Script;
	public DumbTimer howldelayScript;
	private int howlNum;
	private int howlMax;
	public AudioClip howlSound;
	// Use this for initialization
	void Start () {
		howlNum = 0;
		howlMax = 2;
		timerScript = DumbTimer.New(7.0f, 1.0f);					// Howl Cooldown
		timer2Script = DumbTimer.New(1.5f, 1.0f);
		howldelayScript = DumbTimer.New(1.5f, 1.0f);					// Howl Spawn Wolves Delay
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
	// Howl Function:  Plays Audioclip of the howl sound.  Also creates a set number of wolves(numWolves) which are determined by a random number
	public void Howl(){
		audio.PlayOneShot(howlSound);
		Vector3 pos = transform.position;
		animation.Play("WolfHowl");


		howldelayScript.Update();
		timer2Script.Update();
		spawnerObj = GameObject.Find("spawner_prefab");
		if(howldelayScript.Finished()){
			this.GetComponent<CanMove>().MoveScale -= 1;


			for(int i = 0;i < numWolves;i++) {
					float rand = Random.value;


					if(rand < 0.5f){
						spawnerObj.GetComponent<CanSpawnUpdate>().makeMelee(pos);
					}
					else{
						spawnerObj.GetComponent<CanSpawnUpdate>().makeFastMelee(pos);
					}
					

				
			}
			howlNum++;
			howldelayScript.Reset();
			timerScript.Reset();
			if(timer2Script.Finished()){ this.GetComponent<CanMove>().MoveScale += 1;}
			timer2Script.Reset();
		}

	
	
	}

}
