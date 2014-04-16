using UnityEngine;
using System.Collections;

public class EB_LightFist : EquipmentBehavior {

	public bool isPunching;
	public Rigidbody punchObj;
	private Rigidbody clone;
	
	public override void Action ( Vector3 cursor){
		//if (!isPunching) {
			clone = Instantiate (punchObj, transform.position, Quaternion.identity) as Rigidbody;
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
