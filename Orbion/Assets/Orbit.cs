using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	private int degrees = 10;
	public Transform target;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (target.position, Vector3.up, degrees * Time.deltaTime * 10);
	}

	void LateUpdate(){
		//transform.rotation = Quaternion.identity;
	}
}
