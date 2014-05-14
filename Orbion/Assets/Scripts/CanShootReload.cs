using UnityEngine;
using System.Collections;

public class CanShootReload : CanShoot {
	// Variables for UI
	public int clipSize = 30;
	public int currentAmmo = 30;									

	public float reloadCooldown = 3.0F;
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

	}



	//if you try to fire with no ammo and you're not realoding, it will reload for you
	public override void ShootDir (Vector3 dir)
	{
		if(FinishCooldown() && !reloading){
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


	public override void Shoot( Vector3 target){
		if (currentAmmo <= 0)
			Reload ();
		else {
			int adjustedBulletCount = Mathf.Min(numOfBulletShot, currentAmmo);
			Scattershot( target, adjustedBulletCount);
		}
	}


	public void Reload () {
		if (reloading || currentAmmo == clipSize) return;

		reloading = true;
		reloadTime = 0;

	}

	protected bool FinishReloadCooldown(){
		return reloadTime >= reloadCooldown;
	}

}
