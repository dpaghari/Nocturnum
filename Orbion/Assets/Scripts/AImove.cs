using UnityEngine;
using System.Collections;

public class AImove : MonoBehaviour {
	/*
	Placeholder AI behavior before implementing behavior trees
	2 states chase and flee
	*/
	public float speed = 6.0F;
	private float distance = 0.0F;
	public GameObject target;
	public string status;

	void Start(){
		status = "chase";
		this.renderer.material.color = Color.red;
	}

	void Update() {
		if(status == "chase"){
			//Debug.Log (GetComponent<Killable>().getHP ());
			if(GetComponent<Killable>().currHP <= 15.0F){
				status = "flee";
			}

			this.target = GameObject.Find ("player_prefab");
			distance = Vector3.Distance(transform.position,target.transform.position);
			if (distance > 20.0) {
				Vector3 targ = target.transform.position;
				Vector3 direction = targ - transform.position;
				direction.Normalize ();
				transform.position += direction * speed * Time.deltaTime;
			} else {
				GetComponent<CanShoot>().Shoot(this.target.transform.position);
			}			
		} else if(status == "flee"){
			this.target = GameObject.Find ("player_prefab");
			distance = Vector3.Distance(transform.position,target.transform.position);
			if (distance > 45.0) {

			} else {
				Vector3 targ = target.transform.position;
				Vector3 direction = targ - transform.position;
				direction.Normalize ();
				transform.position -= direction * speed * Time.deltaTime;
			}
		}
	}
}