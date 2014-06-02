using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class AvatarController : MonoBehaviour {

	public CanMove moveScript;
	public CanShootReload shootScript;
	public EquipmentUser equipScript;
	public hasOverdrive overdriveScript;
	public CanUse useScript;
	public CanBuild buildScript;
	public CanResearch researchScript;

	public AudioClip missioncompleteSound;
	public AudioClip shotSound;
	public AudioClip bgm;
	public AudioClip dashSound;

	//public AudioClip collectSound;
	private Rigidbody clone;

	public Rigidbody normalBullet;
	public Rigidbody orbBullet;

	private int startingClipSize;
	//public AudioClip fistSound;

	public bool isPaused;
	public bool isDashing;
	public float dashForce;
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
		dashForce = 60.0F;
		dashCDScript = DumbTimer.New(3.0f);
		isDashing = false;
		isPaused = false;
		moveScript = GetComponent<CanMove>();
		shootScript = GetComponent<CanShootReload>();
		equipScript = GetComponent<EquipmentUser>();
		useScript = GetComponent<CanUse>();
		overdriveScript = GetComponent<hasOverdrive>();
		buildScript = GetComponent<CanBuild>();
		researchScript = GetComponent<CanResearch>();
		collider.enabled = true;
		startingClipSize = shootScript.clipSize;
	}


	void OnEnable(){
		EventManager.ResearchedUpgrade += UpdateUpgrades;
	}

	void OnDisable(){
		EventManager.ResearchedUpgrade -= UpdateUpgrades;
	}




	void FixedUpdate() {


		if(GameManager.KeysEnabled){


			if( Input.GetKeyDown( KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && !isPaused && !isDashing){
				if(dashCDScript.Finished()){
					audio.PlayOneShot(dashSound);
				isDashing = true;
				animation.Play("Dash", PlayMode.StopAll);
				moveScript.Force += dashForce * 2;
				moveScript.Move( Vector3.forward, ForceMode.Impulse);
				dashCDScript.Reset();

				}
			}
			else if( Input.GetKeyDown( KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && !isPaused && !isDashing){
				if(dashCDScript.Finished()){
					audio.PlayOneShot(dashSound);
					isDashing = true;
				animation.Play("Dash", PlayMode.StopAll);
				moveScript.Force += dashForce * 2;
				moveScript.Move( Vector3.left, ForceMode.Impulse);
				dashCDScript.Reset();

				}
			}
			else if( Input.GetKeyDown( KeyCode.S) && Input.GetKey(KeyCode.LeftShift) && !isPaused && !isDashing){
				if(dashCDScript.Finished()){
					audio.PlayOneShot(dashSound);
					isDashing = true;
				animation.Play("Dash", PlayMode.StopAll);
				moveScript.Force += dashForce * 2;
				moveScript.Move( Vector3.back, ForceMode.Impulse);
				dashCDScript.Reset();

				}
			}
			else if( Input.GetKeyDown( KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && !isPaused && !isDashing){
				if(dashCDScript.Finished()){
					audio.PlayOneShot(dashSound);
				isDashing = true;
				animation.Play("Dash", PlayMode.StopAll);
				moveScript.Force += dashForce * 2;
				moveScript.Move( Vector3.right, ForceMode.Impulse);
				dashCDScript.Reset();

				}
			}


			if( Input.GetKey( KeyCode.W) && !isPaused && !isDashing){

				moveScript.Move( Vector3.forward);

			}
			if( Input.GetKey( KeyCode.S) && !isPaused && !isDashing){

				moveScript.Move( Vector3.back);


			}
			if( Input.GetKey( KeyCode.D) && !isPaused && !isDashing){

				moveScript.Move( Vector3.right);


			}
			if( Input.GetKey( KeyCode.A) && !isPaused && !isDashing){

				moveScript.Move( Vector3.left);


			}
		}


	}
	


	// Update is called once per frame
	void Update () {

		//[Don't delete] debug code for showing our shooting angle
		//Debug.DrawRay(transform.position, Utility.GetMouseWorldPos(transform.position.y) - transform.position);
		dashCDScript.Update();
		isDashing = false;
		if(isDashing){

			GameManager.KeysEnabled = false;
		}

		if(TechManager.missionComplete){
			audio.PlayOneShot(missioncompleteSound, 0.3f);
		}

		if(GameManager.KeysEnabled){
			//Uses the CanShootReload component to shoot at the cursor
			if( Input.GetMouseButton( 0) && !isPaused && !buildScript.inBuildingMode){
				
				if( shootScript.FinishCooldown()){
					if(shootScript.reloading == false){
						//if(!animation.isPlaying)
						animation.CrossFade("Shooting");
						if(overdriveScript.overdriveActive == false){
							audio.clip = shotSound;
							audio.PlayOneShot(shotSound,1.0f);
						}
					}


				}

				shootScript.Shoot( Utility.GetMouseWorldPos( transform.position.y));
			}



			//use our current equipment
			if(Input.GetMouseButtonDown(1) && !isPaused){
				equipScript.UseEquip(Utility.GetMouseWorldPos( transform.position.y));

			}

			if(Input.GetKeyDown(KeyCode.F9)){
				ResManager.Reset();
				TechManager.Reset();
				MetricManager.Reset();
				AutoFade.LoadLevel(Application.loadedLevel, 1.0f, 1.0f, Color.black);


			}

			if(Input.GetKeyDown(KeyCode.F10) && !isPaused)
			{

				Time.timeScale = 0.0f;
				isPaused = true;

			}
			else if(Input.GetKeyDown(KeyCode.F10) && isPaused)
			{

				Time.timeScale = 1.0f;
				isPaused = false;    
			} 

		


			if( Input.GetKeyDown( KeyCode.R) && !isPaused){
				
				shootScript.Reload();
				
			}

			if( Input.GetKeyDown( KeyCode.E) && !isPaused){
				useScript.UseAction( useScript.useRange);
			}

			if( Input.GetKeyDown(KeyCode.F11)){

				Application.Quit();
			}

			//swaps to the next equipingment for testing purposes
			//has a little more logic since not all the equipment are implemented yet

			if(Input.GetKeyDown(KeyCode.T) && !isPaused){
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
}
