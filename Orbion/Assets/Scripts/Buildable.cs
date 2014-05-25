using UnityEngine;
using System.Collections;

public class Buildable : MonoBehaviour {
	public int cost = 0;
	public int energyCost = 0;
	public float buildTime = 10f;
	public Tech TechType = Tech.none;
	public GameObject hologram;

	//How close can an edge of an adjacent building be
	public float contactRadius = 2.0f;


	// Use this for initialization
	void Start () {
		TechManager.AddNumBuilding( TechType, 1);
		
	}
	void OnDestroy() {
		TechManager.RmNumBuilding( TechType, 1);
		ResManager.AddEnergy( energyCost);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
