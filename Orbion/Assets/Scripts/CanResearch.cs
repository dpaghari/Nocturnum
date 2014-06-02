//Purpose: Allows player to bring up and interact with the build menu

using UnityEngine;
using System.Collections;



public class CanResearch : MonoBehaviour {

	public AudioClip errBuild;
	public dfPanel _upgradeWheelPanel;

	private CanBuild buildScript;

	//Slows down game everytime player opens up upgrade menu
	private float slowDownRatio = 0.5f;
	private float originalFixedUpdate = 0.02f;
	private bool _inUpgradeMenu = false;
	public bool MenuUp{
		get{ return _inUpgradeMenu;}
		
		private set{
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
	//If the tech is also an upgrade, it must not be researching already
	public bool MeetsRequirement(Tech theUpgr){
		if( !TechManager.IsTechAvaliable( theUpgr)){ 
			audio.PlayOneShot(errBuild, 0.5f);
			return false;

		}
		if (ResManager.Lumen < TechManager.GetUpgradeLumenCost( theUpgr)){ 
			audio.PlayOneShot(errBuild, 0.5f);
			return false;

		}

		if ( ResManager.Energy < TechManager.GetUpgradeEnergyCost( theUpgr)) {
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
		ResManager.RmEnergy( TechManager.GetUpgradeEnergyCost( theUpgr));
		TechManager.Research( theUpgr);
	}


	public void OpenMenu(){
		MenuUp = true;
		_upgradeWheelPanel.IsVisible = true;
		//inUpgradeMenu = true;
	}
	
	public void CloseMenu(){
		MenuUp = false;
		_upgradeWheelPanel.IsVisible = false;
		//inUpgradeMenu = false;
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
			//Debug.Log("Pressing Lightfist");
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



	void Awake(){
		originalFixedUpdate = Time.fixedDeltaTime;
	}


	void Start () {
		MenuUp = false;
		_upgradeWheelPanel.IsVisible = false;
		buildScript = GetComponent<CanBuild>();
	}


	// Update is called once per frame
	void Update () {

		if(MenuUp)
			GetComponent<CanShoot>().ResetFiringTimer();
		
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
			}

			if(Input.GetKeyDown(KeyCode.Escape))
				CloseMenu();

		}
	
	}
}
