using UnityEngine;
using System.Collections;

public class ShowReloading : MonoBehaviour {
	public dfLabel _label;
	public GameObject ammoRef;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.AvatarContr.shootScript.reloading){
			_label.Text = "Reloading...";
		} else {
			_label.Text = "Ammo: " + ammoRef.GetComponent<CanShootReload>().currentAmmo + "/" + ammoRef.GetComponent<CanShootReload>().clipSize;
		}
	}
}
