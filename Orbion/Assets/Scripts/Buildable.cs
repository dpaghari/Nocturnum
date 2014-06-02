// Purpose: General properties of structures that the player can build



using UnityEngine;
using System.Collections;

public class Buildable : MonoBehaviour {

	//Lumen & Energy cost to buy this building
	public int lumenCost = 0;
	public int energyCost = 0;

	//Time(sec) required to build this structure
	public float buildTime = 10f;

	//The type of building this is, in respect to the tech tree
	public Tech TechType = Tech.none;

	//Whether this building needs to be built within light
	public bool requiresLight = true;

	//The prefab used for UI when the player buys this building
	public GameObject hologram;

	//Radius of the circular region that other buildings cannot be built in
	//Used to prevent building structure too close to each other
	public float contactRadius = 2.0f;


	void OnEnable(){
		TechManager.AddNumBuilding( TechType, 1);
	}

	void OnDisable(){
		TechManager.RmNumBuilding( TechType, 1);
	}



}
