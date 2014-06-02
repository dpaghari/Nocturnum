// PURPOSE:  Mission 2 encourages the player to explore more by checking how many Light Geysers they have extracted energy from and power up the ship's core.
// Checks for Using Light Fist on enemies 3 times(teaching player to use equipment), teaches players pressing E is Use and upon using the Light Geyser completing the quest.

using UnityEngine;
using System.Collections;

public class Mission2 : MonoBehaviour {
	public dfCheckbox _checkbox_1;
	public dfCheckbox _checkbox_2;
	public dfCheckbox _checkbox_3;
	public dfCheckbox _checkbox_4;
	//public dfCheckbox _checkbox_5;
	public dfLabel _label_mission_clear;
	public dfLabel _label_paused;
	public dfLabel _label_dead;
	string collectString;
	string punchemString;
	public bool questComplete;
	public bool bossDefeated;
	public int numFistHits = 3;
	
	// Use this for initialization
	void Start () {
		questComplete = false;
		bossDefeated = false;
		_checkbox_1.IsChecked = false;
		_checkbox_2.IsChecked = false;
		_checkbox_3.IsChecked = false;
		_checkbox_4.IsChecked = false;
		//_checkbox_5.IsChecked = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		if(_checkbox_1.IsChecked == true && _checkbox_2.IsChecked == true && _checkbox_3.IsChecked == true){
			questComplete = true;
			TechManager.missionComplete = true;
			_label_mission_clear.IsVisible = true;
		}

		punchemString = string.Format("{0} of {1}",  TechManager.hitByFist, numFistHits);
		_checkbox_2.Label.Text = "Hit 3 Enemies with LightFist: " + punchemString;
		collectString = string.Format("{0} of {1}", ResManager.LGCoreCharges, ResManager.LGCoreMaxCharges);
		_checkbox_3.Label.Text = "Power Ship Cores: " + collectString;
		if(TechManager.hasGeyser){
			_checkbox_1.IsChecked = true;
		}
		if(TechManager.haslightFist && TechManager.hitByFist >= numFistHits){
			_checkbox_2.IsChecked = true;
		}
		if(ResManager.LGCoreCharges >= ResManager.LGCoreMaxCharges){
			_checkbox_3.IsChecked = true;
		}

	


		if(GameManager.AvatarContr.isPaused){
			_label_paused.IsVisible = true;
		}
		else
			_label_paused.IsVisible = false;
		
		if(GameManager.PlayerDead){
			_label_dead.IsVisible = true;
		}
		else
			_label_dead.IsVisible = false;
		
	}
	
	
	
	
	
}
