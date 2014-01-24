using UnityEngine;
using System.Collections;

public class IsGenerator : MonoBehaviour {
	public int energyGeneration = 50;


	// Use this for initialization
	void Start () {
		//ResourceManager.Instance.AddMaxEnergy(energyGeneration);
	}

	void OnDestroy() {
		//ResourceManager.Instance.RemoveMaxEnergy(energyGeneration);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
