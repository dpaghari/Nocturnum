using UnityEngine;
using System.Collections;

public class EB_LightFist : EquipmentBehavior {


	public Rigidbody punchObj;
	private Rigidbody clone;

	public AudioClip fistSound;


	public override void Action ( Vector3 cursor){

		Vector3 temp = transform.position;
		temp.y += 4;
		audio.PlayOneShot(fistSound, 1.0f);
		clone = Instantiate (punchObj, temp, Quaternion.identity) as Rigidbody;
		animation.CrossFade("Groundpunch");	

	}
	public override void FixedUpdateEB (){
	
	}
	
	
	public override void UpdateEB ()
	{
	
		
	}

	public override void OnSwitchEnter ()
	{

	}
	
	public override void OnSwitchExit (){return;}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
