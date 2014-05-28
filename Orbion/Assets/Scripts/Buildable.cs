using UnityEngine;
using System.Collections;

public class Buildable : MonoBehaviour {
	public int cost = 0;
	public int energyCost = 0;
	public Tech TechType = Tech.none;
	public GameObject hologram;
	public int isPoweredTimer = 0;
	public bool isPowered = true;

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
		isPoweredTimer--;
		if (isPoweredTimer < 0) {
			isPowered = false;
			TechManager.RmNumBuilding( TechType, 1);
		}
	}

	public void power(){
		isPoweredTimer = 5;
		if (!isPowered) {
			TechManager.AddNumBuilding( TechType, 1);
			isPowered = true;
		}
	}


}
