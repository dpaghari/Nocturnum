using UnityEngine;
using System.Collections;

public class HPLight : MonoBehaviour {
	public Killable killScript;
	//public GameObject target;
	public float duration = 5.0F;
	public Color color0 = Color.white;
	public Color color1 = Color.yellow;
	// Use this for initialization
	void Start () {
		//target = GameObject.Find("player");
	
	}
	
	// Update is called once per frame
	void Update () {

		if(killScript.currHP > 70){
		//float t = Mathf.PingPong(Time.time, duration) / duration;
		light.color = Color.white;
		}
		if(killScript.currHP <= 70 && killScript.currHP > 30){
			color0 = Color.white;
			color1 = Color.yellow;
			float t = Mathf.PingPong(Time.time, duration) / duration;
			//light.color = Color.Lerp(color0, color1, t);
			light.color = color1;
		}
		if(killScript.currHP <= 30){
			color0 = Color.yellow;
			color1 = Color.red;
			float t = Mathf.PingPong(Time.time, duration) / duration;
			//light.color = Color.Lerp(color0, color1, t);
			light.color = color1;
		}

	}
}

