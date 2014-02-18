using UnityEngine;
using System.Collections;

public class IsEffect : MonoBehaviour {

	private float lifeTime = 5.0F;
	private float lifeCounter = 0.0F;
	
	public void Update(){
		if(lifeCounter > lifeTime){
			Destroy(this.gameObject);
		} else {
			lifeCounter += Time.deltaTime;
		}
	}
}
