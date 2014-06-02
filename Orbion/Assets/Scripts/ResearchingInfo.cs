//Purpose: Object that holds and processes the time of upgrades

using UnityEngine;
using System.Collections;

public class ResearchingInfo : ScriptableObject {

	//Stores the in-game time when an upgrade should finish
	private float[] researchTimes;
	
	//Stores the closest tech to finish upgrading
	//Used to prevent iterating through whole table everytime
	private Tech closestTech = Tech.none;

	//Constants for readability and iteration
	public const float NOT_RESEARCHING = Mathf.Infinity;
	public const int FIRST_UPGRADE = (int) Tech._upgradesFRONT + 1;
	public const int LAST_UPGRADE = (int) Tech._upgradesEND - 1;



	//Set all the research times
	public void SetAllTimes( float theTime){
		for( int i = ResearchingInfo.FIRST_UPGRADE; i <= ResearchingInfo.LAST_UPGRADE; i++)
			researchTimes[i] = theTime;
	}


	public static ResearchingInfo New(){
		ResearchingInfo newInfo = ScriptableObject.CreateInstance<ResearchingInfo>();
		newInfo.researchTimes = new float[ (int)Tech._upgradesEND + 1];
		newInfo.SetAllTimes( ResearchingInfo.NOT_RESEARCHING);

		return newInfo;
	}


	public float GetFinishTime( Tech theUpgrade){
		return researchTimes[ (int)theUpgrade];
	}

	//Updates the closest tech to finishing every time a new upgrade is inserted
	public void SetFinishTime( Tech theUpgrade, float theFinishTime){
		researchTimes[ (int)theUpgrade] = theFinishTime;
		UpdateClosestTech();
	}

	public bool IsResearching( Tech theUpgrade){
		return GetFinishTime( theUpgrade) != ResearchingInfo.NOT_RESEARCHING;
	}
	
	public bool IsFinished( Tech theUpgrade){
		return Time.time >= GetFinishTime( theUpgrade);
	}


	//Updates and returns the closest tech to finishing
	//returns tech.none if nothing is currently researching
	private Tech GetClosestTech(){
		if( closestTech != Tech.none) return closestTech;
		UpdateClosestTech();
		return closestTech;
	}
	
	
	//Iterates through the table and sets the closest upgrade to finishing
	private void UpdateClosestTech(){
		Tech lowestTimeTech = Tech.none;
		float lowestTime = ResearchingInfo.NOT_RESEARCHING;
		
		for( int i = ResearchingInfo.FIRST_UPGRADE; i <= ResearchingInfo.LAST_UPGRADE; i++){
			Tech currTech = (Tech)i;
			float currTime = GetFinishTime(currTech);
			if ( currTime < lowestTime){
				lowestTimeTech = currTech;
				lowestTime = currTime;
			}
		}
		
		closestTech = lowestTimeTech;
	}


	//If there are no upgrades that are finished, returns Tech.none
	public Tech GetFinishedUpgrade(){
		Tech techToCheck = GetClosestTech();

		if( techToCheck != Tech.none && IsFinished( techToCheck))
			return techToCheck;

		return Tech.none;
	}


	//Increment level activated by UpdateStatus instead of event because we want to make sure
	//the tech maanger's level has updated before all other events run.
	private void IncrementUpgradeLevel( Tech theUpgrade){
		TechManager.SetUpgradeLv( theUpgrade, TechManager.GetUpgradeLv( theUpgrade) + 1);
		//Debug.Log ( string.Format( "{0} is now level {1}!", theUpgrade, TechManager.GetUpgradeLv( theUpgrade)));
	}


	//Check if any upgrades are done and run the appropiate event
	public void UpdateStatus(){
		Tech theFinishedUpgrade = GetFinishedUpgrade();
		while( theFinishedUpgrade != Tech.none){
			IncrementUpgradeLevel( theFinishedUpgrade);
			EventManager.OnResearchedUpgrade( theFinishedUpgrade);
			SetFinishTime( theFinishedUpgrade, ResearchingInfo.NOT_RESEARCHING);
			closestTech = Tech.none;
			theFinishedUpgrade = GetFinishedUpgrade();
		}
	}

}
