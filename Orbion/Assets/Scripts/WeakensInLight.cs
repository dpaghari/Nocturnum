//Purpose: Objects with this script have their combat abilities weakened when in the light

using UnityEngine;
using System.Collections;

public class WeakensInLight : MonoBehaviour {

	//How long it stays weakened after leaving the light
	public float WeakenDuration;
	
	//Whether the object is currently weakend
	public bool IsWeakened {get; protected set;}

	//Ratio that the movement is slowed down by
	public float SlowedRatio;

	protected CanMove moveScript;
	protected CanShoot shootScript;
	protected DumbTimer WeakenTimer;



	public void Weaken(){
		WeakenTimer.Reset();
		
		//Do not slow down again if we are already weakened
		if(! IsWeakened){
			moveScript.MoveScale -= SlowedRatio;
			shootScript.SetFiringRate( shootScript.firingTimer.MaxTime + 0.2f);
			IsWeakened = true;
		}
	}


	public void UndoWeaken(){
		moveScript.MoveScale += SlowedRatio;
		shootScript.SetFiringRate( shootScript.firingTimer.MaxTime - 0.2f);
		IsWeakened = false;
	}


	void Start () {
		moveScript = GetComponent<CanMove>();
		shootScript = GetComponent<CanShoot>();
		IsWeakened = false;
		WeakenTimer = DumbTimer.New( WeakenDuration);
	}
	

	void Update () {
		if (WeakenTimer.Finished()){
			if( IsWeakened )
				UndoWeaken();
		}

		WeakenTimer.Update();
	}
}
