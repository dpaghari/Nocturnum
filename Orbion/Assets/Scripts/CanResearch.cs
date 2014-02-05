using UnityEngine;
using System.Collections;

public class CanResearch : MonoBehaviour {

	private bool menuUp = false;

	// Use this for initialization
	void Start () {
	
	}
	

	void OnGUI() {
		if(menuUp){
			GetComponent<CanShoot>().ResetFiringTimer();
			GUI.Box(new Rect (10,10,100,90), "Research Menu");
			
			if(GUI.Button(new Rect(20,40,80,20), "Scattershot")) {
				if(TechManager.IsTechAvaliable(Tech.scatter)){
					TechManager.Research(Tech.scatter);
					menuUp = false;
				}
			}

		}
	
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.V)){
			menuUp = !menuUp;
		}
	
	}
}
