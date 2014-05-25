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
	public bool questComplete;
	public bool bossDefeated;
	
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
		
		
		
		if(TechManager.hasGeyser == true && TechManager.haslightFist == true && _checkbox_3.IsChecked == true){
			questComplete = true;
			TechManager.missionComplete = true;
			_label_mission_clear.IsVisible = true;
		}
		
		collectString = string.Format("{0} of {1}", ResManager.LGCoreCharges, ResManager.LGCoreMaxCharges);
		_checkbox_3.Label.Text = "Power Ship Cores: " + collectString;
		if(TechManager.hasGeyser){
			_checkbox_1.IsChecked = true;
		}
		if(TechManager.haslightFist){
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
