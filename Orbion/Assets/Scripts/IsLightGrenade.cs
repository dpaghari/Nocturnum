using UnityEngine;
using System.Collections;

public class IsLightGrenade : MonoBehaviour {

	//public AIController enemyStat;
	public GameObject flare;
	void OnCollisionEnter(Collision other)
	{
		
		if(other.gameObject.tag == "ground"){
			Debug.Log ("hit ground");
			Instantiate(flare, transform.position, Quaternion.identity);

		}
		Destroy(gameObject);
		
	}


}
