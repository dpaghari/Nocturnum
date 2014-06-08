// PURPOSE:  Keeps track of Tech functions.  Also in charge of keeping track of completion of Mission Objectives and moving on to the next level.  



using UnityEngine;
using System.Collections;
using System.Collections.Generic;



//The 4 underscored enums are used to test whether a Tech is a building or upgrade.
//They should not normally be used outside of this class.
//All buildings should go between _buildingsFront and _buildingsEND.
//All upgrades should go between _upgradesFRONT and _upgradesEND.
public enum Tech {
	none = 0,
	_buildingsFRONT,
	ballistics,
	fortification,
	generator,
	genome,
	incendiary,
	medbay,
	photon,
	refraction,
	spotlight,
	turret,
	wall,
	_buildingsEND,
	_upgradesFRONT,
	buildingHp,
	buildingLightRange,
	bulletAbsorber,
	clipSize,
	lightFist,
	lightGrenade,
	lightPath,
	mindControl,
	moveSpeed,
	playerHp,
	orbshot,
	scatter,
	searing,
	seeker,
	spotlightResearch,
	stealth,
	reloadSpeed,
	turretResearch,
	ricochet,
	_upgradesEND
}







public class TechManager : Singleton<TechManager> {

	protected TechManager() {} // guarantee this will be always a singleton only - can't use the constructor!



	protected TechTree PlayerTech;
	protected int[] UpgrLevels;
	protected int[] NumBuildings;
	protected UpgradeCostTable UpgradeCosts;
	protected ResearchingInfo researchProgress;




	public static bool missionComplete = false;
	public static string currLevel;


	//-----------Mission 1 variables------------//
	// Temporary should be moved into a Questmanager 
	public static bool hasTurret = false;
	public static bool hasGenerator = false;
	public static bool hasScatter = false;
	public static bool hasMedbay = false;
	public static bool hasWolves = false;
	public static bool hasBeatenWolf = false;
	public static DumbTimer timerScript = DumbTimer.New(5.0f, 1.0f);

	//------------------------------------------//

	//------------Mission 2 variables-----------//
	public static bool haslightFist = false;
	public static int hitByFist = 0;
	public static bool hasGeyser = false;
	//------------------------------------------//

	//------------Mission 3 variables-----------//

	public static bool foundSC = false;
	public static bool transportedSC = false;
	public static bool hasTurrets = false;

	//-------------------------------------------//

	//------------Mission 4 variables------------//
	public static bool isCharged = false;
	public static bool transportedBat = false;
	public static bool foundBat = false;
	//-------------------------------------------//

	//------------Bat Boss Battle----------------//
	public static bool defeatedMotherBat = false;

	//-------------------------------------------//





	public static ResearchingInfo ResearchProgress(){ return Instance.researchProgress;}

	public static bool IsBuilding( Tech theTech){
		return theTech > Tech._buildingsFRONT && theTech < Tech._buildingsEND;
	}



	public static bool IsUpgrade( Tech theTech){
		return theTech > Tech._upgradesFRONT && theTech < Tech._upgradesEND;
	}



	private static bool CheckBuilding( Tech building){
		if( IsBuilding( building)) return true;

		Debug.LogError( string.Format("{0} is not a building.", building.ToString()));
		return false;
	}



	private static bool CheckUpgrade ( Tech upgrade){
		if( IsUpgrade( upgrade)) return true;


		Debug.LogError( string.Format ("{0} is not an upgrade.", upgrade.ToString()));
		return false;

	}



	public static int GetNumBuilding( Tech building){
		if(TechManager.applicationIsQuitting) return int.MinValue;
		if( CheckBuilding( building)) return Instance.NumBuildings[(int)building];
		return int.MinValue;
	}


	
	public static bool HasBuilding( Tech building){
		return GetNumBuilding( building) > 0;
	}
	


	public static void SetNumBuilding( Tech building, int amt){
		if(TechManager.applicationIsQuitting) return;
		if( CheckBuilding( building)) Instance.NumBuildings[(int)building] = amt;
	}



	public static void AddNumBuilding( Tech building, int amt){
		SetNumBuilding(building, GetNumBuilding(building) + amt);
		if( building == Tech.generator) hasGenerator = true;
		if( building == Tech.medbay) hasMedbay = true;
	}



	public static void RmNumBuilding( Tech building, int amt){
		SetNumBuilding(building, GetNumBuilding(building) - amt);
		if (GetNumBuilding(building) < 0) SetNumBuilding(building, 0);
	}
	


	public static int GetUpgradeLv( Tech upgrade){
		if( CheckUpgrade( upgrade)) return Instance.UpgrLevels[(int)upgrade];
		return -1;
	}

	
	public static void SetUpgradeLv( Tech upgrade, int newLevel){
		if( CheckUpgrade( upgrade)) Instance.UpgrLevels[(int)upgrade] = newLevel;
	}


	public static bool HasUpgrade( Tech upgrade){
		return GetUpgradeLv( upgrade) > 0;
	}



	public static float GetUpgradeLumenCost( Tech upgrade){
		if( CheckUpgrade( upgrade)) return Instance.UpgradeCosts.costTable[upgrade].lumen;
		return 0;
	}


	public static int GetUpgradeEnergyCost( Tech upgrade){
		if( CheckUpgrade( upgrade)) return Instance.UpgradeCosts.costTable[upgrade].energy;
		return 0;
	}


