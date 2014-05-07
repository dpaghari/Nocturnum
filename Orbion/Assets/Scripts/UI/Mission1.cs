using UnityEngine;
using System.Collections;

public class Mission1 : MonoBehaviour {
	public dfCheckbox _checkbox_1;
	public dfCheckbox _checkbox_2;
	public dfCheckbox _checkbox_3;
	public dfCheckbox _checkbox_4;
	public dfCheckbox _checkbox_5;

	// Use this for initialization
	void Start () {
		_checkbox_1.IsChecked = false;
		_checkbox_2.IsChecked = false;
		_checkbox_3.IsChecked = false;
		_checkbox_4.IsChecked = false;
		_checkbox_5.IsChecked = false;
	}
	
	// Update is called once per frame
	void Update () {
		string collectString = string.Format("{0}", ResManager.Collectible);
		_checkbox_4.Label.Text = "Collect 30 Enemy Specimens: " + collectString;
		if(TechManager.hasGenerator){
			_checkbox_1.IsChecked = true;
		}
		if(TechManager.hasScatter){
			_checkbox_2.IsChecked = true;
		}
		if(TechManager.hasTurret){
			_checkbox_3.IsChecked = true;
		}
		if(ResManager.Collectible >= 30){
			_checkbox_4.IsChecked = true;
		}
		if(TechManager.hasWolves){
			_checkbox_5.IsVisible = true;
			_checkbox_5.Label.Text = "Defeat the Alpha Wolf.";
			if(TechManager.hasBeatenWolf)
				_checkbox_5.IsChecked = true;
		}
	}


}
