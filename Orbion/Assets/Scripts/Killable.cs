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
	public GameObject deathFX;
	private GameObject clone;
	//public GUITexture screenflashTexture;
	private Material m_Material;
	private bool m_Fading;

	//used to check if we should call the event when a building is hurt
	private Buildable buildScript;




	// Set HP to default
	void Start () {
		m_Material = new Material("Shader \"Plane/No zTest\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha ZWrite Off Cull Off Fog { Mode Off } BindChannels { Bind \"Color\",color } } } }");
		m_Fading = false;
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
			StartCoroutine(FlashWhenHit());
			if(!animation.IsPlaying("Groundpunch")){
			animation.Play("GetHit");
			}
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

			
			}

			collider.enabled = false;
			StartCoroutine(WaitAndCallback(animation["Dead"].length));

			return;
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

			if(GetComponent<AC_ZingbatBoss>() != null){
					TechManager.defeatedMotherBat = true;
					clone = Instantiate(deathFX, transform.position, Quaternion.identity) as GameObject;
					
			}


			return;
		}
		Destroy(this.gameObject);

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

	private void DrawQuad(Color aColor,float aAlpha)
	{
		aColor.a = aAlpha;
		m_Material.SetPass(0);
		GL.Color(aColor);
		GL.PushMatrix();
		GL.LoadOrtho();
		GL.Begin(GL.QUADS);
		GL.Vertex3(0, 0, -1);
		GL.Vertex3(0, 1, -1);
		GL.Vertex3(1, 1, -1);
		GL.Vertex3(1, 0, -1);
		GL.End();
		GL.PopMatrix();
	}
	
	private IEnumerator Fade(float aFadeOutTime, float aFadeInTime, Color aColor)
	{
		float t = 0.0f;
		while (t<0.1f)
		{
			yield return new WaitForEndOfFrame();
			t = Mathf.Clamp01(t + Time.deltaTime / aFadeOutTime);
			DrawQuad(aColor,t);
		}

		while (t>0.0f)
		{
			yield return new WaitForEndOfFrame();
			t = Mathf.Clamp01(t - Time.deltaTime / aFadeInTime);
			DrawQuad(aColor,t);
		}
		m_Fading = false;
	}
	private void StartFade(float aFadeOutTime, float aFadeInTime, Color aColor)
	{
		m_Fading = true;
		StartCoroutine(Fade(aFadeOutTime, aFadeInTime, aColor));
	}
	
	IEnumerator FlashWhenHit (){
		//StartFade(0.0f, 0.2f, Color.red/2);
		yield return new WaitForSeconds (.01f);
		StartFade (0.8f, 0.0f, Color.red);
		//Debug.Log("FLASH");
	}


}