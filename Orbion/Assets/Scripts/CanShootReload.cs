using UnityEngine;
using System.Collections;

public class CanShootReload : CanShoot {
	// Variables for UI
	public int clipSize = 30;
	public int currentAmmo = 30;									

	public float reloadCooldown = 5.0F;
	public bool reloading = false;

	protected float reloadTime = 0.0F;


	protected override void Start (){
		base.Start ();
		reloadTime = reloadCooldown;
	}

	// Update is called once per frame
	protected override void Update () {
		//the base classes' update isn't called in a child class
		base.Update();
		

		if( FinishReloadCooldown()) {
			reloadTime = 0;
			currentAmmo = clipSize;
			reloading = false;
		}


		if( reloading)
			reloadTime += Time.deltaTime;
		/*
		if(currentAmmo <= 0)  														// if object is out of ammo
		   reloading = true;

		if(Input.GetKeyDown("r") && currentAmmo < clipSize && currentAmmo > 0)		// only called if attached to player
		   reloading = true;

		
		if(reloading == true){

			reload ();

		}
		*/
	}


	/*	callShoot should be called by something like AI Controller where all it does is pass in the position of the player as the target position
	 *  which should be calculated in that script.
	 * 
	 *  callShoot is also called by the AvatarController script when the player presses the left mouse button passing in the mouseposition
	 *   as the target position.

	*/
	/*
	public void callShoot(Vector3 targ){											// call Shoot() from CanShoot component with target vec3
		if(currentAmmo > 0 && !reloading){
			GetComponent<CanShoot>().Shoot(targ);
		}
	}
	*/


	//if you try to fire with no ammo and you're not realoding, it will reload for you
	public override void ShootDir (Vector3 dir)
	{
		if(FinishCooldown() && !reloading){
			if(currentAmmo > 0 ){
				base.ShootDir(dir);
				currentAmmo--;
			}

			else {
				Reload();
			}
		}

	}

	public void Reload () {
		if (reloading || currentAmmo == clipSize) return;

		reloading = true;
		reloadTime = 0;

		/*
		reloadTime++; 
		if(reloadTime >= reloadCooldown){
			currentAmmo = clipSize;
			reloading = false;
			reloadTime = 0.0F;
		}
		*/

	}

	protected bool FinishReloadCooldown(){
		return reloadTime >= reloadCooldown;
	}

}
