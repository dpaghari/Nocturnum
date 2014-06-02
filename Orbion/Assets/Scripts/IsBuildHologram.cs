using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IsBuildHologram : MonoBehaviour {

	public Rigidbody RealBuilding;
	public Buildable buildScript;
	public Color defaultColor;
	public Color cannotBuildColor;
	public bool CanBuildHere {get; private set;}

	private MeshRenderer[] childrenMR;

	private float inLightToggleTime = 0.01f;
	private DumbTimer inLightToggler;


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



	//returns true if there is another building too close to this one
	public bool IsBuildingNearby(){
		
		

		int searchLayers = Utility.Building_PLM | Utility.Environment_PLM | Utility.Quest_PLM | Utility.Plant_PLM;

		GameObject closestBuilding = Utility.GetClosestWith(transform.position, 10, IsBuilding, searchLayers);
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
	
	
	//Returns true if given object is a building
	bool IsBuilding(GameObject theBuilding){

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
		return !IsBuildingNearby() && inLight;
	}

	
	public void ChangeColors( Color theNewColor){
		if( childrenMR != null)
			foreach( MeshRenderer mr in childrenMR)
				for( int i =0; i < mr.materials.Length; i++)
					mr.materials[i].color = theNewColor;
	}



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

	

	// Use this for initialization
	void Start () {
		buildScript = RealBuilding.GetComponent<Buildable>();
		childrenMR = GetComponentsInChildren<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateBuildStatus();
	}


}
