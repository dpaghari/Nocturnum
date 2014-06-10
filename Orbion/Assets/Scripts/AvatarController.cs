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

	private Rigidbody clone;

	public Rigidbody normalBullet;
	public Rigidbody orbBullet;

	private int startingClipSize;


	public CanMove moveScript {get; set;}
	public CanShootReload shootScript {get; set;}
	public EquipmentUser equipScript {get; set;}
	public hasOverdrive overdriveScript {get; set;}
	public CanUse useScript {get; set;}
	public CanBuild buildScript;
	public CanResearch researchScript {get; set;}



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


	public void Run( Vector3 direction){
		if( direction.magnitude > 0){
			animation.CrossFade("Run");
			Utility.LerpLook(this.gameObject, transform.position + direction, 10, false);
			moveScript.Move( direction);
		}
		else 
			animation.CrossFade("Idle");
	}


	//Need to add in check for paused game since this doesn't use game update
	//or multiply by game scale
	IEnumerator EaseInDash(Vector3 direction) {
		for(;;){
			if( animation.IsPlaying("Dash") == false) break;

			if( animation["Dash"].normalizedTime > 0.35 ){	
				StartCoroutine( "EaseOutDash", direction);
				break;
			}

			if( animation["Dash"].normalizedTime > 0.15)
				moveScript.Move( direction * 12);

			yield return new WaitForFixedUpdate();
				
		}
	}
	
	IEnumerator EaseOutDash(Vector3 direction) {
		for(;;){
			if( animation["Dash"].normalizedTime >= 0.95 || animation.IsPlaying("Dash") == false)
				break;

			moveScript.Move( direction * 0.5f);
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

		//[Don't delete] debug code for showing our shooting angle
		//Debug.DrawRay(transform.position, Utility.GetMouseWorldPos(transform.position.y) - transform.position);
		dashCDScript.Update();
		isDashing = false;
		if(isDashing){

			GameManager.KeysEnabled = false;								// disable keys when dashing
		}

		if(TechManager.missionComplete){
			audio.PlayOneShot(missioncompleteSound, 0.2f);					// play Mission Complete sound
		}

		if(GameManager.KeysEnabled){


			//Pause
			if(Input.GetKeyDown(KeyCode.F10) && !isPaused)
			{

				Time.timeScale = 0.0f;
				isPaused = true;

			}
			//Unpause
			else if(Input.GetKeyDown(KeyCode.F10) && isPaused)
			{

				Time.timeScale = 1.0f;
				isPaused = false;    
			} 




			//swaps to the next equipment for testing purposes
			//has a little more logic since not all the equipment are implemented yet
			//Switch Equipment
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

