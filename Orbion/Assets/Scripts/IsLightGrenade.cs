using UnityEngine;
using System.Collections;

public class IsLightGrenade : MonoBehaviour {

	//public AIController enemyStat;
	public GameObject flare;
	public bool createdFlare;

	void Start(){
		createdFlare = false;
	}



	void OnCollisionEnter(Collision other)
	{
		
		if(other.gameObject.tag == "ground"){
			Debug.Log ("hit ground");
			Instantiate(flare, transform.position, Quaternion.identity);
			createdFlare = true;

		}
		Destroy(gameObject);
		
	}

	void FixedUpdate(){
		//rigidbody.AddForce(Vector3.up * 100, ForceMode.Impulse);
	}

	void Update(){
		Debug.Log(flare.light.intensity);
		if(createdFlare){
			flare.light.intensity -= 1;
		}
		if(flare.light.intensity == 0){

			Destroy(flare.light);
			createdFlare = false;
		}

	}



}
