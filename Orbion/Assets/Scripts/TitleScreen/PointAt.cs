using UnityEngine;
using System.Collections;

public class PointAt : MonoBehaviour {
	public Transform titleCube;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.transform.LookAt (titleCube);


	}


}
