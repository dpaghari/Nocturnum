using UnityEngine;
using System.Collections;

public class ShowEnergy : MonoBehaviour {
	public dfLabel _label;
	private string energyString; 
	// Use this for initialization
	void Start () {
		energyString = string.Format("{0}", ResManager.Energy);
	}
	
	// Update is called once per frame
	void Update () {
		energyString = string.Format("{0}", ResManager.Energy);
		//string energyString = string.Format(ResManager.Energy);
		_label.Text = "Energy: " + energyString;
		//Debug.Log(ResManager.Energy);
	}
}
