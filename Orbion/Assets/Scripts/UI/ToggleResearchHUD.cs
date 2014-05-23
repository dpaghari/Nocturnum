using UnityEngine;
using System.Collections;

public class ToggleResearchHUD : MonoBehaviour {
	public dfTabContainer _tabContainer;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToggleHUD(){
		if(!_tabContainer.IsVisible){
			_tabContainer.IsVisible = true;
		} else if(_tabContainer.IsVisible){
			_tabContainer.IsVisible = false;
		}

	}
}
