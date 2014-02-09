using UnityEngine;
using System.Collections;

public class isBulletAbsorber : MonoBehaviour {

	public GameObject player;



	// Use this for initialization
	void Start () {
		player = GameObject.Find("player_prefab");

	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "enemy_bullet")
		{
			player.GetComponent<AbsorbBullet>().bulletCount += 1;

		}
		
	}
}
