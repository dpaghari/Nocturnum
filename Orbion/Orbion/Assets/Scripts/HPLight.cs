using UnityEngine;
using System.Collections;

public class HPLight : MonoBehaviour {
	public Killable killScript;
	public float duration = 5.0F;
	//public Color color0 = Color.white;
	//public Color color1 = Color.yellow;
	// Use this for initialization
	void Start () {
	
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(killScript.currHP > 70){
			//float t = Mathf.PingPong(Time.time, duration) / duration;
			light.color = Color.white;
		}
		if(killScript.currHP <= 70 && killScript.currHP > 30){

			//float t = Mathf.PingPong(Time.time, duration) / duration;
			//light.color = Color.Lerp(color0, color1, t);
			light.color = Color.yellow;
		}
		if(killScript.currHP <= 30){

			//float t = Mathf.PingPong(Time.time, duration) / duration;
			//light.color = Color.Lerp(color0, color1, t);
			light.color = Color.red;
		}
		
	}
}
