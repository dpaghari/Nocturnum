// PURPOSE:  Script in charge of Screen Text after selecting the Difficulty Settings.  Checks if the player presses enter and moves to the next 
// string.  It also moves to the next string after a timer.

using UnityEngine;
using System.Collections;

public class StoryText : MonoBehaviour {


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
		storyStrings = new string[3];
		//part = new bool[2];
		count = 0;
		numStorylines = 2;

		storyStrings[0] = "Earth has become too overpopulated that world\nleaders have decided to look at alternative living in space.\nBecause of how lucrative real estate has become\ndue to the high demand of land and the limited availability,\ncorporations from around the world race to find\nhabitable planets for mankind, with sole ownership of a planet\ngoing to whichever corporation finds and terraforms it first.";		
		storyStrings[1] = "Lustre Corp is one such corporation.\nIn fact, Lustre Corp is currently the world leader\nin success stories for terraforming.\nLustre trains its agents with Lustre Corp. technology\ncapable of making these agents efficient one man armies,\nallowing them to spread their agents and\nfind planets faster.";
		storyStrings[2] = "A strange planet called Nocturnum\nhas been discovered by Lustre's Researchers.\nThey immediately immobilize one of their top agents, Luna,\nto investigate the state of the planet.\nLustre is interested in Nocturnum ability to sustain life\ndespite its lack of solar energy\n and to harness that ability for humanity's future.\nOr so they say...";



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
				AutoFade.LoadLevel("tutorial", 2.0f, 2.0f, Color.black);


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
