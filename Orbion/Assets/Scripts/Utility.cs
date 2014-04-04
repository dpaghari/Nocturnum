using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class Utility{


	//Input.mousePosition gives you a screen position, not world position of the map.
	//To get the world position, we create a vertical ray from the screen position,
	//raycast it and if it hits something (like our floor), we take the position of
	//the point we hit. Note that we could easily raycast onto an object above the
	//floor and as such the y value (height) of this function is volatile and generally
	//unreliable/irrelevant. The function allows you to set the y value of the retuned
	// vector3 directly.
	public static Vector3 GetMouseWorldPos(float yvalue = -Mathf.Infinity){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		//floor layer is currently at 10 update this if that changes
		int layerMask = 1 << 10; 
		
		//raycasting onto lightshards were making the angle at which we shoot wonky
		//so only raycast onto things with the floor layer
		Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
		
		//[Don't delete] debug code for showing where our mouse position is parsing into. 
		//Debug.DrawRay (ray.origin, hit.point);
		Vector3 mousePos = hit.point;
		if ( yvalue > -Mathf.Infinity) mousePos.y = yvalue;
		return mousePos;
	}





	//Returns the difference between 2 vectors but ignores the Y value
	//since we only need to use x and z for a top down.
	public static Vector3 FindDirNoY(Vector3 origin, Vector3 target){
		Vector3 dir = target-origin;
		dir.y = 0;
		return dir.normalized;
	}





	//Example conditions for GetClosestWith func below

	//No Conditions; Every game object is a valid return
	public static bool NoCondition( GameObject gobj){ return true;}

	//Only consider GameObjects that have a component T
	public static bool GoHasComponent<T>(GameObject gobj) where T:UnityEngine.Component{
		
		if ( gobj.GetComponent<T>() ) return true;
		return false;
	}
	

	//Returns the clostest GameObject (must have a collider), within radius of the origin, 
	//   and fulfilling the requirements in Condition
	//Returns null if no GameObjects are found.
	//
	//Condition is a function that takes in a GameObject and returns a bool
	//	if it returns true, its parameter is a valid candidate for this function to return 
	//	else we ignore it
	//Example functions are found above
	public static GameObject GetClosestWith(Vector3 origin, float radius, Func<GameObject, bool> Condition = null){
		if (Condition == null) Condition = NoCondition;
		Collider[] hitColliders = Physics.OverlapSphere(origin, radius);

		Collider closest = null;
		float closestDist = Mathf.Infinity;

		for (int i=0; i < hitColliders.Length; i++) {
			if( Condition(hitColliders[i].gameObject) == false) continue;
			
			float distToOrigin = Vector3.Distance(origin, hitColliders[i].transform.position);
			if (distToOrigin < closestDist){
				closest = hitColliders[i];
				closestDist = distToOrigin;
			}
		}

		if (closest == null) return null;
		return closest.gameObject;
	}


}
