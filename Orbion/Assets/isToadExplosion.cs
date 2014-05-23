using UnityEngine;
using System.Collections;

public class isToadExplosion : MonoBehaviour {
	public float pushForce;
	public ForceMode pushForceMode = ForceMode.Impulse;
	public int dmg;

	// Use this for initialization
	void Start () {
		dmg = 30;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		Killable killScript = other.GetComponent<Killable>();

		if(killScript != null){
			killScript.damage(dmg);
			Vector3 dir = (other.transform.position - transform.position).normalized;
			//Debug.Log("Should push things");
			
			other.rigidbody.AddForce (dir * pushForce, pushForceMode);

		}
	}
}
