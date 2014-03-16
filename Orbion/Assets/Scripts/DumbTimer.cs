using UnityEngine;
using System.Collections;

//An object that acts as timer, units are in seconds.
//Does not update unless instructed to.
//It's dumbness makes timer interaction with scripts more clear.

//Timer countsdown from Max to 0


public class DumbTimer : ScriptableObject {

	//The time to count down from or count up to
	public float MaxTime {get; protected set;}

	public float CurrTime {get; set;}

	//Multiplier for how fast/slow the timer is counting. 1 is normal speed.
	public float TimeScale {get; set;}

	public float GetStart() { return MaxTime; }

	public float GetGoal() { return 0; }

	//Gets the completion ratio of the timer. 1.0 is 100% done, 0 is 0% done.
	public float GetProgress(){
		return ( MaxTime - CurrTime) / MaxTime;
	}

	//Sets the completion ratio of the timer. 1.0 is 100% done, 0 is 0% done.
	public void SetProgress ( float ratio){
		CurrTime = ( 1 - ratio * MaxTime) ;
	}



	public static DumbTimer New(float maxTime, float timeScale = 1.0f){
		DumbTimer newTimer = ScriptableObject.CreateInstance<DumbTimer>();

		newTimer.MaxTime = maxTime;
		newTimer.TimeScale = timeScale;
		newTimer.SetProgress(0);

		return newTimer;
	}



	public void Update(){
		CurrTime -= (Time.deltaTime * TimeScale);
	}




}
