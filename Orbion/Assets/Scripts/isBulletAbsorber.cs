using UnityEngine;
using System.Collections;

public class isBulletAbsorber : MonoBehaviour {

	//public GameObject playerpos;
	public int bulletCount = 0;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (bulletCount);
	
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "enemy_bullet")
		{
			bulletCount += 1;

		}
		
	}
}
