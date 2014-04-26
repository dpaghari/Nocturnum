using UnityEngine;
using System.Collections;

public class hasOverdrive : MonoBehaviour {

	public float overdriveCount;
	public CanShootReload shootScript;
	public CanMove moveScript;
	public DumbTimer timerScript;
	public DumbTimer dwindleScript;



	public GameObject overdriveEffect;
	private GameObject clone;
	private GameObject trailClone;

	public bool overdriveActive;
	public float odtime;

	public float overdriveLimit;

	private float fireRate;
	private float moveRate;
	private float odfireRate;
	private float odmoveRate;
	// Use this for initialization
	void Start () {
		overdriveLimit = 30.0f;
		fireRate = shootScript.firingRate;
		moveRate = moveScript.MoveScale;
		odfireRate = fireRate - 0.1f;
		odmoveRate = moveRate + 0.2f;

		odtime = 10.0f;
		overdriveCount = 0.0f;
		overdriveActive = false;
		timerScript = DumbTimer.New (odtime, 1.0f);
		dwindleScript = DumbTimer.New (10.0f, 1.0f);
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

			overdriveActive = true;
			overdriveCount = 0.0f;

			activateOverdrive();
		}

		if(overdriveCount > 0.0f){
			Debug.Log (overdriveCount);
			dwindleScript.Update();
			if(dwindleScript.Finished() == true){
				overdriveCount -= 1.0f;
				dwindleScript.Reset();
			}

		}



	}

	void activateOverdrive(){
		Debug.Log ("Overdrive!");

		if (overdriveActive) {
			clone = Instantiate(overdriveEffect, transform.position, Quaternion.identity) as GameObject;
			//timerScript.Update ();
			shootScript.firingRate = odfireRate;
			moveScript.MoveScale = odmoveRate;
		}

	
	}

	void turnoffOverdrive(){
		Debug.Log ("Turning off overdrive");
		if (overdriveActive) {
			shootScript.firingRate = fireRate;
			moveScript.MoveScale = moveRate;
			overdriveActive = false;

		}
		overdriveCount = 0.0f;
		timerScript.Reset();
		
		
	}





}
