using UnityEngine;
using System.Collections;

public class hasOverdrive : MonoBehaviour {

	public float overdriveCount;
	public CanShootReload shootScript;
	public CanMove moveScript;
	public DumbTimer timerScript;
	public DumbTimer dwindleScript;

	public AudioClip overdriveSound;



	public GameObject overdriveEffect;
	private GameObject clone;
	private GameObject trailClone;

	public bool overdriveOn;
	public bool overdriveActive;
	public float odtime;

	public float overdriveLimit;

	private float fireRate;
	private float moveRate;
	private float odfireRate;
	private float odmoveRate;
	// Use this for initialization
	void Start () {
		overdriveOn = false;
		overdriveLimit = 50.0f;
		fireRate = shootScript.firingRate;
		moveRate = moveScript.MoveScale;
		odfireRate = fireRate - 0.1f;
		odmoveRate = moveRate + 0.2f;

		odtime = 10.0f;
		overdriveCount = 0.0f;
		overdriveActive = false;
		timerScript = DumbTimer.New (odtime, 1.0f);
		dwindleScript = DumbTimer.New (3.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {

	
		checkOverdrive ();

		if (overdriveActive) {
			timerScript.Update ();
			if(clone != null)
			clone.transform.position = transform.position;

			//if(trailClone != null)
			trailClone = Instantiate(overdriveEffect, transform.position, Quaternion.identity) as GameObject;
		}

		if (timerScript.Finished() == true) {
			turnoffOverdrive();
			//Destroy(clone.gameObject);
			//Destroy (trailClone.gameObject);
		}
	
	}

	void checkOverdrive(){

		if (overdriveCount >= overdriveLimit) {
			overdriveOn = true;
			//Debug.Log ("You have OVERDRIVE! PRESS SPACE TO ACTIVATE!");
			overdriveCount = overdriveLimit;
			if(Input.GetKeyDown(KeyCode.Space)){
				audio.PlayOneShot(overdriveSound);

				overdriveActive = true;

				activateOverdrive();
			}
		}

		if(overdriveCount > 0.0f && !overdriveOn){
			//Debug.Log (overdriveCount);
			dwindleScript.Update();
			if(dwindleScript.Finished() == true){
				overdriveCount -= 1.0f;
				dwindleScript.Reset();
			}

		}



	}

	void activateOverdrive(){
		//Debug.Log ("Overdrive!");

		if (overdriveActive) {
			clone = Instantiate(overdriveEffect, transform.position, Quaternion.identity) as GameObject;
			//timerScript.Update ();
			shootScript.SetReloadCooldown(0);
			shootScript.SetFiringRate(odfireRate);
			moveScript.MoveScale = odmoveRate;
		}

	
	}

	void turnoffOverdrive(){
		//Debug.Log ("Turning off overdrive");
		if (overdriveActive) {
			shootScript.SetReloadCooldown(1.8f);
			shootScript.SetFiringRate(fireRate);
			moveScript.MoveScale = moveRate;
			overdriveActive = false;

		}
		overdriveOn = false;
		overdriveCount = 0.0f;
		timerScript.Reset();
		
		
	}





}
