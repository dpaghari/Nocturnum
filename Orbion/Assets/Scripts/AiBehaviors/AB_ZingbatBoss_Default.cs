using UnityEngine;
using System.Collections;

public class AB_ZingbatBoss_Default : AiBehavior {

	AC_ZingbatBoss controller;
	public GameObject target {get; set;}
	public CanShoot defaultShoot;

	public float attackRange = 7f;


	IEnumerator DelayedShot(){
		yield return new WaitForSeconds(0.35f);
		defaultShoot.Shoot( target.transform.position);
	}


	//initialization of the behavior
	override public void OnBehaviorEnter(){
		controller = GetComponent<AC_ZingbatBoss>();
		target = GameManager.Player;
	}
	
	//cleanup/transitions when leaving this behavior
	override public void OnBehaviorExit(){ return;}
	
	//Stuff we run on FixedUpdate when this is the current behavior
	override public void FixedUpdateAB(){
		if( target != null){
			transform.LookAt(target.transform.position);
			transform.forward = -transform.forward;
			if(!animation.IsPlaying("ZingBatAttack")){
				controller.moveScript.Move( target.transform.position - transform.position);
				animation.CrossFade("ZingBatGlide");
			}
		}
	}
	
	//Stuff we run on Update when this behavior is the current running
	override public void UpdateAB(){
		if( target == null) return;

		if( defaultShoot.FinishCooldown()){
			//Debug.Log(Utility.FindDistNoY( transform.position, target.transform.position));
			if( Utility.FindDistNoY( transform.position, target.transform.position) <= attackRange){
				animation.CrossFade("ZingBatAttack");
				StartCoroutine("DelayedShot");
			}
		}


	}

}
