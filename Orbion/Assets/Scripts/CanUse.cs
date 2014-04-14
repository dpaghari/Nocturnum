using UnityEngine;
using System.Collections;

public class CanUse : MonoBehaviour {

	public float useRange;

	public void UseAction (float searchRange){
		GameObject useableObject = Utility.GetClosestWith( transform.position, searchRange, Utility.GoHasComponent<Useable>);
		if (useableObject != null) useableObject.GetComponent<Useable>().Activate(this);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
