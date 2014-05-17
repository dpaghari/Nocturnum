using UnityEngine;
using System.Collections;

public class HandleDisabling : MonoBehaviour
{
		public dfLabel notEnoughLumen;
		public dfLabel notEnoughEnergy;
		public dfLabel preReqsNotMet;
		public dfButton buttonDisabled;
		public GameObject canResearchRef;
		public GameObject canBuildRef;
		public Tech name;
		bool haveLumen;
		bool haveEnergy;
		bool havePreReqs;
		public Rigidbody buildingType;

		// Use this for initialization
		void Start ()
		{
				notEnoughLumen.IsVisible = false;
				notEnoughEnergy.IsVisible = false;
				preReqsNotMet.IsVisible = false;
				buttonDisabled.Enable ();
				haveLumen = true;
				haveEnergy = true;
				havePreReqs = true;
				canResearchRef = GameObject.Find ("player_prefab");
				canBuildRef = GameObject.Find ("player_prefab");

		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!TechManager.IsTechAvaliable (name)) {
						preReqsNotMet.IsVisible = true;
						buttonDisabled.Disable ();
						havePreReqs = false;
				} else {
						havePreReqs = true;
						preReqsNotMet.IsVisible = false;
				}
		
				Buildable buildInfo = buildingType.GetComponent<Buildable> ();
				if (ResManager.Lumen < buildInfo.cost) {
						notEnoughLumen.IsVisible = true;
						buttonDisabled.Disable ();
						haveLumen = false;
				} else if (ResManager.Lumen < TechManager.GetUpgradeLumenCost (name)) {
						notEnoughLumen.IsVisible = true;
						buttonDisabled.Disable ();
						haveLumen = false;
				} else {
						haveLumen = true;
						notEnoughLumen.IsVisible = false;
				}

				float neededMaxEnergyBuilding = buildInfo.energyCost;
				float neededMaxEnergyUpgrades = TechManager.GetUpgradeEnergyCost (name);
				if (neededMaxEnergyBuilding > ResManager.Energy) {
						notEnoughEnergy.IsVisible = true;
						buttonDisabled.Disable ();
						haveEnergy = false;
				} else if (neededMaxEnergyUpgrades > ResManager.Energy) {
						notEnoughLumen.IsVisible = true;
						buttonDisabled.Disable ();
						haveLumen = false;
				} else {
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
