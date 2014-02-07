using UnityEngine;
using System.Collections;

public class LightFade : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(this.light.intensity);
		//if(createdFlare){
			light.intensity -= 0.1f;
		//}
		if(light.intensity == 0.0f){
			
			//Destroy(light);
			//createdFlare = false;
			Destroy(gameObject);
		}


	
	}
}
