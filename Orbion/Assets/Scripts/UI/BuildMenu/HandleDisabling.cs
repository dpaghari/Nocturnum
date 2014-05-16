using UnityEngine;
using System.Collections;

public class HandleDisabling : MonoBehaviour {
	dfLabel notEnoughLumen;
	dfLabel notEnoughEnergy;
	dfLabel preReqsNotMet;
	dfButton buttonDisabled;
	public GameObject canResearchRef;
	public GameObject canBuildRef;
	//public var name;

	// Use this for initialization
	void Start () {
		notEnoughLumen.IsVisible = true;
		notEnoughEnergy.IsVisible = true;
		preReqsNotMet.IsVisible = true;
		buttonDisabled.Disable();
		canResearchRef = GameObject.Find ("player_prefab");
		canBuildRef = GameObject.Find ("player_prefab");

	}
	
	// Update is called once per frame
	void Update () {
		//if(canResearchRef.GetComponent<CanResearch>().MeetsRequirement(Tech.name)){

		//} else if(ResManager.Lumen > 49){

		//}
	}
}
