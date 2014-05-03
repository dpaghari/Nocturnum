using UnityEngine;
using System.Collections;

//This script makes gameobjects that are killable display their damage through rays of light when attached. 

public class IsDamagedEffect : MonoBehaviour {

	public GameObject damageEffect;
	private GameObject clone;
	
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addDamage() {
		clone = Instantiate(damageEffect, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360)) as GameObject;
		clone.transform.parent = transform;
	}

	public void removeDamage(){
		foreach (Transform child in transform) {
			if(child.gameObject.tag == "FxTemporaire"){
				Destroy(child.gameObject);
				if(gameObject.GetComponent<Killable>().currHP < gameObject.GetComponent<Killable>().baseHP){
					return;
				}
			}
		}
	}
}
