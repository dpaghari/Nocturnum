using UnityEngine;
using System.Collections;

public class AImove : MonoBehaviour {
	public float speed = 6.0F;
	private float distance = 0;
	public GameObject target;

	void Update() {
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
	
	void FixedUpdate() {
		
	}
}