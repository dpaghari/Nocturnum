using UnityEngine;
using System.Collections;

public class isQuestExit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			AutoFade.LoadLevel("loadscreen", 2.0f, 2.0f, Color.black);
			other.GetComponent<CanMove>().MoveScale -= 1;

			//GameManager.KeysEnabled = false;
		}



	}
}
