using UnityEngine;
using System.Collections;

public class EB_LightGrenade : EquipmentBehavior {

	public Rigidbody Grenade;

	private Rigidbody clone;

	protected void GrenadeShot(Vector3 target){
		Vector3 dir = target - transform.position;
		dir.Normalize();
		//dir.y += 5;
		Vector3 temp = transform.position;
		temp.y += 5;

		clone = Instantiate(Grenade, temp + dir * 2, Quaternion.LookRotation(dir, Vector3.up)) as Rigidbody;
	}



	public override void Action (Vector3 cursor) { GrenadeShot(cursor);}
	
	
	public override void FixedUpdateEB() {return;}


	public override void UpdateEB() {return;}
	
	
	public override void OnSwitchEnter() {return;}
	
	
	public override void OnSwitchExit() {return;}
	

}
