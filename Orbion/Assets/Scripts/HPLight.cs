using UnityEngine;
using System.Collections;

public class HPLight : MonoBehaviour {
	public IsGenerator generatorScript;
	public IsLightCone lightScript;
	public float duration = 0.2F;
	//public GameObject playerLight;
	//public Vector3 lightHeight;

	//public Color color0 = Color.white;
	//public Color color1 = Color.yellow;
	// Use this for initialization
	void Start () {

		//light.transform.position = lightHeight;
		
	}
	
	// Update is called once per frame
	void Update () {
		/*  Checks if the player's Suit energy is within a certain level
		 *  Tells player how much Suit Energy they still have based on the Color
		 *  of the light
		 * 
		*/

		if(lightScript.SuitEnergy > 10){
			//float t = Mathf.PingPong(Time.time, duration) / duration;
			light.color = Color.white;
		}
		if(lightScript.SuitEnergy <= 5 && lightScript.SuitEnergy > 3){
			light.color = Color.yellow;
		}
		if(lightScript.SuitEnergy <= 3 && lightScript.SuitEnergy > 1){
			light.color = Color.red;
		}
		if(lightScript.SuitEnergy <= 1){
			Color color1 = Color.red;
			Color color2 = Color.clear;

			float t = Mathf.PingPong(Time.time, duration) / duration;
			light.color = Color.Lerp(color1, color2, t);

		}
	
		
	}
}
