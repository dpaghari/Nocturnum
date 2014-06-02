using UnityEngine;
using System.Collections;

public class DebugTimer : MonoBehaviour {
	//Timer for debugging purposes.
	public dfLabel _dfTimer;

	public double gameTimeSec = 0.0;
	public double gameTimeMin = 0.0;
	private double gameTimeHours = 0.0;

	// Use this for initialization
	void Start () {
		_dfTimer.IsVisible = true;
		InvokeRepeating ("addTime", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if(gameTimeSec > 59){
			gameTimeSec = 0;
			gameTimeMin++;
		}

		if(gameTimeMin > 59){
			gameTimeMin = 0;
			gameTimeHours++;
		}

		_dfTimer.Text = string.Format ("{0:00}:{1:00}:{2:00}",gameTimeHours, gameTimeMin, gameTimeSec);
	}
	void addTime(){
		gameTimeSec++;
	}
}
