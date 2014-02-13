using UnityEngine;
using System.Collections;


//This is a very basic AI controller that just runs one behavior.

//Derive more advanced controllers from this class.

//AiControllers should contain logic to manage behaviors
public class AiController : MonoBehaviour {

	public AiBehavior CurrBehavior;	
	
	protected virtual void Start () {
		if (CurrBehavior == null)
			CurrBehavior = GetComponent<AiBehavior>();
		CurrBehavior.OnBehaviorEnter();
	}
	
	
	protected virtual void FixedUpdate () {
		CurrBehavior.FixedUpdateAB();
	}
	
	
	
	protected virtual void Update () {
		CurrBehavior.UpdateAB();
	}
	
	public virtual void SwitchBehavior( AiBehavior newBehavior){
		CurrBehavior.OnBehaviorExit();
		newBehavior.OnBehaviorEnter();
		CurrBehavior = newBehavior;
	}

}
