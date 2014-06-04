//Purpose: Gives object hit points and allows them to die

using UnityEngine;
using System.Collections;

public class Killable : MonoBehaviour {

	//current hp, gets set to baseHP at start
	public int currHP = 100; 
	//the normal/max hp 
	public int baseHP = 100;

	//Special effect whe bullet heals
	public GameObject healEffect;

	//Variables for making toad explosion
	public GameObject toadExplosion;
	public GameObject deathTarget;
	public GameObject collectTarget;

	//used to check if we should call the event when a building is hurt
	private Buildable buildScript;




	// Set HP to default
	void Start () {
		buildScript = gameObject.GetComponent<Buildable>();
		currHP = baseHP;

	}


	public void increaseHealth(int temp){
		baseHP += temp;
		currHP += temp;
	}


	// Updates HP based on damage taken, calls kill() on dying objects
	public void damage (int dmg) {

		if(gameObject.tag == "Player"){
			animation.Play("GetHit");
		}

		if (buildScript != null) EventManager.OnDamagingBuilding(this);
		currHP -= dmg;

		if (currHP <= 0) kill();

		if(gameObject.GetComponent<IsDamagedEffect>() != null){
			gameObject.GetComponent<IsDamagedEffect>().addDamage();
		}
	}


	// Kills the object
	public void kill () {
		if(gameObject.tag == "Player"){
			GameManager.KeysEnabled = false;
			if(!GameManager.PlayerDead){
			animation.Play("Dead");
			GameManager.PlayerDead = true;
			collider.enabled = false;
			StartCoroutine(WaitAndCallback(animation["Dead"].length));
			}
		}


		IsEnemy enemyScript = GetComponent<IsEnemy>();
		if(enemyScript){
			if(enemyScript.enemyType == EnemyType.luminotoad){
				explode();
			}
			else{
				Destroy (gameObject);
				if (deathTarget != null) {
					Vector3 temp = transform.position;
					temp.y += 4;
					float rand = Random.value;
					if(rand > 0.0 && rand < 0.5){
						if(collectTarget != null){
						Instantiate (collectTarget, temp, this.transform.rotation);
						}
					}
					Instantiate(deathTarget,temp, this.transform.rotation);
				}
			}
			
			MetricManager.AddEnemiesKilled(1);
			MetricManager.AddEnemies(-1);
			
			if(GetComponent<isBossEnemy>() != null)
				TechManager.hasBeatenWolf = true;

			return;
		}
		GameObject.Destroy(this.gameObject);

	}


	public void explode(){
		StartCoroutine(ToadExplode(animation["Luminotoad_Bomb"].length));
		//explode dmg
	}


	IEnumerator ToadExplode(float waitTime){
		yield return new WaitForSeconds(waitTime); 
		if(toadExplosion != null){
			Instantiate(toadExplosion,transform.position, this.transform.rotation);
		}
		Vector3 temp = transform.position;
		temp.y += 4;
		Instantiate(deathTarget,temp, this.transform.rotation);
		Destroy(gameObject);
	}


	//Resets the level if the player dies
	IEnumerator WaitAndCallback(float waitTime){
		yield return new WaitForSeconds(waitTime + 1.5f); 
		ResManager.Reset();
		TechManager.Reset();
		MetricManager.Reset();
		//GameManager.KeysEnabled = true;
		GameManager.PlayerDead = false;

		AutoFade.LoadLevel(Application.loadedLevel, 1.0f, 1.0f, Color.black);
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
			Instantiate(healEffect, transform.position, Quaternion.identity);
	}
}