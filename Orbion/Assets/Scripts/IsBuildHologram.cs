//Purpose: UI object that acts as a cursor for building

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IsBuildHologram : MonoBehaviour {

	//The building that this is a hologram of
	public Rigidbody RealBuilding;

	//The binary colors the hologram should have
	public Color defaultColor;
	public Color cannotBuildColor;

	//Whether we can build at the last position UpdateBuildStatus() was called on
	public bool CanBuildHere {get; private set;}

	//Autofilled array of renderers that we use to change colors
	private MeshRenderer[] childrenMR;

	//Need to know the obstacle radius of the real building we want to build
	private Buildable buildScript;



	//Detects whether something is in light
	//Cannot use the lightsource's trigger because the physics is slowed down
	//when in build mode, making the status update very unreliable
	public bool IsInLight(){
		bool inSomeLight = false;
		if( IsLightSource.LightSources == null) return false;
		foreach( KeyValuePair<int, IsLightSource> entry in IsLightSource.LightSources){
			float lightRange = entry.Value.lightArea.radius;
			float distFromLight = Utility.FindDistNoY( transform.position, entry.Value.transform.position);
			if( distFromLight <= lightRange) return true;
		}
		return inSomeLight;
	}



	//returns true if there is another building, construction, or obstacle
	public bool IsObstacleNearby(){
		
		

		int searchLayers = Utility.Building_PLM | Utility.Environment_PLM | Utility.Quest_PLM | Utility.Plant_PLM;

		GameObject closestBuilding = Utility.GetClosestWith(transform.position, 10, IsObstacle, searchLayers);
		if (closestBuilding == null) return false;

		Buildable closestBuildScript = null;
		IsUnderConstruction closestConstructionScript = null;
		IsBuildingObstacle closestObstableScript = null;
		closestBuildScript = closestBuilding.GetComponent<Buildable>();
		closestConstructionScript = closestBuilding.GetComponent<IsUnderConstruction>();
		closestObstableScript = closestBuilding.GetComponent<IsBuildingObstacle>();

		float obstacleRadius = 0;
		if( closestObstableScript) obstacleRadius = closestObstableScript.contactRadius;
		if( closestConstructionScript) obstacleRadius = closestConstructionScript.toBuild.GetComponent<Buildable>().contactRadius;
		if( closestBuildScript) obstacleRadius = closestBuildScript.contactRadius;


		float minimumDistance = buildScript.contactRadius + obstacleRadius;
		float actualDistance = Utility.FindDistNoY( transform.position, closestBuilding.transform.position);

		if (actualDistance < minimumDistance)
			return true;


		return false;
	}
	
	
	//Returns true if given object is an obstacle of building
	bool IsObstacle(GameObject theBuilding){

		if (theBuilding == gameObject)
			return false;

		if( theBuilding.GetComponent<Buildable>() != null)
			return true;
		if( theBuilding.GetComponent<IsUnderConstruction>() != null) 
			return true;
		if( theBuilding.GetComponent<IsBuildingObstacle>() != null)
			return true;

		return false;
	}


	public bool InValidBuildZone(){
		bool inLight = IsInLight() || !buildScript.requiresLight;
		return !IsObstacleNearby() && inLight;
	}

	
	//Changes the colors of all of this children's materials
	public void ChangeColors( Color theNewColor){
		if( childrenMR != null)
			foreach( MeshRenderer mr in childrenMR)
				for( int i =0; i < mr.materials.Length; i++)
					mr.materials[i].color = theNewColor;
	}


	//Detects whether the hologram is in a buildable location
	//and changes colors accordingly
	public void UpdateBuildStatus(){
		Color newColor = Color.white;
		if( InValidBuildZone()){
			CanBuildHere = true;
			newColor = defaultColor;
		}
		else{
			CanBuildHere = false;
			newColor = cannotBuildColor;
		}

		ChangeColors( newColor);	
	}

	
	void Start () {
		buildScript = RealBuilding.GetComponent<Buildable>();
		childrenMR = GetComponentsInChildren<MeshRenderer>();
	}
	

	void Update () {
		UpdateBuildStatus();
	}


}
