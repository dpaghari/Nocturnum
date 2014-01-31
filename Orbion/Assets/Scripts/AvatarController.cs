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

		if(Input.GetKeyDown(KeyCode.R))
			shootScript.Reload();

	}
	
	// Update is called once per frame
	void Update () {

		/*	Calls callShoot() from CanShootReload component with Mouse Position's 
		 *  Position
		*/
		if(Input.GetMouseButton(0)){
			//Input.mousePosition.y = 1;
			Vector3 adjustedMouse = Input.mousePosition;
			//adjustedMouse.y = transform.position.y;
			//
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			// Casts the ray and get the first game object hit
			Physics.Raycast(ray, out hit);
			Vector3 adjustedHit = hit.point;
			adjustedHit.y= transform.position.y;
			//GetComponent<CanShootReload>().callShoot(hit.point);
			//shootScript.Shoot(hit.point);
			shootScript.Shoot(adjustedHit);


		}

	}
}
