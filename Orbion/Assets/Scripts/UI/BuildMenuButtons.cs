using UnityEngine;
using System.Collections;

public class BuildMenuButtons : MonoBehaviour {
	public dfPanel _panel;
	private bool menuUp;

	// Use this for initialization
	void Start () {
		menuUp = false;
		_panel.IsVisible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.B)){
				menuUp = !menuUp;
		}
		if(menuUp){
			_panel.IsVisible = true;
		} else {
			_panel.IsVisible = false;
		}


	}
}
