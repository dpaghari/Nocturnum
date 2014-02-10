using UnityEngine;
using System.Collections;

public class EB_LightGrenade : EquipmentBehavior {

	public Rigidbody Grenade;

	protected void GrenadeShot(Vector3 target){
		Vector3 dir = target - transform.position;
		dir.Normalize();
		Rigidbody clone = Instantiate(Grenade, transform.position + dir * 2, Quaternion.LookRotation(dir, Vector3.up)) as Rigidbody;
	}



	public override void Action (Vector3 cursor) { GrenadeShot(cursor);}
	
	
	public override void Perform() {return;}
	
	
	public override void OnSwitchEnter() {return;}
	
	
	public override void OnSwitchExit() {return;}
	

}
