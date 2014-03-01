using UnityEngine;
using System.Collections;

public class IsFog : MonoBehaviour {
	public Rigidbody target;
	Vector3 dir;


	// Use this for initialization
	void Start () {
		dir = target.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<CanMove>().Move(dir);
		Debug.DrawRay(transform.position, dir);
	}
}
