// PURPOSE:  Equipment Behavior that handles the Light Grenade Equipment.  Lobs a grenade object towards the mouse cursor.
// Grenade Object destroys itself upon hitting the ground and creates a trigger that pulls enemies towards the center of the grenade's area of effect.
// Also deals damage.
using UnityEngine;
using System.Collections;

public class EB_LightGrenade : EquipmentBehavior {

	public Rigidbody Grenade;
	private Rigidbody player;

	private Rigidbody clone;
	private Vector3 playerPos;

	protected void GrenadeShot(Vector3 target){
		Vector3 dir = target - playerPos;
		dir.Normalize();
		//dir.y += 5;
		Vector3 temp = playerPos;
		//temp.Normalize();
		temp.y += 5;

		clone = Instantiate(Grenade, temp + dir, Quaternion.LookRotation(dir, Vector3.up)) as Rigidbody;
	}



	public override void Action (Vector3 cursor) { GrenadeShot(cursor);}
	
	
	public override void FixedUpdateEB() {
		playerPos = transform.position;



		return;
	}


	public override void UpdateEB() {return;}
	
	
	public override void OnSwitchEnter() {return;}
	
	
	public override void OnSwitchExit() {return;}
	

}
