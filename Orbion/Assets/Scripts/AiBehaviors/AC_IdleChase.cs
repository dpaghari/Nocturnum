using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AB_Aggressive))]
[RequireComponent(typeof(AB_DoNothing))]

//A example of how to do multi behavior controller
//If player is out of sight, AI does nothing, else it chases the player

public class AC_IdleChase : AiController {


	public AB_DoNothing Idle {get; protected set;}
	public AB_Aggressive Chase {get; protected set;}

	public float SightRange;

	
	public bool ShouldGoIdle( ){
		if (CurrBehavior is AB_DoNothing) return false;
		float distToTarget = Vector3.Distance( Chase.FindTarget().position, rigidbody.position);
		return distToTarget > SightRange;
	}


	public bool ShouldGoChase( ){
		if (CurrBehavior is AB_Aggressive) return false;
		float distToTarget = Vector3.Distance( Chase.FindTarget().position, rigidbody.position);
		return distToTarget <= SightRange;
	}


	protected override void Start () {
		Idle = GetComponent<AB_DoNothing>();
		Chase = GetComponent<AB_Aggressive>();
		CurrBehavior = Idle;
		base.Start();
	}


	protected override void FixedUpdate () { base.FixedUpdate ();}


	protected override void Update ()
	{
		if( ShouldGoIdle()) SwitchBehavior(Idle);
		else if ( ShouldGoChase()) SwitchBehavior(Chase);

		base.Update ();
	}
}
