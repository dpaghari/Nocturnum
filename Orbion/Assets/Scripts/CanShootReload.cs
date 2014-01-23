using UnityEngine;
using System.Collections;


public class CanShootReload : MonoBehaviour {

	public int clipSize = 5;
	public int currentAmmo = 5;
	public float reloadTime = 0.0F;
	public float reloadCooldown = 200.0F;
	public bool reloading = false;


	
	// Update is called once per frame
	void Update () {



		if(Input.GetMouseButton(0)){
			Debug.Log(currentAmmo);
			if(currentAmmo > 0 && reloading == false){
			GetComponent<CanShoot>().Shoot();
			}
		
		}

		if(currentAmmo == 0 || (Input.GetKeyDown("r") && currentAmmo != clipSize)) 
		   reloading = true;

		if(reloading)
			reload ();
	
	}

	void reload () {
		reloadTime++; 
		if(reloadTime > reloadCooldown){
			currentAmmo = clipSize;
		}

		reloading = false;
	}
}
