using UnityEngine;
using System.Collections;

public class ToggleResearchHUD : MonoBehaviour {
	public dfTabContainer _tabContainer;
	bool onHUD;

	// Use this for initialization
	void Start () {
		onHUD = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToggleHUD(){
		_tabContainer.IsVisible = !onHUD;

	}
}
