using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AB_Aggressive))]
[RequireComponent(typeof(AB_DoNothing))]

//A example of how to do multi behavior controller
//If player is out of sight, AI does nothing, else it chases the player

public class AC_IdleChase : AiController {


	public AB_DoNothing Idle {get; protected set;}
	public AB_Aggressive Chase {get; protected set;}

	//If player is out of range and no buildings exist return true
	public bool ShouldGoIdle( ){
		if (CurrBehavior is AB_DoNothing) return false;
		return (!Chase.PlayerInRange() && !Chase.BuildingsExist());
	}

	//If player is in range or buildings exist return true
	public bool ShouldGoChase( ){
		if (CurrBehavior is AB_Aggressive) return false;
		return (Chase.PlayerInRange() || Chase.BuildingsExist());
	}

	protected override void Start () {
		Idle = GetComponent<AB_DoNothing>();
		Chase = GetComponent<AB_Aggressive>();
		CurrBehavior = Idle;
		base.Start();
	}

	protected override void FixedUpdate () { base.FixedUpdate ();}

	//If player is out of range and no buildings exist switch to idle
	//If player is in range or buildings exist switch to chase
	protected override void Update ()
	{
		if( ShouldGoIdle()) SwitchBehavior(Idle);
		else if ( ShouldGoChase()) SwitchBehavior(Chase);

		base.Update ();
	}
}
