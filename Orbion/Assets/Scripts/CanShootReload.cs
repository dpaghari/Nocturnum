using UnityEngine;
using System.Collections;



public class CanShootReload : MonoBehaviour {
	// Variables for UI
	public int clipSize = 12;
	public int currentAmmo = 12;									


	public float reloadTime = 0.0F;
	public float reloadCooldown = 200.0F;
	public bool reloading = false;


	
	// Update is called once per frame
	void Update () {
		
		
		if(currentAmmo <= 0)  														// if object is out of ammo
		   reloading = true;

		if(Input.GetKeyDown("r") && currentAmmo < clipSize && currentAmmo > 0)		// only called if attached to player
		   reloading = true;

		
		if(reloading == true){

			reload ();

		}
	
	}


	/*	callShoot should be called by something like AI Controller where all it does is pass in the position of the player as the target position
	 *  which should be calculated in that script.
	 * 
	 *  callShoot is also called by the AvatarController script when the player presses the left mouse button passing in the mouseposition
	 *   as the target position.

	*/
	public void callShoot(Vector3 targ){											// call Shoot() from CanShoot component with target vec3
		if(currentAmmo > 0 && !reloading){
			GetComponent<CanShoot>().Shoot(targ);
		}
	}
	 
	void reload () {
		reloadTime++; 
		if(reloadTime > reloadCooldown){
			currentAmmo = clipSize;
			reloading = false;
			reloadTime = 0.0F;
		}


	}
}
