//Purpose: Attach to an object to prevent building near/on this object

using UnityEngine;
using System.Collections;

public class IsBuildingObstacle : MonoBehaviour {

	//Radius of the circular region that buildings cannot be built in
	public float contactRadius = 2.0f;
}
