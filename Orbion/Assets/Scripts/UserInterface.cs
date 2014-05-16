using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour {

	public Texture2D health_bar_100;
	public Texture2D health_bar_80;
	public Texture2D health_bar_60;
	public Texture2D health_bar_40;
	public Texture2D health_bar_20;
	public Texture2D health_bar_0;
	public Texture2D icon_lumen;
	public Texture2D icon_energy;
	public Texture2D icon_empty;
	public Texture2D icon_scattershot;
	public Texture2D icon_clipsize;
	public Texture2D icon_lightgrenade;
	public Texture2D ammo_bar;
	public double gameTimeSec = 0.0;
	public double gameTimeMin = 0.0;
	private double gameTimeHours = 0.0;
	public GUISkin uiSkin;
	public GameObject ammoRef;
	public GameObject resRef;
	public GameObject healthRef;

	public bool questComplete;
	public bool bossDefeated;


	//Temporary variable placeholders for health, energy, and Light
	public float player_Health;
	public int player_Energy;
	public int player_Light;

	// Use this for initialization
	void Start () {
		questComplete = false;
		bossDefeated = false;


		InvokeRepeating ("addTime", 1, 1);
		ammoRef = GameObject.Find ("player_prefab");
		healthRef = GameObject.Find ("player_prefab");
		//Code for when the stuff is attached to a GameObject.
		//resRef = GameObject.Find("     ");

		//Temporary placeholders for testing numbers.
		player_Health = (float)healthRef.GetComponent<Killable>().currHP/healthRef.GetComponent<Killable>().baseHP;
		//player_Energy = 50;
		player_Light = 30;
	}

	void addTime(){
		gameTimeSec++;
	}
	
	// Update is called once per frame
	void Update () {
		if(TechManager.hasGenerator == true && TechManager.hasScatter == true && TechManager.hasTurret == true && TechManager.hasWolves == true){
			questComplete = true;
		}

	}

	void OnGUI () {

		//GUIStyle uiFont = new GUIStyle();
		//uiFont.font = chintzy;
		GUI.skin = uiSkin;

		// Health Bar
		//
		/*player_Health = (float)healthRef.GetComponent<Killable>().currHP/healthRef.GetComponent<Killable>().baseHP;
		if(player_Health > 0.8){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_100.width/6, health_bar_100.height/3), health_bar_100);
			//Debug.Log (player_Health);
		} else if(player_Health > 0.6){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_80.width/6, health_bar_80.height/3), health_bar_80);
			//Debug.Log (player_Health);
		} else if(player_Health > 0.4){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_60.width/6, health_bar_60.height/3), health_bar_60);
			//Debug.Log (player_Health);
		} else if(player_Health > 0.2){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_40.width/6, health_bar_40.height/3), health_bar_40);
			//Debug.Log (player_Health);
		} else if(player_Health > 0){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_20.width/6, health_bar_20.height/3), health_bar_20);
			//Debug.Log (player_Health);
		} else if(player_Health == 0){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_0.width/6, health_bar_0.height/3), health_bar_0);
			//Debug.Log (player_Health);
		}*/

		//Game Timer
		if(gameTimeSec > 59){
			gameTimeSec = 0;
			gameTimeMin++;
		}
		if(gameTimeMin > 59){
			gameTimeMin = 0;
			gameTimeHours++;
		}
		GUI.Label(new Rect(5, 5, 150, 50), string.Format ("{0:00}:{1:00}:{2:00}",gameTimeHours, gameTimeMin, gameTimeSec));

		//Ammo
		//if(ammoRef.GetComponent<CanShootReload>().currentAmmo == 0) {
		//GUI.Label(new Rect(2, Screen.height/2-85, 150, 50), "Ammo");
		//GUI.DrawTexture(new Rect(0, Screen.height/2-60, ammo_bar.width, ammo_bar.height), ammo_bar);
		if(GameManager.AvatarContr.shootScript.reloading){
			GUI.Label(new Rect(2, Screen.height-30, 150, 50), "Reloading...");
		} else {
			GUI.Label(new Rect(2, Screen.height-30, 150, 50), ammoRef.GetComponent<CanShootReload>().currentAmmo + "/" + ammoRef.GetComponent<CanShootReload>().clipSize);
		}




		//------------------------------- Mission 1----------------------------------------------------------------//



		if(!questComplete){
		//Generator quest
		GUI.Label(new Rect(Screen.width - 400 ,50, 300, 100), "Build a Generator: ");
		if(TechManager.hasGenerator)
			GUI.Label(new Rect(Screen.width - 200 ,50, 300, 100), "Complete");

		// Upgrade Scattershot
		GUI.Label(new Rect(Screen.width - 400 ,100, 300, 100), "Upgrade Scattershot: ");
		if(TechManager.hasScatter)
			GUI.Label(new Rect(Screen.width - 180 ,100, 300, 100), "Complete");

		// Build a Turret
		GUI.Label(new Rect(Screen.width - 400 ,150, 300, 100), "Build a Turret");
		if(TechManager.hasTurret)
			GUI.Label(new Rect(Screen.width - 180 ,150, 300, 100), "Complete");
		//Collectible quest
		string collectString = string.Format("{0}", ResManager.Collectible);
		GUI.Label(new Rect(Screen.width - 400, 200, 300, 100), "Collect 30 Enemy Specimen: ");
		GUI.Label(new Rect(Screen.width - 100, 200, 150, 50), collectString);
		}
		if(TechManager.hasWolves)
			GUI.Label(new Rect(Screen.width - 400 , 250, 300, 100), "Defeat the Alpha Wolf");
		if(TechManager.hasBeatenWolf){
			GUI.Label(new Rect(Screen.width - 140 , 250, 300, 100), "Complete");
			bossDefeated = true;
		}

		if(TechManager.hasGenerator == true && TechManager.hasScatter == true && TechManager.hasTurret == true && TechManager.hasWolves == true && TechManager.hasBeatenWolf == true){
			GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 500, 300), "MISSION CLEAR!");
		}

	


		//---------------------------------------------------------------------------------------------------------//






		//Light
		string lumenString = string.Format("{0}", ResManager.Lumen);
		GUI.Label(new Rect(Screen.width/2-70, 10, 150, 50), lumenString);
		GUI.DrawTexture(new Rect(Screen.width/2-120, 5, icon_lumen.width, icon_lumen.height), icon_lumen);
		//GUI.Label(new Rect(5, Screen.height-25, 150, 50), "Light: " + player_Light);
		//GUI.Label(new Rect(5, Screen.height-25, 150, 50), "Light: " + resRef.GetComponent<ResManager>().LightRes);
		/*
		//Energy
		string energyString = string.Format("{0}/{1}", ResManager.Energy, ResManager.Energy);
		GUI.Label(new Rect(Screen.width/2+60, 10, 220, 50), energyString);
		GUI.DrawTexture(new Rect(Screen.width/2+10, 5, icon_energy.width, icon_energy.height), icon_energy);
		//GUI.Label(new Rect(110, Screen.height-25, 150, 50), "Energy: " + player_Energy);
		//GUI.Label(new Rect(5, Screen.height-25, 150, 50), "Energy: " + resRef.GetComponent<ResManager>().UsedEnergy + "/" + resRef.GetComponent<ResManager>().MaxEnergy);
*/
		//Equipment on middle
		if(!TechManager.HasUpgrade(Tech.lightGrenade)){
			GUI.DrawTexture(new Rect(Screen.width/2-32, Screen.height-37, icon_empty.width, icon_empty.height), icon_empty);
		} else if(TechManager.HasUpgrade(Tech.lightGrenade)){
			GUI.DrawTexture(new Rect(Screen.width/2-32, Screen.height-37, icon_lightgrenade.width, icon_lightgrenade.height), icon_lightgrenade);
		}
		
		//Upgrades acquired
		//Clip Size
		if(!TechManager.HasUpgrade(Tech.clipSize)){
			GUI.DrawTexture(new Rect(Screen.width-40, Screen.height-37, icon_empty.width, icon_empty.height), icon_empty);
		} else if(TechManager.HasUpgrade(Tech.clipSize)){
			GUI.DrawTexture(new Rect(Screen.width-40, Screen.height-37, icon_clipsize.width, icon_clipsize.height), icon_clipsize);
		}
		
		//Scattershot
		if(!TechManager.HasUpgrade(Tech.scatter)){
			GUI.DrawTexture(new Rect(Screen.width-40, Screen.height-64, icon_empty.width, icon_empty.height), icon_empty);
		} else if(TechManager.HasUpgrade(Tech.scatter)){
			GUI.DrawTexture(new Rect(Screen.width-40, Screen.height-64, icon_scattershot.width, icon_scattershot.height), icon_scattershot);
		}

	}
}
