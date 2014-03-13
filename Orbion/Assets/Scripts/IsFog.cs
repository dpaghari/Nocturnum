using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanMove))]
public class IsFog : MonoBehaviour {
	public Rigidbody target;

	private Vector3 dir;
	private CanMove moveScript;
	


	// Use this for initialization
	void Start() {
		if (target == null)
			target = GameManager.MainGenerator.rigidbody;
		dir = Utility.FindDirNoY(transform.position, target.position);
		moveScript = GetComponent<CanMove>();
	}
	


	// Update is called once per frame
	void Update() {


	}



	void FixedUpdate() {
		dir = Utility.FindDirNoY(transform.position, target.position);
		Debug.DrawRay(transform.position, dir*2);
		moveScript.Move(dir);
	}

}
