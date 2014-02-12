using UnityEngine;
using System.Collections;

//This script makes the attached game object pay attention to whether or not it's in light in conjunction with objects that have the "EmitsLight" script. 

public class NoticeLight : MonoBehaviour {

	public bool lit;


	// Use this for initialization
	void Start () {
		lit = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void inLight(){
		lit = true;
	}

	public void outLight(){
		lit = false;
	}

	public bool isLit(){
		return lit;
	}
}
