using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct UpgradeCostStruct{
	public float lumen;
	public int energy;
	public float researchTime;

	public UpgradeCostStruct(float lumenCost, int energyCost, float researchTime){
		this.lumen = lumenCost;
		this.energy = energyCost;
		this.researchTime = researchTime;
	}
}


public class UpgradeCostTable : ScriptableObject {

	public Dictionary<Tech, UpgradeCostStruct> costTable;

	

	public static UpgradeCostTable InitTable(){
		UpgradeCostTable newTable = ScriptableObject.CreateInstance<UpgradeCostTable>();

		float tempLumen = 50;
		int tempEnergy = 10;
		float tempResrTime = 10;

		newTable.costTable = new Dictionary<Tech, UpgradeCostStruct> (){
			{ Tech.buildingHp, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.buildingLightRange, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.bulletAbsorber, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.clipSize, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.groundSlam, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.lightGrenade, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.lightPath, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.mindControl, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.moveSpeed, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.playerHp, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.orbshot, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.scatter, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.searing, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.seeker, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.spotlightResearch, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.stealth, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.reloadSpeed, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
			{ Tech.turretResearch, new UpgradeCostStruct( tempLumen, tempEnergy, tempResrTime) },
		};


	return newTable;
	}


}
