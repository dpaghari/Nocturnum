using UnityEngine;
using System.Collections;

public class StoryText : MonoBehaviour {


	public dfLabel storyLine;
	public DumbTimer timerScript;
	public DumbTimer nextlevelScript;

	
	// Use this for initialization
	void Start () {
	
		timerScript = DumbTimer.New(6.0f, 1.0f);
		nextlevelScript = DumbTimer.New(10.0f);
	}
	
	// Update is called once per frame
	void Update () {
		timerScript.Update();
		if(timerScript.Finished() == true){
			storyLine.Text = " Earth has become too overpopulated that world\n leaders have decided to look at alternative living in space.\n Because of how lucrative real estate has become\n due to the high demand of land and the limited availability,\n corporations from around the world race to find\n habitable planets for mankind,\n with sole ownership of a planet\n going to whichever corporation finds and terraforms it first.";		

		}
		else
			storyLine.Text = "";
			timerScript.Reset();
			
		}

		

}
