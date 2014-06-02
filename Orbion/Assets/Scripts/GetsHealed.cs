// PURPOSE:  Checks if object this script is attached to is within a Medbay's Trigger Area of Effect Radius.  If it is it calls the Heal function
// in the object's <Killable> script incrementing the health of the object by a fixed number over time controlled by a cooldown timer.

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
				this.gameObject.GetComponent<Killable>().Heal(10);
				timerScript.Reset();
			}	
		}





	}
}
