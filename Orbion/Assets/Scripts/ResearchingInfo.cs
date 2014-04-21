using UnityEngine;
using System.Collections;

public class ResearchingInfo : ScriptableObject {

	private float[] researchTimes; //stores the in-game time when an upgrade should finish

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

	//should optimize this
	//If there are no upgrades that are finished, returns Tech.none
	public Tech GetFinishedUpgrade(){
		for( int i = ResearchingInfo.FIRST_UPGRADE; i <= ResearchingInfo.LAST_UPGRADE; i++)
			if( IsFinished( (Tech)i)) return (Tech)i;
		return Tech.none;
	}

	public void UpdateStatus(){
		Tech theFinishedUpgrade = GetFinishedUpgrade();
		while( theFinishedUpgrade != Tech.none){
			EventManager.OnResearchedUpgrade( theFinishedUpgrade);
			SetFinishTime( theFinishedUpgrade, ResearchingInfo.NOT_RESEARCHING);
			theFinishedUpgrade = GetFinishedUpgrade();
		}
	}

}
