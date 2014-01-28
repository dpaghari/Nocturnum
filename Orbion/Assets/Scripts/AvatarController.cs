using UnityEngine;
using System.Collections;

public class AvatarController : MonoBehaviour {

	public CanMove moveScript;
	public CanShootReload shootScript;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate() {
		if(Input.GetKey(KeyCode.W))
			moveScript.Move(Vector3.forward);
		
		if(Input.GetKey(KeyCode.S))
			moveScript.Move(Vector3.back);
		
		if(Input.GetKey(KeyCode.D))
			moveScript.Move(Vector3.right);
		
		if(Input.GetKey(KeyCode.A))
			moveScript.Move(Vector3.left);

	}
	
	// Update is called once per frame
	void Update () {

		/*	Calls callShoot() from CanShootReload component with Mouse Position's 
		 *  Position
		*/
		if(Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			// Casts the ray and get the first game object hit
			Physics.Raycast(ray, out hit);

			GetComponent<CanShootReload>().callShoot(hit.point);

		}


	
	}
}
