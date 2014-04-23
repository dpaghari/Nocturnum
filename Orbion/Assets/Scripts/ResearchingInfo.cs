using UnityEngine;
using System.Collections;

public class ResearchingInfo : ScriptableObject {

	private float[] researchTimes; //stores the in-game time when an upgrade should finish
	private Tech closestTech = Tech.none;

	public const float NOT_RESEARCHING = Mathf.Infinity;
	public const int FIRST_UPGRADE = (int) Tech._upgradesFRONT + 1;
	public const int LAST_UPGRADE = (int) Tech._upgradesEND - 1;


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

	public void SetFinishTime( Tech theUpgrade, float theFinishTime){
		researchTimes[ (int)theUpgrade] = theFinishTime;
	}

	public bool IsResearching( Tech theUpgrade){
		return GetFinishTime( theUpgrade) != ResearchingInfo.NOT_RESEARCHING;
	}
	
	public bool IsFinished( Tech theUpgrade){
		return Time.time >= GetFinishTime( theUpgrade);
	}


	//returns tech.none if nothing is currently researching
	private Tech GetClosestTech(){
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

		return lowestTimeTech;
	}
	

	//If there are no upgrades that are finished, returns Tech.none
	public Tech GetFinishedUpgrade(){
		if( closestTech == Tech.none)
			closestTech = GetClosestTech();

		if( closestTech != Tech.none && IsFinished( closestTech))
			return closestTech;

		return Tech.none;
	}


	//Increment level activated by UpdateStatus instead of event because we want to make sure
	//the tech maanger's level has updated before all other events run.
	private void IncrementUpgradeLevel( Tech theUpgrade){
		TechManager.SetUpgradeLv( theUpgrade, TechManager.GetUpgradeLv( theUpgrade) + 1);
		Debug.Log ( string.Format( "{0} is now level {1}!", theUpgrade, TechManager.GetUpgradeLv( theUpgrade)));
	}


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
