//Purpose: Adds ammo and reload functionality to CanShoot

using UnityEngine;
using System.Collections;

public class CanShootReload : CanShoot {
	
	//Max clip size
	public int clipSize = 30;

	//Current ammo in the clip
	public int currentAmmo = 30;									

	public float reloadCooldown = 3.0F;
	public bool reloading = false;

	//protected float reloadTime = 0.0F;
	protected DumbTimer reloadTimer;


	protected override void Start (){
		base.Start ();
		reloadTimer = DumbTimer.New (reloadCooldown);
	}


	protected override void Update () {
		//the base classes' update isn't called in a child class
		base.Update();
		

		if( reloadTimer.Finished()) {
			reloadTimer.Reset();
			currentAmmo = clipSize;
			reloading = false;
		}


		if( reloading)
			reloadTimer.Update();

	}



	//Uses ammo per shot and reloads if empty
	public override void ShootDir (Vector3 dir)
	{
		if(!reloading){
			if(currentAmmo > 0 ){
				base.ShootDir(dir);
				currentAmmo--;
				MetricManager.AddShots (1);
				//Debug.Log(MetricManager.getShotsFired);
			}

			else {
				Reload();
			}
		}

	}


	//If you try to fire with no ammo and you're not reloading, it will reload for you
	public override void Shoot( Vector3 target){
		if (currentAmmo <= 0)
			Reload ();
		else {
			int adjustedBulletCount = Mathf.Min(numOfBulletShot, currentAmmo);
			Scattershot( target, adjustedBulletCount, spreadAngle);
		}
	}


	public void Reload () {
		if (reloading || currentAmmo == clipSize) return;

		reloading = true;
		reloadTimer.Reset();

	}


}
