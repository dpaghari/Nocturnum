using UnityEngine;
using System.Collections;

public class IsBullet : MonoBehaviour {
	public int lifetime = 100;
	public int damage = 5;
	
	// Use this for initialization
	void Start () {
	}
	
	void FixedUpdate(){
	}
	
	// Update is called once per frame
	void Update () {
		lifetime--;
		if(lifetime < 0)
			Destroy (this.gameObject);
	}
	
	void OnCollisionEnter(Collision collision) {
		
		if(collision.gameObject.name == "enemy_prefab"/* || collision.gameObject.name == "player_prefab"*/)
		{
			collision.gameObject.GetComponent<Killable>().damage(damage);	
		}
	}
	
}