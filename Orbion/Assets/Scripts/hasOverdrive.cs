using UnityEngine;
using System.Collections;

public class hasOverdrive : MonoBehaviour {

	public float overdriveCount;
	public CanShootReload shootScript;
	public CanMove moveScript;
	public DumbTimer timerScript;

	public bool overdriveActive;
	public float odtime;

	private float fireRate;
	private float moveRate;
	private float odfireRate;
	private float odmoveRate;
	// Use this for initialization
	void Start () {
		fireRate = shootScript.firingRate;
		moveRate = moveScript.MoveScale;
		odfireRate = fireRate - 0.5f;
		odmoveRate = moveRate + 1.0f;

		odtime = 10.0f;
		overdriveCount = 0.0f;
		overdriveActive = false;
		timerScript = DumbTimer.New (odtime, 1);
	}
	
	// Update is called once per frame
	void Update () {

	
		checkOverdrive ();

		if (overdriveActive) {
			timerScript.Update ();
		}

		if (timerScript.Finished() == true) {
			turnoffOverdrive();
		}
	
	}

	void checkOverdrive(){

		if (overdriveCount >= 10.0) {
			Debug.Log (overdriveCount);
			overdriveActive = true;
			overdriveCount = 0.0f;

			activateOverdrive();
		}


	}

	void activateOverdrive(){
		Debug.Log ("Overdrive!");
		if (overdriveActive) {
			timerScript.Update ();
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
