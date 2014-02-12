using UnityEngine;
using System.Collections;

public class IsSearingShot : MonoBehaviour {
	public float lifetime = 5;
	public int DPS = 20;
	public Rigidbody target = null;
	public float damageTimer;


	// Use this for initialization
	void Start () {
		damageTimer = 1/DPS;
	}
	
	// Update is called once per frame
	void Update () {
		lifetime -= Time.deltaTime;
		damageTimer -= Time.deltaTime;

		if(damageTimer <= 0){
			target.gameObject.GetComponent<Killable>().damage(1);
			damageTimer = 1/DPS;
			Debug.Log("poop");
		}

		if(target != null)
			this.gameObject.transform.position = target.gameObject.transform.position;

		if(lifetime <= 0)
			Destroy(this.gameObject);
	}
}
