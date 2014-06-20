using UnityEngine;
using System.Collections;

public class HoverDownScript : MonoBehaviour {

	public DumbTimer moveonScript;
	public DumbTimer timerScript;
	// Use this for initialization
	void Start () {
		moveonScript = DumbTimer.New(4.5f);
		timerScript = DumbTimer.New(0.01f);
	}
	
	// Update is called once per frame
	void Update () {
		moveonScript.Update();
		if(moveonScript.Finished()){

			AutoFade.LoadLevel("cutscene2", 3.0f, 3.0f, Color.black);
			//moveonScript.Reset();
		}
		/*
		timerScript.Update();
		if(timerScript.Finished()){
			Vector3 temp = transform.position;
			temp.y -= Time.deltaTime;
			transform.position = temp;
			timerScript.Reset();

		}
		*/


	}
	void LateUpdate(){

		Vector3 temp = transform.position;
		temp.y -= 1.5f / ((Time.time + 1) * 5);
		transform.position = temp;
	}
}
