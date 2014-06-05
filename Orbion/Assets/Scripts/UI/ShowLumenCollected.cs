using UnityEngine;
using System.Collections;

public class ShowLumenCollected : MonoBehaviour {
	public dfLabel _label;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		string lumenString = string.Format("{0}", ResManager.Lumen);
		_label.Text = lumenString;
	}
}
