using UnityEngine;
using System.Collections;

public class CanResearch : MonoBehaviour {

	private bool menuUp = false;

	//UI Stuff
	public GUISkin upgradeWheelSkin;
	private float rotAngle = 40;
	private Vector2 pivotPoint;

	// Use this for initialization
	void Start () {
	
	}
	

	//Returns true only if we have enough lumen, energy, and we satisfy the prereqs
	bool MeetsRequirement(Tech theUpgr){
		if( !TechManager.IsTechAvaliable( theUpgr)) return false;
		if (ResManager.Lumen < TechManager.GetUpgradeLumenCost( theUpgr)) return false;

		float neededMaxEnergy = ResManager.UsedEnergy + TechManager.GetUpgradeEnergyCost( theUpgr);
		if ( neededMaxEnergy > ResManager.MaxEnergy) return false;

		return true;
	}


	//Calls Research and spends resources
	void DoResearch(Tech theUpgr){
		ResManager.RmLumen( TechManager.GetUpgradeLumenCost( theUpgr));
		ResManager.AddUsedEnergy( TechManager.GetUpgradeEnergyCost( theUpgr));
		TechManager.Research( theUpgr);
	}


	void OnGUI() {
		GUI.skin = upgradeWheelSkin;
		if(menuUp){
			GetComponent<CanShoot>().ResetFiringTimer();

			if(GUI.Button(new Rect(Screen.width/2-200,Screen.height/2-120,128,128), "Scattershot")) {
				if(MeetsRequirement(Tech.scatter)){
					DoResearch(Tech.scatter);
					menuUp = false;
				}
			}

			pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);
			GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if(GUI.Button(new Rect(Screen.width/2-190,Screen.height/2-140,128,128), "Orbshot")) {
				if(MeetsRequirement(Tech.orbshot)){
					DoResearch(Tech.orbshot);

					//Just slapping it here for now until we have a way to to manage bullets for the player.
					//Should make an event manager to broadcast that a upgrade was researched later
					GameManager.AvatarContr.shootScript.bullet = GameManager.AvatarContr.orbBullet;
					menuUp = false;
				}
			}

			GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if(GUI.Button(new Rect(Screen.width/2-195,Screen.height/2-160,128,128), "Light \n" + "Grenade")) {
				if(MeetsRequirement(Tech.lightGrenade)){
					DoResearch(Tech.lightGrenade);
					menuUp = false;
				}
			}

			GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if(GUI.Button(new Rect(Screen.width/2-210,Screen.height/2-180,128,128), "Bullet \n" + "Absorber")) {
				if(MeetsRequirement(Tech.bulletAbsorber)){
					DoResearch(Tech.bulletAbsorber);
					menuUp = false;
				}
			}

			GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if(GUI.Button(new Rect(Screen.width/2-240,Screen.height/2-180,128,128), "Clip Size")) {
				if(MeetsRequirement(Tech.clipSize)){
					DoResearch(Tech.clipSize);

					//here until we have a event manager for upgrades
					GameManager.AvatarContr.shootScript.clipSize += 10 * TechManager.GetUpgradeLv(Tech.clipSize);
					menuUp = false;
				}
			}



		}
	
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.V)){
			menuUp = !menuUp;
		}
	
	}
}
