// PURPOSE:  Player Character Script.  In charge of Player Character Movement, Shooting, Dashing, Pause, Quit, Restart Level.  
// Also in charge of using Equipment Behaviors(Active Abilities) using Right Mouse Button.


using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class AvatarController : MonoBehaviour {



	public AudioClip missioncompleteSound;
	public AudioClip shotSound;
	public AudioClip bgm;
	public AudioClip dashSound;

	//Our dash speed: MoveSpeed * dashSpeedRatio = dash speed
	public float dashSpeedRatio = 12f;

	//The fraction of our regular movement speed to travel
	//at the ending parts of our dash
	public float dashEaseOutRatio = 0.5f;





	public CanMove moveScript {get; set;}
	public CanShootReload shootScript {get; set;}
	public EquipmentUser equipScript {get; set;}
	public hasOverdrive overdriveScript {get; set;}
	public CanUse useScript {get; set;}
	public CanBuild buildScript;
	public CanResearch researchScript {get; set;}

	private int startingClipSize;
	private Material m_Material;

	public DumbTimer dashCDScript;



	//Used for upgrades that need direct instructions to perform modifications
	//As opposed to upgrades that can just read their level from tech maanger
	//everytime it activates, and behave accordingly
	private void UpdateUpgrades( Tech theUpgrade){
		shootScript.clipSize = startingClipSize + 10 * TechManager.GetUpgradeLv(Tech.clipSize);
		shootScript.numOfBulletShot = 1 + TechManager.GetUpgradeLv( Tech.scatter);
		//shootScript.bullet.gameObject.GetComponent<PB_Linear>().homingLevel = TechManager.GetUpgradeLv(Tech.seeker);
		//shootScript.bullet.gameObject.GetComponent<PB_Linear>().ricochetLevel = TechManager.GetUpgradeLv(Tech.ricochet);
	}




	// Use this for initialization
	void Start () {
		collider.enabled = true;
		dashCDScript = DumbTimer.New(3.0f);
		m_Material = new Material("Shader \"Plane/No zTest\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha ZWrite Off Cull Off Fog { Mode Off } BindChannels { Bind \"Color\",color } } } }");

		moveScript = GetComponent<CanMove>();
		shootScript = GetComponent<CanShootReload>();
		equipScript = GetComponent<EquipmentUser>();
		useScript = GetComponent<CanUse>();
		overdriveScript = GetComponent<hasOverdrive>();
		buildScript = GetComponent<CanBuild>();
		researchScript = GetComponent<CanResearch>();

		startingClipSize = shootScript.clipSize;
	}


	void OnEnable(){
		EventManager.ResearchedUpgrade += UpdateUpgrades;
	}

	void OnDisable(){
		EventManager.ResearchedUpgrade -= UpdateUpgrades;
	}



	IEnumerator DelayedReset(){
		yield return new WaitForSeconds(animation["Dead"].length + 1.5f); 
		GameManager.ResetLevel();
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


	private IEnumerator Fade(float aFadeOutTime, float aFadeInTime, Color aColor){
		float t = 0.0f;
		while (t<0.1f)
		{
			yield return new WaitForEndOfFrame();
			t = Mathf.Clamp01(t + Time.deltaTime / aFadeOutTime);
			DrawQuad(aColor, t);
		}
		
		while (t>0.0f)
		{
			yield return new WaitForEndOfFrame();
			t = Mathf.Clamp01(t - Time.deltaTime / aFadeInTime);
			DrawQuad(aColor, t);
		}

	}

	private void StartFade(float aFadeOutTime, float aFadeInTime, Color aColor){
		StartCoroutine(Fade(aFadeOutTime, aFadeInTime, aColor));
	}
	
	IEnumerator FlashWhenHit (){
		yield return new WaitForSeconds (.01f);
		StartFade (0.8f, 0.0f, Color.red);
	}



	public void OnHit(){

		StartCoroutine(FlashWhenHit());
		if(!animation.IsPlaying("Groundpunch")){
			animation.Play("GetHit");
		}

	}

	public void Die(){
		if(!GameManager.PlayerDead){
			animation.CrossFade("Dead");
			GameManager.PlayerDead = true;	
		}
		collider.enabled = false;

		StartCoroutine("DelayedReset");
		return;
	}


	public void Run( Vector3 direction){
		if( direction.magnitude > 0){
			animation.CrossFade("Run");
			Utility.LerpLook(this.gameObject, transform.position + direction, 10, false);
			moveScript.Move( direction);
		}
		else 
			animation.CrossFade("Idle");
	}


	//Includes time with no movement, then a burst of speed
	//and transitions to EaseOutDash
	IEnumerator EaseInDash(Vector3 direction) {
		for(;;){
			if( animation.IsPlaying("Dash") == false) break;

			if( animation["Dash"].normalizedTime > 0.35 ){	
				//audio.PlayOneShot( dashSound, 0.5f);
				StartCoroutine( "EaseOutDash", direction);
				break;
			}

			if( animation["Dash"].normalizedTime > 0.15)
				moveScript.Move( direction * dashSpeedRatio);

			yield return new WaitForFixedUpdate();
				
		}
	}



	//Slowly moves the player at the ending parts of the dash
	IEnumerator EaseOutDash(Vector3 direction) {
		for(;;){
			if( animation["Dash"].normalizedTime >= 0.95 || animation.IsPlaying("Dash") == false)
				break;

			moveScript.Move( direction * dashEaseOutRatio);
			yield return new WaitForFixedUpdate();
		}
	}



	public void Dash( Vector3 direction){
		if( direction.magnitude > 0){
			animation.CrossFade("Dash");
			Utility.LerpLook(this.gameObject, transform.position + direction, 100, false);
			StartCoroutine( "EaseInDash", direction);
		}
		else 
			animation.CrossFade("Idle");

	}



	public void Shoot( Vector3 position){
		if( shootScript.reloading) return;
		Utility.LerpLook(this.gameObject, position, 100, false);

		if( animation.IsPlaying( "Run"))
			animation.CrossFade( "ShootRun");
		
		else
			animation.CrossFade( "Shooting");

		if( shootScript.FinishCooldown()){
			if( overdriveScript.overdriveActive == false){
				audio.clip = shotSound;
				audio.PlayOneShot(shotSound,1.0f);
				shootScript.Shoot( position);
			}
		}

	}



	public void ActivateEquip( Vector3 position){
		equipScript.UseEquip( position);
	}



	public void Reload(){
		shootScript.Reload();
	}



	void FixedUpdate() {
	}
	


	// Update is called once per frame
	void Update () {

		dashCDScript.Update();


		if( TechManager.missionComplete){
			audio.PlayOneShot(missioncompleteSound, 0.2f);
		}




		//swaps to the next equipment for testing purposes
		//has a little more logic since not all the equipment are implemented yet
		//Switch Equipment
		if( Input.GetKeyDown(KeyCode.T)){
			EquipType nextEquip = equipScript.CurrEquipType;
			int typeIterator = (int) equipScript.CurrEquipType;

			do{
			typeIterator ++;
			if (typeIterator >= (int)EquipType._length)
				typeIterator = 0;
			nextEquip = (EquipType) typeIterator;
			}
			while (equipScript.GetEquip(nextEquip) == null);
			
			Debug.Log(string.Format("Equip switched from {0} to {1}.", equipScript.CurrEquipType, nextEquip));
			equipScript.ChangeEquip(nextEquip);
			
		}




	}
}

