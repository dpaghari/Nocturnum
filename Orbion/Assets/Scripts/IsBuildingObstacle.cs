//Purpose: Attach to an object to prevent building near/on this object
//Note: y position of this object is ignored, so assume y = 0

using UnityEngine;
using System.Collections;

public class IsBuildingObstacle : MonoBehaviour {

	//Radius of the circular region that buildings cannot be built in
	public float contactRadius = 2.0f;

	//Applies x scale of object to contact radius
	//If this is checked, contact radius must be fitted while it's scales is 1
	public bool applyScale= false;

	void Start(){
		contactRadius = contactRadius * transform.localScale.x;
	}
}
