// PURPOSE:  Script in charge of Screen Text after selecting the Difficulty Settings.  Checks if the player presses enter and moves to the next 
// string.  It also moves to the next string after a timer.

using UnityEngine;
using System.Collections;

public class CreditsText : MonoBehaviour {
	
	
	public dfLabel storyLine;
	public DumbTimer timerScript;
	public DumbTimer nextlevelScript;
	bool[] part;
	string[] storyStrings;
	int count;
	int numStorylines;
	// Use this for initialization
	void Start () {
		timerScript = DumbTimer.New(10.0f, 1.0f);
		//nextlevelScript = DumbTimer.New(10.0f);
		storyStrings = new string[11];
		//part = new bool[2];
		count = 0;
		numStorylines = 10;


		storyStrings[0] = "\t\t\tThanks for Playing the Beta!\n\tStay tuned for more updates on the complete game!\n\t\t\twww.nocturnumgame.com";
		storyStrings[1] = "\t\tProject Lead\n\t\tDaniel Pagharion\n\n\t\tTechnical Director\n\t\tWilliam Situ\n\n\t\tProducers\n\t\tRyan Swartzman\n\t\tDaniel Pagharion\n\n\t\tArt Direction\n\t\tJonah Nobleza\n\t\tDaniel Pagharion\n\n\t\tLead Tester\n\t\tMouhammad Shaar";
		storyStrings[2] = "\t\tCreative Director\n\t\tDaniel Pagharion\n\n\t\tLead Artist\n\t\tJonah Nobleza\n\n\t\t3D Modeling\n\t\tAlec Asperslag\n\t\tBill Lin\n\t\tAndrew Plebanek\n\t\tKristian de Leon\n\n\t\tConcept Art\n\t\tSara Taylor\n\t\tOmar Hamed\n\t\tDarrell Bennett\n\t\tDee Pathirana\n\t\tJonah Nobleza";
		storyStrings[3] = "\t\tTechnical Artists\n\t\tAdam Burns\n\t\tJonah Nobleza\n\t\tDaniel Pagharion\n\t\tWilliam Situ\n\t\tRyan Swartzman\n\n\t\tInterface Artist\n\t\tDaniel Pagharion\n\t\tJonah Nobleza\n\n\t\tAnimation\n\t\tJessica Rojeck\n\t\tAlec Asperslag\n\t\tBill Lin\n\t\tKristian de Leon";
		storyStrings[4] = "\t\tGraphic Designers\n\t\tDaniel Pagharion\n\t\tJonah Nobleza\n\t\tAndrew Plebanek\n\t\tOmar Hamed\n\t\tSara Taylor\n\t\tDarrell Bennett\n\t\tDee Pathirana\n\n\t\tTexture Artists\n\t\tAlec Asperslag\n\t\tBill Lin";
		storyStrings[5] = "\t\tLead Designer\n\t\tDaniel Pagharion\n\n\t\tGameplay Programmers\n\t\tWilliam Situ\n\t\tDaniel Pagharion\n\t\tRyan Swartzman\n\t\tMouhammad Shaar\n\t\tJonah Nobleza\n\n\t\tAI Programmers\n\t\tMouhammad Shaar\n\t\tWilliam Situ\n\t\tRyan Swartzman";
		storyStrings[6] = "\t\tAudio Manager\n\t\tDaniel Pagharion\n\n\t\tSound Designer\n\t\tAiden McKee\n\n\t\tSound Editor\n\t\tAiden McKee\n\n\t\tSound Programmers\n\t\tDaniel Pagharion\n\t\tWilliam Situ";
		storyStrings[7] = "\t\tWriting\n\t\tStarr Pagharion\n\t\tAndrew Plebanek\n\t\tDaniel Pagharion\n\n\t\tTesters\n\t\tMouhammad Shaar\n\t\tWilliam Situ\n\t\tDaniel Pagharion\n\t\tRyan Swartzman\n\t\tJonah Nobleza";
		storyStrings[8] = "\t\tSpecial Thanks\n\t\tFriends and Family\n\t\tUCSC Game Design Department\n\t\tPlaytesters\n\t\tDavid Wessman\n\t\tJim Whitehead\n\n\t\t";
		storyStrings[9] = "\t\tShiny Sparkly Games";
		storyStrings[10] = "\t\tNocturnumgame © 2014";
	}
	
	// Update is called once per frame
	void Update () {
		timerScript.Update();
		if(timerScript.Finished() == false){
			storyLine.Text = storyStrings[count];
		}
		else{
			
			if(count < numStorylines){
				count++;
				timerScript.Reset();
			}
		}
		
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)){
			
			if(count < numStorylines){
				count++;
				timerScript.Reset();
			}
			else
				AutoFade.LoadLevel("scene_title", 2.0f, 2.0f, Color.white);
			
			
		}
		/*
		for(int i = 0; i < part.Length; i++){

			if(!part[i]){
				timerScript.Update();
				 
				if(timerScript.Finished()){

					part[i] = true;
					timerScript.Reset();
				}
				else
					storyLine.Text = storyStrings[i];

			}
		}
		*/
		
	}
	
	
}
