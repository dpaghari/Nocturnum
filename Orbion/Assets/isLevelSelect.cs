using UnityEngine;
using System.Collections;

public class isLevelSelect : MonoBehaviour {

	public dfButton tutButton, lvl1Butt, lvl2Butt, lvl3Butt, lvl4Butt, lvl5Butt;

	private int numLevelstoLoad;
	// Use this for initialization
	void Start () {
		numLevelstoLoad = SaveManager.LoadSavedLevels();

		tutButton.IsVisible = false;
		lvl1Butt.IsVisible = false;
		lvl2Butt.IsVisible = false;
		lvl3Butt.IsVisible = false;
		lvl4Butt.IsVisible = false;
		lvl5Butt.IsVisible = false;


		tutButton.IsEnabled = false;
		lvl1Butt.IsEnabled = false;
		lvl2Butt.IsEnabled = false;
		lvl3Butt.IsEnabled = false;
		lvl4Butt.IsEnabled = false;
		lvl5Butt.IsEnabled = false;

	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log(numLevelstoLoad);
		if(numLevelstoLoad >= 1){
			tutButton.IsVisible = true;
			tutButton.IsEnabled = true;
		}
		if(numLevelstoLoad >= 2){
			lvl1Butt.IsVisible = true;
			lvl1Butt.IsEnabled = true;
		}
		if(numLevelstoLoad >= 3){
			lvl2Butt.IsVisible = true;
			lvl2Butt.IsEnabled = true;
		}
		if(numLevelstoLoad >= 4){
			lvl3Butt.IsVisible = true;
			lvl3Butt.IsEnabled = true;
		}
		if(numLevelstoLoad >= 5){
			lvl4Butt.IsVisible = true;
			lvl4Butt.IsEnabled = true;
		}
		if(numLevelstoLoad >= 6){
			lvl5Butt.IsVisible = true;
			lvl5Butt.IsEnabled = true;
		}
		//Debug.Log(numLevelstoLoad)
	}






}
