using UnityEngine;
using System.Collections;

public class Killable : MonoBehaviour {
	/*
	Gives prefabs HP, they can take damage, and die
	*/
	//currHP tracks health, baseHP is given unless changed in the prefab
	public int currHP = 100; 
	public int baseHP = 100;
	public GameObject deathTarget;
	public GameObject collectTarget;
	//used to check if we should call the event when a building is hurt
	public Buildable buildScript;
	public GameObject healEffect;
	private GameObject clone;
	public DumbTimer timerScript;


	// Set HP to default
	void Start () {
		timerScript = DumbTimer.New(1.2f, 1.0f);
		buildScript = gameObject.GetComponent<Buildable>();
		currHP = baseHP;

	}

	public void increaseHealth(int temp){
		baseHP += temp;
		currHP += temp;
	}

	void Update () {
		if(GameManager.PlayerDead){
		timerScript.Update();
		}
		if(timerScript.Finished() == true){
			ResManager.Reset();
			TechManager.Reset();
			MetricManager.Reset();
			GameManager.KeysEnabled = true;
			GameManager.PlayerDead = false;
			timerScript.Reset();
			AutoFade.LoadLevel(Application.loadedLevel, 1.0f, 1.0f, Color.black);


			
		}
		//Debug.Log("Obj: " + this.gameObject.name + "CurrHP = " + currHP);
	}


	// Updates HP based on damage taken, calls kill() on dead objects
	public void damage (int dmg) {

		if (buildScript != null) EventManager.OnDamagingBuilding(this);
		currHP -= dmg;
		if (currHP <= 0){

			kill();
		}
		if(gameObject.GetComponent<IsDamagedEffect>() != null){
			gameObject.GetComponent<IsDamagedEffect>().addDamage();
		}
	}

	// Kills enemy or player
	public void kill () {
		if(gameObject.tag == "Player"){
			GameManager.PlayerDead = true;
			GameManager.KeysEnabled = false;
			animation.Play("Dead");
		}
		else{
			Destroy (gameObject);
			MetricManager.AddEnemiesKilled(1);
			MetricManager.AddEnemies(-1);

			if(GetComponent<isBossEnemy>() != null)
				TechManager.hasBeatenWolf = true;
			if (deathTarget != null) {
				Vector3 temp = transform.position;
				temp.y += 4;
				//if(deathTarget.GetComponent<IsCollectible>() != null){
					float rand = Random.value;
					//Debug.Log(rand);
					if(rand > 0.0 && rand < 0.5){
					Instantiate (collectTarget, temp, this.transform.rotation);
					//	Debug.Log("creating collectible");
					}
				Instantiate(deathTarget,temp, this.transform.rotation);
				//}
				//else
				//	Instantiate(deathTarget, temp, this.transform.rotation);

			}
		}
		//make death object
	}

	public void explode(){
		kill();
		//explode dmg
	}

	/// <summary>
	/// Heal the specified health.
	/// </summary>
	/// <param name="health">Health.</param>
	public void Heal(int health){
		currHP += health;

		if(gameObject.GetComponent<IsDamagedEffect>() != null){
			gameObject.GetComponent<IsDamagedEffect>().removeDamage();
		}
		

		if(currHP > baseHP)
			currHP = baseHP;
		else
			clone = Instantiate(healEffect, transform.position, Quaternion.identity) as GameObject;
	}
}