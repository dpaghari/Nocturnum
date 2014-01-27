using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour {

	public Texture2D health_bar_100;
	public Texture2D health_bar_80;
	public Texture2D health_bar_60;
	public Texture2D health_bar_40;
	public Texture2D health_bar_20;
	public Texture2D health_bar_0;
	private double gameTimeSec = 0.0;
	private double gameTimeMin = 0.0;
	private double gameTimeHours = 0.0;
	public GUISkin uiSkin;
	public GameObject ammoRef;
	public GameObject resRef;

	//Temporary variable placeholders for health, energy, and Light
	public int player_Health;
	public int player_Energy;
	public int player_Light;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("addTime", 1, 1);
		ammoRef = GameObject.Find ("player_prefab");
		//Code for when the stuff is attached to a GameObject.
		//resRef = GameObject.Find("     ");

		//Temporary placeholders for testing numbers.
		player_Health = 100;
		player_Energy = 50;
		player_Light = 30;
	}

	void addTime(){
		gameTimeSec++;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI () {

		//GUIStyle uiFont = new GUIStyle();
		//uiFont.font = chintzy;
		GUI.skin = uiSkin;

		// Health Bar


		if(player_Health > 80){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_100.width/6, health_bar_100.height/3), health_bar_100);
		} else if(player_Health > 60){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_80.width/6, health_bar_80.height/3), health_bar_80);
		} else if(player_Health > 40){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_60.width/6, health_bar_60.height/3), health_bar_60);
		} else if(player_Health > 20){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_40.width/6, health_bar_40.height/3), health_bar_40);
		} else if(player_Health > 0){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_20.width/6, health_bar_20.height/3), health_bar_20);
		} else if(player_Health == 0){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_0.width/6, health_bar_0.height/3), health_bar_0);
		}

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
		if(ammoRef.GetComponent<CanShootReload>().currentAmmo == 0) {
			GUI.Label(new Rect(5, 40, 150, 50), "Reloading...");
		} else {
			GUI.Label(new Rect(5, 40, 150, 50), ammoRef.GetComponent<CanShootReload>().currentAmmo + "/" + ammoRef.GetComponent<CanShootReload>().clipSize);
		}

		//Light
		GUI.Label(new Rect(5, Screen.height-25, 150, 50), "Light: " + player_Light);
		//GUI.Label(new Rect(5, Screen.height-25, 150, 50), "Light: " + resRef.GetComponent<ResManager>().LightRes);

		//Energy
		GUI.Label(new Rect(110, Screen.height-25, 150, 50), "Energy: " + player_Energy);
		//GUI.Label(new Rect(5, Screen.height-25, 150, 50), "Energy: " + resRef.GetComponent<ResManager>().UsedEnergy + "/" + resRef.GetComponent<ResManager>().MaxEnergy);
	}
}
