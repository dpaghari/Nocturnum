using UnityEngine;
using System.Collections;

public class WeakensInLight : MonoBehaviour {


	public float WeakenDuration;
	public bool IsWeakened {get; protected set;}
	public float SlowedRatio;

	protected CanMove moveScript;
	protected float WeakenTimer = 0;


	//returns if we're done being weakened
	public bool FinishWeakenDuration(){
		return WeakenTimer <= 0;
	}
	

	//sets the duration to (ratio * 100)% progress
	public void SetWeakenDuration(float ratio){
		WeakenTimer = ratio * WeakenDuration;
	}
	

	//Resets the duration back to 100%
	public void ResetWeakenDuration(){
		SetWeakenDuration(1.0f);
	}
	

	public void Weaken(){
		ResetWeakenDuration();
		
		//Do not slow down again if we are already weakened
		if(! IsWeakened){
			moveScript.MoveScale -= SlowedRatio;
			IsWeakened = true;
		}
	}

	public void UndoWeaken(){
		moveScript.MoveScale += SlowedRatio;
		IsWeakened = false;
	}


	// Use this for initialization
	void Start () {
		moveScript = GetComponent<CanMove>();
		IsWeakened = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (FinishWeakenDuration()){
			if( IsWeakened )
				UndoWeaken();
		}
		else
			WeakenTimer -= Time.deltaTime;

	
	}
}
