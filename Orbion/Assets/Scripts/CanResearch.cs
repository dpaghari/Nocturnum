using UnityEngine;
using System.Collections;

public class CanResearch : MonoBehaviour {

	private bool menuUp = false;

	//UI Stuff
	public GUISkin upgradeWheelSkin;
	private float rotAngle = 40;
	private Vector2 pivotPoint;
	public Texture2D button_clipSize;
	public Texture2D button_lightGrenade;
	public Texture2D button_scatterShot;

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

	int getLumen(Tech upgradeType){
		int upgradeInfo = (int)TechManager.GetUpgradeLumenCost(upgradeType);
		return upgradeInfo;
	}
	
	int getEnergy(Tech upgradeType){
		int upgradeInfo = (int)TechManager.GetUpgradeEnergyCost(upgradeType);
		return upgradeInfo;
	}


	void OnGUI() {
		GUI.skin = upgradeWheelSkin;
		if(menuUp){
			GetComponent<CanShoot>().ResetFiringTimer();

			if(GUI.Button(new Rect(Screen.width/2-64,Screen.height/2-192,128,128), button_scatterShot)) {
				if(MeetsRequirement(Tech.scatter)){
					DoResearch(Tech.scatter);
					menuUp = false;
				}
			}
			
			//pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			/*if(GUI.Button(new Rect(Screen.width/2-192,Screen.height/2-192,128,128), "Orbshot\n" + "Lumen: " + getLumen(Tech.orbshot) + "\n" + "Energy: " + getEnergy(Tech.orbshot))) {
				if(MeetsRequirement(Tech.orbshot)){
					DoResearch(Tech.orbshot);
					
					//Just slapping it here for now until we have a way to to manage bullets for the player.
					//Should make an event manager to broadcast that a upgrade was researched later
					GameManager.AvatarContr.shootScript.bullet = GameManager.AvatarContr.orbBullet;
					menuUp = false;
				}
			}*/
			
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if(GUI.Button(new Rect(Screen.width/2+64,Screen.height/2-192,128,128), button_lightGrenade)) {
				if(MeetsRequirement(Tech.lightGrenade)){
					DoResearch(Tech.lightGrenade);
					menuUp = false;
				}
			}
			
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			/*if(GUI.Button(new Rect(Screen.width/2-192,Screen.height/2-64,128,128), "Bullet \n" + "Absorber\n"  + "Lumen: " + getLumen(Tech.bulletAbsorber) + "\n" + "Energy: " + getEnergy(Tech.bulletAbsorber))) {
				if(MeetsRequirement(Tech.bulletAbsorber)){
					DoResearch(Tech.bulletAbsorber);
					menuUp = false;
				}
			}*/
			
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if(GUI.Button(new Rect(Screen.width/2-192,Screen.height/2-192,128,128), button_clipSize)) {
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
