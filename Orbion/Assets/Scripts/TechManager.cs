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
	bulletBarrier,
	clipSize,
	lightGrenade,
	lightPath,
	mindControl,
	playerHp,
	orbshot,
	scatter,
	searing,
	seeker,
	shield,
	stealth,
	reloadSpeed,
	_upgradesEND
}



public class TechManager : Singleton<TechManager> {

	protected TechManager() {} // guarantee this will be always a singleton only - can't use the constructor!



	protected TechTree PlayerTech;
	protected int[] UpgrLevels;
	protected int[] NumBuildings;



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
		if( CheckBuilding( building)) return Instance.NumBuildings[(int)building];
		return -1;
	}


	
	public static bool HasBuilding( Tech building){
		return GetNumBuilding( building) > 0;
	}
	


	public static void SetNumBuilding( Tech building, int amt){
		if( CheckBuilding( building)) Instance.NumBuildings[(int)building] = amt;
	}



	public static void AddNumBuilding( Tech building, int amt){
		SetNumBuilding(building, GetNumBuilding(building) + amt);
	}



	public static void RmNumBuilding( Tech building, int amt){
		SetNumBuilding(building, GetNumBuilding(building) - amt);
		if (GetNumBuilding(building) < 0) SetNumBuilding(building, 0);
	}
	


	public static int GetUpgradeLv( Tech upgrade){
		if( CheckUpgrade( upgrade)) return Instance.UpgrLevels[(int)upgrade];
		return -1;
	}



	public static bool HasUpgrade( Tech upgrade){
		return GetUpgradeLv( upgrade) > 0;
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

			if( !IsTechAvaliable( upgrade) && !force){
				string errMsg = string.Format( "Attempt to research {0} when not availiable.", upgrade);
				Debug.LogError( errMsg);
				return;
			}

			Instance.UpgrLevels[(int)upgrade] += 1;
		}
	}



	public static void Reset(){
		for( int techIndex = 0; techIndex < (int)Tech._upgradesEND + 1; techIndex++){
			Instance.NumBuildings[techIndex] = 0;
			Instance.UpgrLevels[techIndex] = 0;
		}
		Instance.PlayerTech = TechTree.MakeDefault();
	}



	void Awake () {
		//Could reduce the size of arrays by fitting it to # of entries of a type using the FRONT and END,
		//but choosing not to since we would need to offset the value of the Upgrades enums by _buildingEND
		//For now, would rather reduce code error potential than memory usage
		UpgrLevels = new int[ (int)Tech._upgradesEND + 1];
		NumBuildings = new int[ (int)Tech._upgradesEND + 1];

		PlayerTech = TechTree.MakeDefault();
	}



	// Use this for initialization
	void Start () {
	
	}
	


	// Update is called once per frame
	void Update () {
	
	}



}
