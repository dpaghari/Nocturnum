﻿using UnityEngine;
using System.Collections;

public class TrackPosition : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		transform.position = target.transform.position;
	}


	void Update () {
		//transform.position = target.position;
	}
}
