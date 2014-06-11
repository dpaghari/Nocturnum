using UnityEngine;
using System.Collections;


public class SaveManager : Singleton<SaveManager> {
	
	protected SaveManager() {} // guarantee this will be always a singleton only - can't use the constructor!


	public int numLevelstoLoad;
	public int levelIndex;
	public int difficultySetting;
	private string currLevel;
	private bool lvl1, lvl2, lvl3, lvl4, lvl5, lvl6;
	//private GameObject _Player;
	
	//public static bool HasPlayer() { return Instance._Player != null; }
	/*
	public static GameObject Player{
		get {
			if (!HasPlayer() ) {
				Instance._Player = (GameObject.FindObjectOfType(typeof(AvatarController)) as AvatarController).gameObject;
				if(! HasPlayer() )
					Debug.Log("Error: Could not find a player object.");
			}
			return Instance._Player;
		}
	}
	
	public static AvatarController AvatarContr{
		get {return Player.GetComponent<AvatarController>(); }
	}
	*/

	
	public static void SaveLevel(){
		CheckCurrLevel();
		PlayerPrefs.SetInt("SavedLevels",Instance.levelIndex);
		PlayerPrefs.SetInt("SavedDifficulty", Instance.difficultySetting);

	}

	public static void CheckCurrLevel(){
		Instance.currLevel = Application.loadedLevelName;
		switch(Instance.currLevel){
		
		case "tutorial" :
			Instance.levelIndex = 2;
			Instance.difficultySetting = GameManager.GameDifficulty;
			break;
			
		case "level1" :
			Instance.levelIndex = 3;
			Instance.difficultySetting = GameManager.GameDifficulty;
			break;
			
		case "level2" :
			Instance.levelIndex = 4;
			Instance.difficultySetting = GameManager.GameDifficulty;
			break;
			
		case "level3" :
			Instance.levelIndex = 5;
			Instance.difficultySetting = GameManager.GameDifficulty;
			break;

		case "level4" :
			Instance.levelIndex = 6;
			Instance.difficultySetting = GameManager.GameDifficulty;
			break;
			
		case "BatBossBattle" :
			Instance.levelIndex = 7;
			Instance.difficultySetting = GameManager.GameDifficulty;
			break;

		default:
			break;
		}

	}

	public static int LoadSavedLevels() {

		Instance.numLevelstoLoad = PlayerPrefs.GetInt("SavedLevels");
		return Instance.numLevelstoLoad;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
