using UnityEngine;
using System.Collections;

public class ShowEnergy : MonoBehaviour {
	public dfLabel _label;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		string energyString = string.Format("{0}", ResManager.Energy);
		//string energyString = string.Format(ResManager.Energy);
		_label.Text = "Energy: " + energyString;
	}
}
