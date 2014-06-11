//Purpose: stores the cost and research times of upgrades

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//The entries of our table
public struct UpgradeCostStruct{

	//The resource costs of the upgrade
	public float lumen;
	public int energy;

	//Time(sec) it takes to research the upgrade
	public float researchTime;

	public UpgradeCostStruct(float lumenCost, int energyCost, float researchTime){
		this.lumen = lumenCost;
		this.energy = energyCost;
		this.researchTime = researchTime;
	}
}


public class UpgradeCostTable : ScriptableObject {

	public Dictionary<Tech, UpgradeCostStruct> costTable;

	//Creates and returns a table with all of our costs
	public static UpgradeCostTable InitTable(){
		UpgradeCostTable newTable = ScriptableObject.CreateInstance<UpgradeCostTable>();

		float tempLumen = 50;
		int tempEnergy = 10;
		float tempResrTime = 15;

		newTable.costTable = new Dictionary<Tech, UpgradeCostStruct> (){
			//{ Tech.buildingHp, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			//{ Tech.buildingLightRange, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			//{ Tech.bulletAbsorber, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.clipSize, new UpgradeCostStruct( 40, 10, tempResrTime) },
			{ Tech.lightFist, new UpgradeCostStruct( 35, 35, tempResrTime) },
			{ Tech.lightGrenade, new UpgradeCostStruct( 50, 10, tempResrTime) },
			//{ Tech.lightPath, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			//{ Tech.mindControl, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			//{ Tech.moveSpeed, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			//{ Tech.orbshot, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			//{ Tech.playerHp, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.scatter, new UpgradeCostStruct( 50, 15, tempResrTime) },
			//{ Tech.searing, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.seeker, new UpgradeCostStruct( 25, 40, tempResrTime) },
			//{ Tech.spotlightResearch, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			//{ Tech.stealth, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			//{ Tech.reloadSpeed, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.ricochet, new UpgradeCostStruct( 40, 25, tempResrTime) },
			//{ Tech.turretResearch, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
		};


	return newTable;
	}


}
