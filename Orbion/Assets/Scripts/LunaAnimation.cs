using UnityEngine;
using System.Collections;

public class LunaAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	 
		if (Input.GetKeyDown(KeyCode.W))
			transform.forward = new Vector3(0f, 0f, 1f);
		else if (Input.GetKeyDown(KeyCode.A))
			transform.forward = new Vector3(-1f, 0f, 0f);
		else if (Input.GetKeyDown(KeyCode.S))
			transform.forward = new Vector3(0f, 0f, -1f);
		else if (Input.GetKeyDown(KeyCode.D))
			transform.forward = new Vector3(1f, 0f, 0f);
		else if (Input.GetKeyDown(KeyCode.W) && (Input.GetKeyDown(KeyCode.A)))
			transform.forward = new Vector3(-1f, 0f, 1f);
		else if (Input.GetKeyDown(KeyCode.W) && (Input.GetKeyDown(KeyCode.D)))
			transform.forward = new Vector3(1f, 0f, 1f);
		else if (Input.GetKeyDown(KeyCode.S) && (Input.GetKeyDown(KeyCode.A)))
			transform.forward = new Vector3(-1f, 0f, -1f);
		else if (Input.GetKeyDown(KeyCode.D))
			transform.forward = new Vector3(1f, 0f, -1f);



	if(Input.GetKeyUp("w")||Input.GetKeyUp("a")||Input.GetKeyUp("d") || Input.GetKeyUp("s")) {
			animation.CrossFade("Idle");
	 }

	
	}
}
