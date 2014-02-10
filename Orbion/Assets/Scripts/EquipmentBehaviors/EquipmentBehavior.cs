using UnityEngine;
using System.Collections;

public abstract class EquipmentBehavior : MonoBehaviour {

	//the cooldown between uses for the behavior
	public float Cooldown = Mathf.Infinity;


	//The triggered action for the behavior;
	//i.e what should happen when the button gets pressed
	abstract public void Action(Vector3 cursor);

	//The update of the behavior
	abstract public void Perform();

	//Called when client swaps in this behavior
	abstract public void OnSwitchEnter();
	
	//Called when client swaps out of this behavior
	abstract public void OnSwitchExit();
	
	

}
