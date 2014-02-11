using UnityEngine;
using System.Collections;

public class IsSearingShot : MonoBehaviour {
	public float lifetime = 5;
	public Rigidbody target;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		lifetime -= Time.deltaTime;

		target.gameObject.GetComponent<Killable>().damage(1);

		this.gameObject.transform.position.Set(target.gameObject.transform.position.x, target.gameObject.transform.position.y, target.gameObject.transform.position.z);

		if(lifetime <= 0)
			Destroy(this.gameObject);
	}
}
