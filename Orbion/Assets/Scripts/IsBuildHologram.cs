﻿using UnityEngine;
using System.Collections;

public class IsBuildHologram : MonoBehaviour {

	public Rigidbody RealBuilding;
	public Color defaultColor;
	public Color cannotBuildColor;
	public bool CanBuildHere {get; private set;}

	private MeshRenderer[] childrenMR;

	private float inLightToggleTime = 0.01f;
	private DumbTimer inLightToggler;

	private bool _isInLight;
	private bool IsInLight{
		get { return _isInLight;}

		set { 
			_isInLight = value;
			if( value == true) inLightToggler.Reset();
		}
	}

	//returns true if there is another building too close to this one
	public bool IsBuildingNearby(){
		
		Buildable realBuildingBuildScript = RealBuilding.GetComponent<Buildable>();

		GameObject closestBuilding = Utility.GetClosestWith(transform.position, 10, IsBuilding, Utility.Building_PLM);
		Buildable closestBuildScript = null;
		if( closestBuilding) closestBuildScript = closestBuilding.GetComponent<Buildable>();


		if (closestBuilding == null)
			return false;


		float minimumDistance;
		if(closestBuildScript != null){
			minimumDistance = closestBuildScript.contactRadius + realBuildingBuildScript.contactRadius;
		}else{
			minimumDistance = closestBuilding.GetComponent<IsUnderConstruction>().toBuild.GetComponent<Buildable>().contactRadius + realBuildingBuildScript.contactRadius;
		}
		float actualDistance = Vector2.Distance (new Vector2(closestBuilding.transform.position.x, closestBuilding.transform.position.z), new Vector2(transform.position.x, transform.position.z));
		if (actualDistance < minimumDistance) {
			return true;
		}
		return false;
		

	}
	
	
	//Returns true if given object is a building
	bool IsBuilding(GameObject theBuilding){
		if(theBuilding.GetComponent<Buildable>() == null && theBuilding.GetComponent<IsUnderConstruction>() == null) 
			return false;
		if (theBuilding == gameObject)
			return false;
		
		return true;
	}


	
	public void ChangeColors( Color theNewColor){
		if( childrenMR != null)
			foreach( MeshRenderer mr in childrenMR)
				for( int i =0; i < mr.materials.Length; i++)
					mr.materials[i].color = theNewColor;
	}



	public void UpdateBuildStatus(){
		Color newColor = Color.white;
		if( IsBuildingNearby()){
			CanBuildHere = false;
			newColor = cannotBuildColor;
		}
		else{
			CanBuildHere = true;
			newColor = defaultColor;
		}

		ChangeColors( newColor);

			
	}

	

	// Use this for initialization
	void Start () {
		childrenMR = GetComponentsInChildren<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateBuildStatus();
	}


}