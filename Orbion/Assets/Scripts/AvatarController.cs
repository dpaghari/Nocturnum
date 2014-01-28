using UnityEngine;
using System.Collections;

public class AvatarController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
