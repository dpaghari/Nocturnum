using UnityEngine;
using System.Collections;

public class IsBullet : MonoBehaviour {
	public int lifetime = 100;
	public int damage = 5;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifetime--;
		if(lifetime < 0)
			Destroy (this.gameObject);
	}
}
