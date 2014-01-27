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
	defense,
	fence,
	generator,
	genome,
	mechatronic,
	turret,
	weapon,
	_buildingsEND,
	_upgradesFRONT,
	scatter,
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



	private bool CheckBuilding( Tech building){
		if( IsBuilding( building)) return true;

		Debug.LogError( string.Format("{0} is not a building.", building.ToString()));
		return false;
	}



	private bool CheckUpgrade ( Tech upgrade){
		if( IsUpgrade( upgrade)) return true;

		Debug.LogError( string.Format ("{0} is not an upgrade.", upgrade.ToString()));
		return false;
	}



	public int GetNumBuilding( Tech building){
		if( CheckBuilding( building)) return NumBuildings[(int)building];
		return -1;
	}


	
	public bool HasBuilding( Tech building){
		return GetNumBuilding( building) > 0;
	}
	


	public void SetNumBuilding( Tech building, int amt){
		if( CheckBuilding( building)) NumBuildings[(int)building] = amt;
	}



	public void AddNumBuilding( Tech building, int amt){
		if( CheckBuilding( building)) NumBuildings[(int)building] += amt;
	}



	public void RmNumBuilding( Tech building, int amt){
		if( CheckBuilding( building)) NumBuildings[(int)building] -= amt;
	}
	


	public int GetUpgradeLv( Tech upgrade){
		if( CheckUpgrade( upgrade)) return UpgrLevels[(int)upgrade];
		return -1;
	}



	public bool HasUpgrade( Tech upgrade){
		return GetUpgradeLv( upgrade) > 0;
	}



	//Checks whether theTech is available.
	//As of now this is determined by:
	//	if its requirement is none then true
	//  if its requirement is a building/upgrade
	//		check if we have them, then if we do
	//			check if they themselves are an available tech
	//		otherwise, we don't have them and its false
	public bool IsTechAvaliable( Tech theTech){
		Tech theReq = PlayerTech.GetReq( theTech);
		if ( theReq == Tech.none) return true;

		if ( IsBuilding( theReq) && !HasBuilding( theReq))
			return false;
		else {
			if ( IsUpgrade( theReq) && !HasUpgrade( theReq))
				return false;
		} 

		return IsTechAvaliable( theReq);
	}



	//For now it just instantly adds to the level.
	//By default, makes an error if try to research an upgrade that isn't availiable.
	//Pass force = true to bypass this.
	public void Research( Tech upgrade, bool force = false){
		if( CheckUpgrade( upgrade)){

			if( !IsTechAvaliable( upgrade) && !force){
				string errMsg = string.Format( "Attempt to research {0} when not availiable.", upgrade);
				Debug.LogError( errMsg);
				return;
			}

			UpgrLevels[(int)upgrade] += 1;
		}
	}



	public void Reset(){
		for( int techIndex = 0; techIndex < (int)Tech._upgradesEND + 1; techIndex++){
			NumBuildings[techIndex] = 0;
			UpgrLevels[techIndex] = 0;
		}
		PlayerTech = TechTree.MakeDefault();
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
