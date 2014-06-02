using UnityEngine;
using System.Collections;

public class isPhoton : MonoBehaviour {
	public GameObject photonGrid;
	//public GameObject photonSphere;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		photonGrid.transform.Rotate(new Vector3(1, 1, 1));
		//photonSphere.transform.Rotate(new Vector3(1, 0, 0));

	
	}
}
