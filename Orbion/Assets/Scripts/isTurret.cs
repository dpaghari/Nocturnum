using UnityEngine;
using System.Collections;

public class isTurret : MonoBehaviour {


	public isPlant plantScript;
	public CanShoot shootScript;
	public Rigidbody target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(target);

		if(plantScript.isActive == true){
			if(target != null)
			shootScript.Shoot(target.position);
		}

	}

	void OnTriggerEnter(Collider other){

		if(other.tag == "Enemy")
			target = other.rigidbody;


	}






}
