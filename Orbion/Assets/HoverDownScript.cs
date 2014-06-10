using UnityEngine;
using System.Collections;

public class HoverDownScript : MonoBehaviour {


	public DumbTimer timerScript;
	// Use this for initialization
	void Start () {
		timerScript = DumbTimer.New(0.01f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = transform.position;
		temp.y -= 1.5f / ((Time.time + 1) * 5);
		transform.position = temp;


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
}
