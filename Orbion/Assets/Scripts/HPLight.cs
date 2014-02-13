using UnityEngine;
using System.Collections;

public class HPLight : MonoBehaviour {
	public Killable killScript;
	public float duration = 0.2F;
	//public Vector3 lightHeight;

	//public Color color0 = Color.white;
	//public Color color1 = Color.yellow;
	// Use this for initialization
	void Start () {

		//light.transform.position = lightHeight;
		
	}
	
	// Update is called once per frame
	void Update () {
		 

		if(killScript.currHP > 70){
			//float t = Mathf.PingPong(Time.time, duration) / duration;
			light.color = Color.white;
		}
		if(killScript.currHP <= 70 && killScript.currHP > 40){
			light.color = Color.yellow;
		}
		if(killScript.currHP <= 40 && killScript.currHP > 20){
			light.color = Color.red;
		}
		if(killScript.currHP <= 20){
			Color color1 = Color.red;
			Color color2 = Color.clear;

			float t = Mathf.PingPong(Time.time, duration) / duration;
			light.color = Color.Lerp(color1, color2, t);

		}
		
	}
}
