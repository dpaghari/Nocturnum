using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AB_TargetPlayer))]
[RequireComponent(typeof(AB_DoNothing))]

//A example of how to do multi behavior controller
//If player is out of sight, AI does nothing, else it chases the player

public class AC_IdleChasePlayer : AiController {

	private Killable killScript;

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
		if(killScript != null){
			//Debug.Log("FUCK");
			if (this.gameObject.GetComponent<Killable>().getCurrHP() < this.gameObject.GetComponent<Killable>().getBaseHP()){
				//Debug.Log("FUCK");
				if(Chase != null){
					this.gameObject.GetComponent<AB_TargetPlayer>().setSearchRadius(Mathf.Infinity);
				}
			 	return true;
			}
		}
		return false;
	}


	public IEnumerator UpdateBehavior(){
		while(true){
			if ( ShouldGoChase()) SwitchBehavior( Chase);
			
			yield return new WaitForSeconds( updateBehaviorRate);
		}
		
	}


	protected override void Start (){
		Idle = GetComponent<AB_DoNothing>();
		Chase = GetComponent<AB_TargetPlayer>();
		killScript = this.gameObject.GetComponent<Killable>();
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
