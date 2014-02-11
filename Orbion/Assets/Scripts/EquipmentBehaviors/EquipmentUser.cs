using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EquipType{
	NoEquip = 0,
	BulletAbsorber,
	BulletBarrier,
	LightGrenade,
	MindControl,
	Shield,
	Stealth,
	_length
}



//require all our Equipment Behaviors so we don't need to individually add all of them
[RequireComponent (typeof (EB_NoEquip))]
[RequireComponent (typeof (EB_BulletAbsorber))]
//[RequireComponent (typeof (EB_BulletBarrier))]
[RequireComponent (typeof (EB_LightGrenade))]
//[RequireComponent (typeof (EB_MindControl))]
//[RequireComponent (typeof (EB_Shield))]
//[RequireComponent (typeof (EB_Stealth))]


public class EquipmentUser : MonoBehaviour {

	//the current equip that we are using
	public EquipType CurrEquipType;

	//the timer used to keep track of our cooldown
	protected float CooldownTimer = 0;

	//array of all our equipment behaviors
	protected EquipmentBehavior[] Equipments ;



	public EquipmentBehavior GetEquip( EquipType theEquipType){
		return Equipments[(int)theEquipType]; 
	}



	public EquipmentBehavior GetCurrEquip(){ return GetEquip(CurrEquipType);}




	//returns if we've finish the cooldown for the current equip
	public bool FinishCooldown(){
		return CooldownTimer >= GetCurrEquip().Cooldown;
	}



	//sets the cooldown to (ratio * 100)% progress
	public void SetCooldown(float ratio){
		CooldownTimer = ratio * GetCurrEquip().Cooldown;
	}



	public void ResetCooldown(){
		SetCooldown(0);
	}



	//Changes into a new equip
	//Resets timer
	public void ChangeEquip(EquipType newEquipType){
		GetCurrEquip().OnSwitchExit();

		CurrEquipType = newEquipType;
		GetCurrEquip().OnSwitchEnter();
		ResetCooldown();
	}



	//Called when the equip button is pressed
	public void UseEquip (Vector3 cursor){
		if ( FinishCooldown()){
			GetCurrEquip().Action(cursor);
			ResetCooldown();
		}
	}



	void Awake() {
		Equipments = new EquipmentBehavior[(int)EquipType._length];

		Equipments[(int)EquipType.NoEquip] = gameObject.GetComponent<EB_NoEquip>();
		Equipments[(int)EquipType.BulletAbsorber] = GetComponent<EB_BulletAbsorber>();
		//Equipments[(int)EquipType.BulletBarrier] = GetComponent<EB_BulletBarrier>();
		Equipments[(int)EquipType.LightGrenade] = GetComponent<EB_LightGrenade>();
		//Equipments[(int)EquipType.MindControl]  = GetComponent<EB_MindControl>();
		//Equipments[(int)EquipType.Shield] = GetComponent<EB_Shield>();
		//Equipments[(int)EquipType.Stealth] = GetComponent<EB_Stealth>();
	}



	void Start () {
		SetCooldown(1.0f);
		GetCurrEquip().OnSwitchEnter();
	}

	

	void FixedUpdate(){
		GetCurrEquip().FixedUpdateEB();
	}
	


	void Update () {
		if( !FinishCooldown()) CooldownTimer += Time.deltaTime;
		GetCurrEquip().UpdateEB();
	}



}
