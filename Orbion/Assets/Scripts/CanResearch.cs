using UnityEngine;
using System.Collections;

/*
TODO:

20 April 2014 - Will
Upgrades that have hardcoded research effects probably do not work
with new event driven upgrade/research system. Fix this later.

*/

public class CanResearch : MonoBehaviour {

	public bool MenuUp { get; private set;}

	//UI Stuff
	public GUISkin upgradeWheelSkin;
	private float rotAngle = 40;
	private Vector2 pivotPoint;
	public Texture2D button_clipSize;
	public Texture2D button_lightGrenade;
	public Texture2D button_scatterShot;

	private CanBuild buildScript;


	

	//Returns true only if we have enough lumen, energy, and we satisfy the prereqs
	//If the tech is also an upgrade, it must not be already researching
	bool MeetsRequirement(Tech theUpgr){
		if( !TechManager.IsTechAvaliable( theUpgr)) return false;
		if (ResManager.Lumen < TechManager.GetUpgradeLumenCost( theUpgr)) return false;

		float neededMaxEnergy = ResManager.UsedEnergy + TechManager.GetUpgradeEnergyCost( theUpgr);
		if ( neededMaxEnergy > ResManager.MaxEnergy) return false;

		if( TechManager.IsUpgrade( theUpgr))
			if( TechManager.ResearchProgress().IsResearching( theUpgr))
				return false;

		return true;
	}


	//Calls Research and spends resources
	void DoResearch(Tech theUpgr){
		ResManager.RmLumen( TechManager.GetUpgradeLumenCost( theUpgr));
		ResManager.AddUsedEnergy( TechManager.GetUpgradeEnergyCost( theUpgr));
		TechManager.Research( theUpgr);
	}

	int getLumen(Tech upgradeType){
		int upgradeInfo = (int)TechManager.GetUpgradeLumenCost(upgradeType);
		return upgradeInfo;
	}
	
	int getEnergy(Tech upgradeType){
		int upgradeInfo = (int)TechManager.GetUpgradeEnergyCost(upgradeType);
		return upgradeInfo;
	}


	void OnGUI() {
		GUI.skin = upgradeWheelSkin;
		if(MenuUp){
			GetComponent<CanShoot>().ResetFiringTimer();

			if(GUI.Button(new Rect(Screen.width/2-64,Screen.height/2-192,128,128), button_scatterShot)) {
				if(MeetsRequirement(Tech.scatter)){
					DoResearch(Tech.scatter);
					MenuUp = false;
				}
			}
			
			
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if(GUI.Button(new Rect(Screen.width/2+64,Screen.height/2-192,128,128), button_lightGrenade)) {
				if(MeetsRequirement(Tech.lightGrenade)){
					DoResearch(Tech.lightGrenade);
					MenuUp = false;
				}
			}
			
			
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if(GUI.Button(new Rect(Screen.width/2-192,Screen.height/2-192,128,128), button_clipSize)) {
				if(MeetsRequirement(Tech.clipSize)){
					DoResearch(Tech.clipSize);
					MenuUp = false;
				}
			}

		}
	
	}


	// Use this for initialization
	void Start () {
		MenuUp = false;
		buildScript = GetComponent<CanBuild>();
	}


	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.V)){
			if( buildScript != null && !buildScript.MenuUp)
			   MenuUp = !MenuUp;
		}
	
	}
}
