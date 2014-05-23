using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResetGame(){
		Time.timeScale = 1.0f;
		ResManager.Reset();
		TechManager.Reset();
		AutoFade.LoadLevel(Application.loadedLevel, 1.0f, 1.0f, Color.black);
	}
}
