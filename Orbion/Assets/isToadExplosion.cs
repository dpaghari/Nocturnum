using UnityEngine;
using System.Collections;

public class isToadExplosion : MonoBehaviour {

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

		}
	}
}
