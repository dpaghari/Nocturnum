//PURPOSE:  Mission UI for Bat Boss Battle

using UnityEngine;
using System.Collections;

public class BatBossBattleMission : MonoBehaviour {
	public dfCheckbox _checkbox_1;
	//public dfCheckbox _checkbox_2;
	//public dfCheckbox _checkbox_3;
	//public dfCheckbox _checkbox_4;
	//public dfCheckbox _checkbox_5;
	public dfLabel _label_mission_clear;
	public dfLabel _label_paused;
	public dfLabel _label_dead;
	string collectString;
	public bool questComplete;
	public bool bossDefeated;
	
	public GameObject questItem;
	
	// Use this for initialization
	void Start () {
		//questItem = GameObject.Find("mission4superconductor");
		questComplete = false;
		//bossDefeated = false;
		_checkbox_1.IsChecked = false;
		//_checkbox_2.IsChecked = false;
		//_checkbox_3.IsChecked = false;
		//_checkbox_4.IsChecked = false;
		//_checkbox_5.IsChecked = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		//if(_checkbox_2.IsChecked == true && _checkbox_3.IsChecked == true && _checkbox_4.IsChecked == true){
		if(_checkbox_1.IsChecked){
			questComplete = true;
			TechManager.missionComplete = true;
			_label_mission_clear.IsVisible = true;
		}
		
		//collectString = string.Format("{0} of {1}", ResManager.TurretCount, ResManager.QuestTurretCount);
		_checkbox_1.Label.Text = "Defeat the Matriarch";

		
		
		
		if(TechManager.defeatedMotherBat){
			_checkbox_1.IsChecked = true;
		}

		
		
		
	
		_label_paused.IsVisible = GameManager.paused;

		
		if(GameManager.PlayerDead){
			_label_dead.IsVisible = true;
		}
		else
			_label_dead.IsVisible = false;
		
	}
	
	
	
	
	
}
