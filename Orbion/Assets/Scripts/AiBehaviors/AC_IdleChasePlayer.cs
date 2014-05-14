using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AB_TargetPlayer))]
[RequireComponent(typeof(AB_DoNothing))]

//A example of how to do multi behavior controller
//If player is out of sight, AI does nothing, else it chases the player

public class AC_IdleChasePlayer : AiController {


	public AB_DoNothing Idle {get; protected set;}
	public AB_TargetPlayer Chase {get; protected set;}
	private float updateBehaviorRate = 0.15f;


	//Go idle if we're Aggressive but have no target
	public bool ShouldGoIdle( ){
		if (CurrBehavior is AB_DoNothing) return false;
		if (Chase.CurrTarget == null) return true;
		return false;
	}


	//If we're not already aggresive and we can find a target, be aggressive
	public bool ShouldGoChase( ){
		if (CurrBehavior is AB_TargetPlayer) return false;
		if (Chase.FindTarget(Chase.TargetSearchRadius) != null) return true;
		return false;
	}


	public IEnumerator UpdateBehavior(){
		while(true){
			if( ShouldGoIdle()) SwitchBehavior( Idle);
			else if ( ShouldGoChase()) SwitchBehavior( Chase);
			
			yield return new WaitForSeconds( updateBehaviorRate);
		}
		
	}


	protected override void Start (){
		Idle = GetComponent<AB_DoNothing>();
		Chase = GetComponent<AB_TargetPlayer>();
		CurrBehavior = Idle;
		base.Start();

		StartCoroutine( UpdateBehavior());
	}


	protected override void FixedUpdate () { base.FixedUpdate ();}





	//If player is out of range and no buildings exist switch to idle
	//If player is in range or buildings exist switch to chase
	protected override void Update (){
		base.Update ();
	}
}
