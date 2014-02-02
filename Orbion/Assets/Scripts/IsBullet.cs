using UnityEngine;
using System.Collections;

public class IsBullet : MonoBehaviour {
	/*
	Handles all player bullet types
	*/
	// lifetime - How many seconds until the bullet gets destroyed
	public float lifetime = 10.0F;
	public int damage = 5;
	public CanMove moveScript;
	
	void Start () {
	}
	// Destroy bullet after lifetime
	void Update () {
		lifetime -= Time.deltaTime;
		if(lifetime < 0.0F){
			Destroy (this.gameObject);
		}
	}
	// If you hit an enemy it calls damage() on it
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Enemy")	{
			collision.gameObject.GetComponent<Killable>().damage(damage);	
		}
		Destroy (this.gameObject);
	}
}