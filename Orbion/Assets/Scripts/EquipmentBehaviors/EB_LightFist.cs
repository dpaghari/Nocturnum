using UnityEngine;
using System.Collections;

public class EB_LightFist : EquipmentBehavior {

	public bool isPunching;
	public Rigidbody punchObj;
	private Rigidbody clone;

	public AudioClip fistSound;


	public override void Action ( Vector3 cursor){
		//if (!isPunching) {
		Vector3 temp = transform.position;
		temp.y += 4;
		audio.PlayOneShot(fistSound, 1.0f);
			clone = Instantiate (punchObj, temp, Quaternion.identity) as Rigidbody;
			//Debug.Log ("running");

		//}	
	}
	public override void FixedUpdateEB (){
	
	}
	
	
	public override void UpdateEB ()
	{
	
		
	}

	public override void OnSwitchEnter ()
	{
		isPunching = false;
	}
	
	public override void OnSwitchExit (){return;}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
