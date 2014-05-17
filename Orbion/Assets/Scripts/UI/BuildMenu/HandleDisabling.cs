using UnityEngine;
using System.Collections;

public class HandleDisabling : MonoBehaviour
{
		public dfLabel notEnoughLumen;
		public dfLabel notEnoughEnergy;
		public dfLabel preReqsNotMet;
		public dfLabel preReqsList;
		public dfButton buttonDisabled;
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
		if (buildingType != null && TechManager.IsBuilding (name)) {
						Buildable buildInfo = buildingType.GetComponent<Buildable> ();
						neededMaxEnergyBuilding = buildInfo.energyCost;
						neededLumenCost = buildInfo.cost;
				}
		if (TechManager.IsUpgrade (name)) {
			neededMaxEnergyUpgrades = TechManager.GetUpgradeEnergyCost (name);
			neededLumenCostUpgrade = TechManager.GetUpgradeLumenCost(name);
		}
	}
	


// Update is called once per frame
		void Update ()
		{
				if (!TechManager.IsTechAvaliable (name)) {
						preReqsNotMet.IsVisible = true;
						buttonDisabled.Disable ();
						havePreReqs = false;
				preReqsList.IsVisible = true;
				} else {
			preReqsNotMet.IsVisible = false;
			preReqsList.IsVisible = false;
			havePreReqs = true;

		}

		if (ResManager.Lumen < neededLumenCost || ResManager.Lumen < neededLumenCostUpgrade) {
								notEnoughLumen.IsVisible = true;
								buttonDisabled.Disable ();
								haveLumen = false;
				} else {
						haveLumen = true;
						notEnoughLumen.IsVisible = false;
				}


				
		if (neededMaxEnergyBuilding > ResManager.Energy || neededMaxEnergyUpgrades > ResManager.Energy) {
								notEnoughEnergy.IsVisible = true;
								buttonDisabled.Disable ();
								haveEnergy = false;
						}
				 else {
						haveEnergy = true;
						notEnoughEnergy.IsVisible = false;
				}


				if (haveLumen && haveEnergy && havePreReqs) {
						buttonDisabled.Enable ();
						notEnoughLumen.IsVisible = false;
						notEnoughEnergy.IsVisible = false;
						preReqsNotMet.IsVisible = false;
				}

		}
}
