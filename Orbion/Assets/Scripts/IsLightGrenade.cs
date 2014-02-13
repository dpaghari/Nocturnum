using UnityEngine;
using System.Collections;

public class IsLightGrenade : MonoBehaviour {

	//public AIController enemyStat;
	public GameObject flare;


	void Start(){

	}



	void OnCollisionEnter(Collision other)
	{
		
		if(other.gameObject.tag == "ground"){
		//	Debug.Log ("hit ground");
			Vector3 temp = transform.position;
			temp.y += 1;
			Instantiate(flare, temp, Quaternion.identity);


		}
		Destroy(gameObject);
		
	}

	void FixedUpdate(){
		//rigidbody.AddForce(Vector3.up * 100, ForceMode.Impulse);
	}

	void Update(){


	}



}
