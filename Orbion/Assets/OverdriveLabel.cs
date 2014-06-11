using UnityEngine;
using System.Collections;

public class OverdriveLabel : MonoBehaviour {


	public dfLabel _label_overdrive;
	public hasOverdrive overdriveScript;


	// Use this for initialization
	void Start () {
		overdriveScript = GameManager.AvatarContr.GetComponent<hasOverdrive>();
	}
	
	// Update is called once per frame
	void Update () {
		if(overdriveScript.overdriveOn){
			if(!overdriveScript.overdriveActive)
			
				_label_overdrive.IsVisible = true;

			if(Input.GetKeyDown(KeyCode.Space)){
				
				_label_overdrive.IsVisible = false;
			}
		}
	}
}
