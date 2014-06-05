using UnityEngine;
using System.Collections;

public class AB_ZingbatBoss_Aoe : AiBehavior {

	AC_ZingbatBoss controller;

	public CanShoot spinShoot;
	public float spinSpeed = 1f;
	public float spinDuration = 2f;


	public DumbTimer spinTimer {get; set;}
	private Vector3 rotation = new Vector3(0, 1, 0);
	private GameObject target;

	
	public float enterDelay = 1.5f;
	public float exitDelay = 3.0f;
	public DumbTimer enterDelayTimer {get; set;}
	public DumbTimer exitDelayTimer {get; set;}


	//initialization of the behavior
	override public void OnBehaviorEnter(){
		controller = GetComponent<AC_ZingbatBoss>();
		spinTimer = DumbTimer.New( spinDuration);
		enterDelayTimer = DumbTimer.New( enterDelay);
		exitDelayTimer = DumbTimer.New( exitDelay);
		target = GameManager.Player;

	}
	
	//cleanup/transitions when leaving this behavior
	override public void OnBehaviorExit(){
		return;
	}
	
	//Stuff we run on FixedUpdate when this is the current behavior
	override public void FixedUpdateAB(){

		if( enterDelayTimer.Finished()){
			if( spinTimer.Finished()){
				if( target != null) Utility.LerpLook( this.gameObject, target, 5);
			}
			else
				transform.Rotate(rotation * spinSpeed);
		}

		animation.CrossFade("ZingBatGlide");
	}
	
	//Stuff we run on Update when this behavior is the current running
	override public void UpdateAB(){

		if( enterDelayTimer.Finished()){
			if( !spinTimer.Finished())
				if( spinShoot.FinishCooldown())
					spinShoot.Shoot(transform.position - transform.forward);
			spinTimer.Update();
		}

		if( spinTimer.Finished()){
			if( exitDelayTimer.Finished())
				controller.SwitchBehavior( controller.defaultBehavior);
			exitDelayTimer.Update();
		}

		enterDelayTimer.Update();
	}

}
