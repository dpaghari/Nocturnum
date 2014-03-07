using UnityEngine;
using System.Collections;

public class Buildable : MonoBehaviour {
	public int cost = 0;
	public int energyCost = 0;
	public Tech TechType = Tech.none;


	// Use this for initialization
	void Start () {
		//TechManager.AddNumBuilding( TechType, 1);
		
	}
	void OnDestroy() {
		//TechManager.RmNumBuilding( TechType, 1);
		//ResManager.RmUsedEnergy( energyCost);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
