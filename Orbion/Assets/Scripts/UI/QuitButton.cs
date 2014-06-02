using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void QuitGame(){
		Time.timeScale = 1.0f;
		Application.Quit();
		/*
		MetricManager.Reset();
		ResManager.Reset();
		TechManager.Reset();
		AutoFade.LoadLevel(Application.loadedLevel, 1.0f, 1.0f, Color.black);
		*/
	}
}
