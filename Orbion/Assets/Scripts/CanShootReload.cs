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



		if(Input.GetMouseButton(0)){

			if(currentAmmo > 0 && !reloading){
			GetComponent<CanShoot>().Shoot();
			}
		
		}

		if(currentAmmo <= 0)  
		   reloading = true;

		if(Input.GetKeyDown("r") && currentAmmo < clipSize && currentAmmo > 0)
		   reloading = true;



		if(reloading == true){

			reload ();

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
