using UnityEngine;
using System.Collections;

public class HandleButtonRequirements : MonoBehaviour
{
	public dfLabel notEnoughLumen;
	public dfLabel notEnoughEnergy;
	public dfLabel nameLabel;
	public dfTextureSprite redBack;
	public dfSprite requireSprite;
	public GameObject canResearchRef;
	public GameObject canBuildRef;
	public Tech name;
	bool haveLumen;
	bool haveEnergy;
	bool havePreReqs;
	public Rigidbody buildingType;
	float neededMaxEnergyBuilding;
	float neededLumenCost;
	float neededMaxEnergyUpgrades;
	float neededLumenCostUpgrade;
	
	//Colors
	Color32 redText;
	Color32 whiteText;
	
	// Use this for initialization
	void Start ()
	{
		/*notEnoughLumen.IsVisible = false;
				notEnoughEnergy.IsVisible = false;
				preReqsNotMet.IsVisible = false;
				buttonDisabled.Enable ();*/
		haveLumen = true;
		haveEnergy = true;
		havePreReqs = true;
		canResearchRef = GameObject.Find ("player_prefab");
		canBuildRef = GameObject.Find ("player_prefab");
		neededMaxEnergyBuilding = 0.0f;
		neededLumenCost = 0.0f;
		neededMaxEnergyUpgrades = 0.0f;
		neededLumenCostUpgrade = 0.0f;
		redText = new Color(255, 0, 0, 1);
		whiteText = new Color(255, 255, 255, 1);
		
		//Checks if it's a building or an upgrade button (that way we
		//only have one script for handling the disabling of both).
		//Grab the necessary information after determining if it's a
		//building or an upgrade.
		if (buildingType != null && TechManager.IsBuilding (name)) {
			Buildable buildInfo = buildingType.GetComponent<Buildable> ();
			neededMaxEnergyBuilding = buildInfo.energyCost;
			neededLumenCost = buildInfo.lumenCost;
		}

	}
	
	
	
	// Update is called once per frame
	void Update ()
	{
		if (TechManager.IsUpgrade (name)) {
			neededMaxEnergyUpgrades = TechManager.GetUpgradeEnergyCost (name);
			neededLumenCostUpgrade = TechManager.GetUpgradeLumenCost(name);
		}

		//Continuously check if the prerequisites are met for the
		//building/upgrade, and alter the button states as needed.
		if (!TechManager.IsTechAvaliable (name)) {
			//preReqsNotMet.IsVisible = true;
			//preReqsNotMet.BackgroundColor = redText;
			//buttonDisabled.Disable ();
			havePreReqs = false;
			//preReqsList.IsVisible = true;
			redBack.IsVisible = true;
			nameLabel.Color = redText;
		} else {
			//preReqsNotMet.IsVisible = false;
			//preReqsList.IsVisible = false;
			redBack.IsVisible = false;
			havePreReqs = true;
			
		}
		
		//Handles what text appears if a button is disabled.
		if (ResManager.Lumen < neededLumenCost || ResManager.Lumen < neededLumenCostUpgrade) {
			//notEnoughLumen.IsVisible = true;
			notEnoughLumen.Color = redText;
			//buttonDisabled.Disable ();
			nameLabel.Color = redText;
			haveLumen = false;
			
		} else {
			haveLumen = true;
			//notEnoughLumen.IsVisible = false;
			notEnoughLumen.Color = whiteText;
		}
		
		
		
		if (neededMaxEnergyBuilding > ResManager.Energy || neededMaxEnergyUpgrades > ResManager.Energy) {
			//notEnoughEnergy.IsVisible = true;
			notEnoughEnergy.Color = redText;
			//buttonDisabled.Disable ();
			nameLabel.Color = redText;
			haveEnergy = false;
		}
		else {
			haveEnergy = true;
			//notEnoughEnergy.IsVisible = false;
			notEnoughEnergy.Color = whiteText;
		}
		
		
		if (haveLumen && haveEnergy && havePreReqs) {
			//buttonDisabled.Enable ();
			//notEnoughLumen.IsVisible = false;
			notEnoughLumen.Color = whiteText;
			//notEnoughEnergy.IsVisible = false;
			notEnoughEnergy.Color = whiteText;
			redBack.IsVisible = false;
			nameLabel.Color = whiteText;
			//preReqsNotMet.IsVisible = false;
		}
		
	}
}