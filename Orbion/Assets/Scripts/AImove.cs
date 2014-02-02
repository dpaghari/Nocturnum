using UnityEngine;
using System.Collections;

public class AImove : MonoBehaviour {
	public float speed = 6.0F;
	private float distance = 0;
	public GameObject target;

	public string status = "player_chase";

	void Update() {

		if(status == "player_chase"){
			//Debug.Log (GetComponent<Killable>().getHP ());
			if(GetComponent<Killable>().getHP() <= 15.0F){
				status = "flee";
			}

			this.target = GameObject.Find ("player_prefab");
			distance = Vector3.Distance(transform.position,target.transform.position);
			if (distance > 20.0) {
				Vector3 targ = target.transform.position;
				Vector3 direction = targ - transform.position;
				direction.Normalize ();
				transform.position += direction * speed * Time.deltaTime;
			} else/* if (distance < 100.0)*/ {
				GetComponent<CanShoot>().Shoot(this.target.transform.position);
			}
			
			
		}
	}
	
	void FixedUpdate() {
		
	}
}