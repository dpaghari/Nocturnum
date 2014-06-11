// PURPOSE:  Mission UI reflected from Tech Manager for Mission 4.  Throws the player in an interesting divide between protecting the base
// and moving the mission objective back to the base.

using UnityEngine;
using System.Collections;

public class Mission4 : MonoBehaviour {
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

	public GameObject questItem;
	
	// Use this for initialization
	void Start () {
		questItem = GameObject.Find("mission4superconductor");
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
		
		
		
		if(_checkbox_2.IsChecked == true && _checkbox_3.IsChecked == true && _checkbox_4.IsChecked == true){
			_checkbox_1.IsChecked = true;
			questComplete = true;
			TechManager.missionComplete = true;
			_label_mission_clear.IsVisible = true;
			MetricManager.setCompletionTime(Time.time);
			MetricManager.calculateScore();
		}
		
		//collectString = string.Format("{0} of {1}", ResManager.TurretCount, ResManager.QuestTurretCount);
		_checkbox_1.Label.Text = "Survive till the Lustre Rover charges";
		_checkbox_2.Label.Text = "Lustre Rover Charge: " + Mathf.Round(questItem.GetComponent<isLevel4QuestItem>().numCharges) + " of " + Mathf.Round(questItem.GetComponent<isLevel4QuestItem>().maxCharges);
		_checkbox_3.Label.Text = "Find Zingbat Youngling";
		_checkbox_4.Label.Text = "Bring Zingbat youngling back to ship";


		
		if(TechManager.foundBat){
			_checkbox_3.IsChecked = true;
		}
		if(TechManager.transportedBat){
			_checkbox_4.IsChecked = true;
		}
		if(TechManager.isCharged){
			_checkbox_2.Label.Text = "Lustre Rover Charge: Complete!";
			_checkbox_2.IsChecked = true;
		}

		
		
		
		
		_label_paused.IsVisible = GameManager.paused;
		
		if(GameManager.PlayerDead){
			_label_dead.IsVisible = true;
		}
		else
			_label_dead.IsVisible = false;
		
	}
	
	
	
	
	
}
