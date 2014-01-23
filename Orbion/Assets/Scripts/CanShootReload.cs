using UnityEngine;
using System.Collections;


public class CanShootReload : MonoBehaviour {

	public int clipSize = 5;
	public int currentAmmo;
	public float reloadTime = 0.0F;
	public float reloadCooldown = 200.0F;
	public bool reloading = false;


	
	// Update is called once per frame
	void Update () {



		if(Input.GetMouseButton(0)){
			Debug.Log(currentAmmo);
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
