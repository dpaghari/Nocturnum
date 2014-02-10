using UnityEngine;
using System.Collections;

public class EquipmentUser : MonoBehaviour {


	protected float CooldownTimer = 0;
	public EquipmentBehavior CurrEquip;



	//returns if we've finish the cooldown for the current equip
	public bool FinishCooldown(){
		return CooldownTimer >= CurrEquip.Cooldown;
	}



	public void SetCooldown(float ratio){
		CooldownTimer = ratio * CurrEquip.Cooldown;
	}



	public void ResetCooldown(){
		SetCooldown(0);
	}



	//Changes into a new equip
	//Resets timer
	public void ChangeEquip(EquipmentBehavior newEquip){
		CurrEquip.OnSwitchExit();
		CurrEquip = newEquip;
		CurrEquip.OnSwitchEnter();
		ResetCooldown();
	}



	public void UseEquip (Vector3 cursor){
		if ( FinishCooldown()){
			CurrEquip.Action(cursor);
			ResetCooldown();
		}
	}



	void Start () {
		SetCooldown(1.0f);
		if (CurrEquip) CurrEquip.OnSwitchEnter();
	}

	
	void FixedUpdate(){
		CurrEquip.FixedUpdateEB();
	}
	

	void Update () {
		if( !FinishCooldown()) CooldownTimer += Time.deltaTime;
		CurrEquip.UpdateEB();
	}
}
