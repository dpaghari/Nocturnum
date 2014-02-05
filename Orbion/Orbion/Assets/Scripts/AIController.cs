using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
	/*
	Placeholder AI behavior before implementing behavior trees
	2 states chase and flee
	*/
	public float speed;
	private float distance = 0.0F;
	public GameObject target;
	public string status;
	public bool isWeakened;


	public EnemyBehavior behaviorScript;

	void Start(){
		isWeakened = false;

		status = "chase";
		this.renderer.material.color = Color.red;
	}

	void Update() {



		if(isWeakened)
			speed = 3.0F;
		else
			speed = 10.0F;

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