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
	public Texture2D button_lightFist;
	public AudioClip errBuild;
	private CanBuild buildScript;
	public dfPanel _upgradeWheelPanel;

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
	public bool MeetsRequirement(Tech theUpgr){
		if( !TechManager.IsTechAvaliable( theUpgr)){ 
			audio.PlayOneShot(errBuild, 0.5f);
			return false;

		}
		if (ResManager.Lumen < TechManager.GetUpgradeLumenCost( theUpgr)){ 
			audio.PlayOneShot(errBuild, 0.5f);
			return false;

		}

		float neededMaxEnergy = ResManager.UsedEnergy + TechManager.GetUpgradeEnergyCost( theUpgr);
		if ( neededMaxEnergy > ResManager.MaxEnergy) {
			audio.PlayOneShot(errBuild, 0.5f);
			return false;
		}


		if( TechManager.IsUpgrade( theUpgr)){

			if( TechManager.ResearchProgress().IsResearching( theUpgr)){
				audio.PlayOneShot(errBuild, 0.5f);
				return false;

			}

		}

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
		_upgradeWheelPanel.IsVisible = true;
		inUpgradeMenu = true;
	}
	
	public void CloseMenu(){
		MenuUp = false;
		_upgradeWheelPanel.IsVisible = false;
		inUpgradeMenu = false;
	}






	public void GetClipSize(){
		if(MeetsRequirement(Tech.clipSize)){
			DoResearch(Tech.clipSize);
			CloseMenu();
		}
	}

	public void GetScattershot(){
		if(MeetsRequirement(Tech.scatter)){
			DoResearch(Tech.scatter);
			CloseMenu();
		}
	}

	public void GetLightGrenade(){
		if(MeetsRequirement(Tech.lightGrenade)){
			DoResearch(Tech.lightGrenade);
			CloseMenu();
		}
	}

	public void GetRicochet(){
		if(MeetsRequirement(Tech.ricochet)){
			DoResearch(Tech.ricochet);
			CloseMenu();
		}
	}

	public void GetLightFist(){
		if(MeetsRequirement(Tech.lightFist)){
			Debug.Log("Pressing Lightfist");
			DoResearch(Tech.lightFist);
			CloseMenu();
		}
	}

	public void GetSeeker(){
		if(MeetsRequirement(Tech.seeker)){
			DoResearch(Tech.seeker);
			CloseMenu();
		}
	}

	/*void OnGUI() {
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
			if(GUI.Button(new Rect(Screen.width/2-192,Screen.height - 280,128,128), button_lightFist)) {
				if(MeetsRequirement(Tech.lightFist)){
					Debug.Log("Pressing Lightfist");
					DoResearch(Tech.lightFist);
					CloseMenu();
				}
				
			}



		}
	
	}*/

	void Awake(){
		originalFixedUpdate = Time.fixedDeltaTime;
	}

	// Use this for initialization
	void Start () {
		MenuUp = false;
		_upgradeWheelPanel.IsVisible = false;
		buildScript = GetComponent<CanBuild>();
	}


	// Update is called once per frame
	void Update () {
		if(MenuUp){
			GetComponent<CanShoot>().ResetFiringTimer();
		}
		if(GameManager.KeysEnabled){
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
			if(Input.GetKeyDown(KeyCode.Escape))
				CloseMenu();

		}
	
	}
}
