using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradeCostTable : ScriptableObject {

	public Dictionary<Tech, float> LumenCost;
	public Dictionary<Tech, int> EnergyCost;

	public static UpgradeCostTable InitTable(){
		UpgradeCostTable newTable = ScriptableObject.CreateInstance<UpgradeCostTable>();

		newTable.LumenCost = new Dictionary<Tech, float>() {
			{Tech.buildingHp, 100f},
			{Tech.buildingLightRange, 100f},
			{Tech.bulletAbsorber, 100f},
			{Tech.clipSize, 100f},
			{Tech.groundSlam, 100f},
			{Tech.lightGrenade, 100f},
			{Tech.lightPath, 100f},
			{Tech.mindControl, 100f},
			{Tech.moveSpeed, 100f},
			{Tech.playerHp, 100f},
			{Tech.orbshot, 100f},
			{Tech.scatter, 100f},
			{Tech.searing, 100f},
			{Tech.seeker, 100f},
			{Tech.spotlightResearch, 100f},
			{Tech.stealth, 100f},
			{Tech.reloadSpeed, 100f},
			{Tech.turretResearch, 100f},
		};

		newTable.EnergyCost = new Dictionary<Tech, int>() {
			{Tech.buildingHp, 20},
			{Tech.buildingLightRange, 20},
			{Tech.bulletAbsorber, 20},
			{Tech.clipSize, 20},
			{Tech.groundSlam, 20},
			{Tech.lightGrenade, 20},
			{Tech.lightPath, 20},
			{Tech.mindControl, 20},
			{Tech.moveSpeed, 20},
			{Tech.playerHp, 20},
			{Tech.orbshot, 20},
			{Tech.scatter, 20},
			{Tech.searing, 20},
			{Tech.seeker, 20},
			{Tech.spotlightResearch, 20},
			{Tech.stealth, 20},
			{Tech.reloadSpeed, 20},
			{Tech.turretResearch, 20},
		};

	return newTable;
	}

}
