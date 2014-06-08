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
		timerScript = DumbTimer.New(20.0f, 1.0f);
		nextlevelScript = DumbTimer.New(10.0f);
		storyStrings = new string[5];
		//part = new bool[2];
		count = 0;
		numStorylines = 4;
		
		storyStrings[0] = "\t\tThanks for Playing the Beta!\n\tStay tuned for more updates on the complete game!\n\t\twww.nocturnumgame.com";
		storyStrings[1] = "\t\tProject Lead\n\t\tDaniel Pagharion\n\n\t\tLead Developer\n\t\tWilliam Situ\n\n\t\tLead Producers\n\t\tRyan Swartzman\n\t\tDaniel Pagharion\n\n\t\tArt Coordinators\n\t\tJonah Nobleza\n\t\tDaniel Pagharion\n\n\t\tLead Tester\n\t\tMouhammad Shaar";
		storyStrings[2] = "\t\t3D Modeling\n\t\tAlec Asperslag\n\t\tBill Lin\n\t\tAndrew Plebanek\n\n\t\tConcept Art\n\t\tSara Taylor\n\t\tOmar Hamed\n\t\tDarrell Bennett\n\t\tDee Pathirana\n\t\tJonah Nobleza";
		storyStrings[3] = "\t\tAnimation\n\t\tJessica Rojeck\n\t\tAlec Asperslag\n\t\tBill Lin\n\n\t\tMusic\n\t\tAiden McKee\n\n\t\tSound Effects\n\t\tAiden McKee";
		storyStrings[4] = "\t\tWriting\n\t\tStarr Pagharion\n\t\tAndrew Plebanek\n\t\tDaniel Pagharion\n\n\t\tSpecial Thanks\n\t\tThe Food Truck on Science Hill for feeding us\n\t\tUCSC Game Design Department\n\n\t\tNocturnumgame © 2014";
		
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
