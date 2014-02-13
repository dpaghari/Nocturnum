using UnityEngine;
using System.Collections;


//The AI behaviors run actions from components
public abstract class AiBehavior : MonoBehaviour {

	//initialization of the behavior
	abstract public void OnBehaviorEnter();
	
	//cleanup/transitions when leaving this behavior
	abstract public void OnBehaviorExit();
	
	//Stuff we run on FixedUpdate when this is the current behavior
	abstract public void FixedUpdateAB();
	
	//Stuff we run on Update when this behavior is the current running
	abstract public void UpdateAB();
}
