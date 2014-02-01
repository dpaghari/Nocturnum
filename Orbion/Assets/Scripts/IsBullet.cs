using UnityEngine;
using System.Collections;

public class IsBullet : MonoBehaviour {
	public int lifetime = 100;
	public int damage = 5;
	public CanMove moveScript;
	
	// Use this for initialization
	void Start () {
	}
	
	void FixedUpdate(){

		//moveScript.Move();
	}
	
	// Update is called once per frame
	void Update () {
		lifetime--;
		if(lifetime < 0)
			Destroy (this.gameObject);
	}
	
	void OnCollisionEnter(Collision collision) {
		
		if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<Killable>().damage(damage);	
		}
		Destroy (this.gameObject);
	}
	
}