using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	private int degrees = 10;
	public Transform target;
	private Vector3 direction;
	private Vector3 position;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//transform.RotateAround (target.position, Vector3.up, degrees * Time.deltaTime * 10);
	

	}

	void FixedUpdate(){
		position = target.transform.position;
		position.y += 2;
		position.z -= 2;
		position.x -= 2;

		transform.position = position;

		//transform.position = target.transform.position;
		direction = Utility.GetMouseWorldPos(transform.position.y);
		transform.forward = direction - transform.position;
		//transform.rotation = Quaternion.identity;
	}
}
