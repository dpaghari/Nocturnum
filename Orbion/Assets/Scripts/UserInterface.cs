using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour {

	public Texture2D health_bar_100;
	public Texture2D health_bar_80;
	public Texture2D health_bar_60;
	public Texture2D health_bar_40;
	public Texture2D health_bar_20;
	public Texture2D health_bar_0;
	public int player_Health;
	private double gameTimeSec = 0.0;
	private double gameTimeMin = 0.0;
	private double gameTimeHours = 0.0;
	public GUISkin uiSkin;
	public GameObject ammoRef;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("addTime", 1, 1);
		ammoRef = GameObject.Find ("player_prefab");
		player_Health = 100;
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
		GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_100.width/6, health_bar_100.height/3), health_bar_100);

		if(player_Health < 80){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_80.width/6, health_bar_80.height/3), health_bar_80);
		} else if(player_Health < 60){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_60.width/6, health_bar_60.height/3), health_bar_60);
		} else if(player_Health < 40){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_40.width/6, health_bar_40.height/3), health_bar_40);
		} else if(player_Health < 20){
			GUI.DrawTexture(new Rect(Screen.width/2-80, Screen.height-85, health_bar_20.width/6, health_bar_20.height/3), health_bar_20);
		} else if(player_Health < 20){
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
	}
}
