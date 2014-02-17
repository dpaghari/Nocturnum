using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradeCostTable : ScriptableObject {

	public Dictionary<Tech, float> LumenCost;
	public Dictionary<Tech, int> EnergyCost;

	public static UpgradeCostTable InitTable(){
		UpgradeCostTable newTable = ScriptableObject.CreateInstance<UpgradeCostTable>();
		
		float tempLumen = 50;
		int tempEnergy = 10;

		newTable.LumenCost = new Dictionary<Tech, float>() {
			{Tech.buildingHp, tempLumen},
			{Tech.buildingLightRange, tempLumen},
			{Tech.bulletAbsorber, tempLumen},
			{Tech.clipSize, tempLumen},
			{Tech.groundSlam, tempLumen},
			{Tech.lightGrenade, tempLumen},
			{Tech.lightPath, tempLumen},
			{Tech.mindControl, tempLumen},
			{Tech.moveSpeed, tempLumen},
			{Tech.playerHp, tempLumen},
			{Tech.orbshot, tempLumen},
			{Tech.scatter, tempLumen},
			{Tech.searing, tempLumen},
			{Tech.seeker, tempLumen},
			{Tech.spotlightResearch, tempLumen},
			{Tech.stealth, tempLumen},
			{Tech.reloadSpeed, tempLumen},
			{Tech.turretResearch, tempLumen},
		};

		newTable.EnergyCost = new Dictionary<Tech, int>() {
			{Tech.buildingHp, tempEnergy},
			{Tech.buildingLightRange, tempEnergy},
			{Tech.bulletAbsorber, tempEnergy},
			{Tech.clipSize, tempEnergy},
			{Tech.groundSlam, tempEnergy},
			{Tech.lightGrenade, tempEnergy},
			{Tech.lightPath, tempEnergy},
			{Tech.mindControl, tempEnergy},
			{Tech.moveSpeed, tempEnergy},
			{Tech.playerHp, tempEnergy},
			{Tech.orbshot, tempEnergy},
			{Tech.scatter, tempEnergy},
			{Tech.searing, tempEnergy},
			{Tech.seeker, tempEnergy},
			{Tech.spotlightResearch, tempEnergy},
			{Tech.stealth, tempEnergy},
			{Tech.reloadSpeed, tempEnergy},
			{Tech.turretResearch, tempEnergy},
		};

	return newTable;
	}

}
