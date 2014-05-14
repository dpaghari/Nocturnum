using UnityEngine;
using System.Collections;

public class GetsHealed : MonoBehaviour {



	public DumbTimer timerScript;
	// Use this for initialization
	void Start () {
		timerScript = DumbTimer.New(0.5f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		timerScript.Update();

	}

	void OnTriggerStay(Collider other){
		if(timerScript.Finished() == true){
			if(other.tag == "MedBay"){
				this.gameObject.GetComponent<Killable>().Heal(1);
				timerScript.Reset();
			}	
		}





	}
}
