using UnityEngine;
using System.Collections;



public class CanResearch : MonoBehaviour {

	//set temporarily public until we make function to turn on/off menu
	public bool MenuUp { get; private set;}

	//UI Stuff
	public GUISkin upgradeWheelSkin;
	private float rotAngle = 40;
	private Vector2 pivotPoint;
	public Texture2D button_clipSize;
	public Texture2D button_lightGrenade;
	public Texture2D button_scatterShot;
	public Texture2D button_seekerShot;
	public Texture2D button_ricochetShot;

	private CanBuild buildScript;

	//Setting inBuildingMode will slowdown/restore time
	private float slowDownRatio = 0.5f;
	private float originalFixedUpdate = 0.02f;
	private bool _inUpgradeMenu = false;
	public bool inUpgradeMenu{
		get{ return _inUpgradeMenu;}
		
		set{
			if( value == true){
				Time.timeScale = slowDownRatio;
				Time.fixedDeltaTime = originalFixedUpdate * slowDownRatio;
			}
			else{
				Time.timeScale = 1.0f;
				Time.fixedDeltaTime = originalFixedUpdate;
			}
			_inUpgradeMenu = value;
		}
		
	}

	

	//Returns true only if we have enough lumen, energy, and we satisfy the prereqs
	//If the tech is also an upgrade, it must not be already researching
	bool MeetsRequirement(Tech theUpgr){
		if( !TechManager.IsTechAvaliable( theUpgr)) return false;
		if (ResManager.Lumen < TechManager.GetUpgradeLumenCost( theUpgr)) return false;

		float neededMaxEnergy = ResManager.UsedEnergy + TechManager.GetUpgradeEnergyCost( theUpgr);
		if ( neededMaxEnergy > ResManager.MaxEnergy) return false;

		if( TechManager.IsUpgrade( theUpgr))
			if( TechManager.ResearchProgress().IsResearching( theUpgr))
				return false;

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

	public void OpenMenu(){
		MenuUp = true;
		inUpgradeMenu = true;
	}
	
	public void CloseMenu(){
		MenuUp = false;
		inUpgradeMenu = false;
	}


	void OnGUI() {
		GUI.skin = upgradeWheelSkin;
		if(MenuUp){
			GetComponent<CanShoot>().ResetFiringTimer();

			if(GUI.Button(new Rect(Screen.width/2-64,Screen.height/2-192,128,128), button_scatterShot)) {
				if(MeetsRequirement(Tech.scatter)){
					DoResearch(Tech.scatter);
					CloseMenu();
				}
			}
			
			
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if(GUI.Button(new Rect(Screen.width/2+64,Screen.height/2-192,128,128), button_lightGrenade)) {
				if(MeetsRequirement(Tech.lightGrenade)){
					DoResearch(Tech.lightGrenade);
					CloseMenu();
				}
			}
			
			
			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if(GUI.Button(new Rect(Screen.width/2-192,Screen.height/2-192,128,128), button_clipSize)) {
				if(MeetsRequirement(Tech.clipSize)){
					DoResearch(Tech.clipSize);
					CloseMenu();
				}
			}

			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if(GUI.Button(new Rect(Screen.width/2+64,Screen.height/2,128,128), button_seekerShot)) {
				if(MeetsRequirement(Tech.seeker)){
					DoResearch(Tech.seeker);
					CloseMenu();
				}

			}

			//GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
			if(GUI.Button(new Rect(Screen.width/2-192,Screen.height/2,128,128), button_ricochetShot)) {
				if(MeetsRequirement(Tech.ricochet)){
					DoResearch(Tech.ricochet);
					CloseMenu();
				}

			}


		}
	
	}

	void Awake(){
		originalFixedUpdate = Time.fixedDeltaTime;
	}

	// Use this for initialization
	void Start () {
		MenuUp = false;
		buildScript = GetComponent<CanBuild>();
	}


	// Update is called once per frame
	void Update () {

		if ( Input.GetKeyDown(KeyCode.V) && buildScript != null && !buildScript.MenuUp){
			//prevents player from placing buildings if they open upgrade menu
			buildScript.CloseMenu(); 

			if( MenuUp)
				CloseMenu();
			else
				OpenMenu();
		}

		if (Input.GetKeyDown(KeyCode.B) && MenuUp && buildScript != null){
			CloseMenu();
			buildScript.OpenMenu();
		}
	
	}
}
