//Purpose: Attach to game objects that can interact with game objects that have the Useable script

using UnityEngine;
using System.Collections;

public class CanUse : MonoBehaviour {

	//The search radius for useable objects
	public float useRange;

	//activates the closest useable object, if any
	public void UseAction (float searchRange){
		GameObject useableObject = Utility.GetClosestWith( transform.position, searchRange, Utility.GoHasComponent<Useable>);
		if (useableObject != null) useableObject.GetComponent<Useable>().Activate(this);
	}

}