	public static float GetUpgradeTime( Tech upgrade){
		if( CheckUpgrade( upgrade)) return Instance.UpgradeCosts.costTable[upgrade].researchTime;
		return 0;
	}


	public static UpgradeCostStruct GetUpgradeCosts( Tech upgrade){
		if( CheckUpgrade( upgrade)) return Instance.UpgradeCosts.costTable[upgrade];
		return new UpgradeCostStruct(0, 0, 0);
	}




	//Checks whether theTech is available.
	//As of now this is determined by:
	//  for each PreReq in the set,
	//  	if its requirement is a building/upgrade
	//			if we don't have it built/upgraded then obviously it's not satisfied
	//			otherwise, we check if that upgrade is availiable
	//				if any of them are not, then it is also not satisfied
	//  if any of the prereqs were not satisfied, theTech is not availiabe
	//  otherwise all the prereqs are satisfied (or there were no prereqs) and we are availiable
	public static bool IsTechAvaliable( Tech theTech){
		PreReqSet thePreReqs = Instance.PlayerTech.GetReq( theTech);
		foreach (Tech PreReq in thePreReqs){
			if ( IsBuilding( PreReq) && !HasBuilding( PreReq))
				return false;
			else {
				if ( IsUpgrade( PreReq) && !HasUpgrade( PreReq))
					return false;
			}
			
			if ( ! IsTechAvaliable(PreReq) )
				return false;
				
		}
		return true;
	}



	//For now it just instantly adds to the level.
	//By default, makes an error if try to research an upgrade that isn't availiable.
	//Pass force = true to bypass this.
	public static void Research( Tech upgrade, bool force = false){
		if( CheckUpgrade( upgrade)){
			// Mission 1 check for researching Scattershot
			if(upgrade == Tech.scatter){
				if(!hasScatter)
				hasScatter = true;
			}
			// Mission 2 check for researching LightFist
			if(upgrade == Tech.lightFist){
				if(!haslightFist)
					haslightFist = true;
			}

			if( !force){
				
				if( !IsTechAvaliable( upgrade)){
					string errMsg = string.Format( "Attempt to research {0} when not availiable.", upgrade);
					Debug.LogError( errMsg);
					return;
				}
				if( ResearchProgress().IsResearching( upgrade)){
					string errMsg = string.Format( "Attempt to research {0} when already in progress.", upgrade);
					Debug.LogError( errMsg);
					return;
				}
			}
			ResearchProgress().SetFinishTime( upgrade, Time.time + GetUpgradeTime( upgrade));


		}
	}


	// Reset quest variables
	public static void Reset(){
		for( int techIndex = 0; techIndex < (int)Tech._upgradesEND + 1; techIndex++){
			Instance.NumBuildings[techIndex] = 0;
			Instance.UpgrLevels[techIndex] = 0;
		}
		Instance.PlayerTech = TechTree.MakeDefault();
		missionComplete = false;
		hasMedbay = false;
		hasGenerator = false;
		hasWolves = false;
		hasScatter = false;
		hasBeatenWolf = false;
		haslightFist = false;
		hasGeyser = false;
		hasTurrets = false;
		foundSC = false;
		transportedSC = false;
		hitByFist = 0;
		isCharged = false;
		transportedBat = false;
		foundBat = false;
		defeatedMotherBat = false;
	}



	void Awake () {
		//Could reduce the size of arrays by fitting it to # of entries of a type using the FRONT and END,
		//but choosing not to since we would need to offset the value of the Upgrades enums by _buildingEND
		//For now, would rather reduce code error potential than memory usage
		UpgrLevels = new int[ (int)Tech._upgradesEND + 1];
		NumBuildings = new int[ (int)Tech._upgradesEND + 1];

		PlayerTech = TechTree.MakeDefault();
		UpgradeCosts = UpgradeCostTable.InitTable();
		researchProgress = ResearchingInfo.New();
	}


	// Use this for initialization
	void Start () {

	}


	// Resets the Tech and the Resources
	public static void CompleteMission(){
		if(timerScript.Finished()){
			ResManager.Reset();
			TechManager.Reset();
			MetricManager.Reset();
			checkLevel();
			timerScript.Reset();
			
		}

	}

	//Checks the current level and moves to the next level in the sequence
	public static void checkLevel(){

		currLevel = Application.loadedLevelName;
		switch(currLevel){
		
		case "tutorial" :
			AutoFade.LoadLevel("loadscreen", 2.0f, 2.0f, Color.black);
			break;

		case "loadscreen" :
			AutoFade.LoadLevel("level1", 2.0f, 2.0f, Color.black);
			break;
		
		case "level1" :
			AutoFade.LoadLevel("level2", 2.0f, 2.0f, Color.black);
			break;
			
		case "level2" :
			AutoFade.LoadLevel("level3", 2.0f, 2.0f, Color.black);
			break;

		case "level3" :
			AutoFade.LoadLevel("level4", 2.0f, 2.0f, Color.black);
			break;

		case "level4" :
			AutoFade.LoadLevel("BatBossBattle", 2.0f, 2.0f, Color.black);
			break;

		case "BatBossBattle" :
			AutoFade.LoadLevel("Credits", 2.0f, 2.0f, Color.white);
			break;

		case "Credits"	:
			AutoFade.LoadLevel("scene_title", 2.0f, 2.0f, Color.white);
			break;
		
		default:
			break;


		}
	}

	// Update is called once per frame
	void Update () {

		if(missionComplete){
			timerScript.Update();
			CompleteMission();
			
		}
	
		ResearchProgress().UpdateStatus();
	}



}
