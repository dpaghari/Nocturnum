using UnityEngine;
using System.Collections;


//Global stuff for miscellanous objects
//If a lot of functionality is being added to a certain object in here,
//it should be moved to it's own separate singleton class
public class GameManager : Singleton<GameManager> {

	protected GameManager() {} // guarantee this will be always a singleton only - can't use the constructor!

	public static bool KeysEnabled;

	private GameObject _Player;

	public static bool HasPlayer() { return Instance._Player != null; }

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


	//sticking it here for now
	private GameObject ArrowPrefab;
	private static void BuildingAttack( Killable killableScript){
		//Figure out better way to prevent creating arrows near each other
		//This way seems to add way too much in an attemp to optimize: requiring colliders, and doing physics calcs
		//Maybe store references to all arrows created and search against it?
		//return Utility.GetClosestWith(newArrowPos, searchRange, Utility.GoHasComponent<NotificationArrow>) != null;
		Instantiate(Instance.ArrowPrefab, killableScript.transform.position, Quaternion.identity);
	}

	void OnEnable(){
		EventManager.DamagingBuilding += BuildingAttack;
	}
	
	
	void OnDisable(){
		EventManager.DamagingBuilding -= BuildingAttack;
	}


	// Use this for initialization
	void Start () {
		KeysEnabled = true;
		ArrowPrefab =  Resources.Load( "ArrowNotification", typeof(GameObject)) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
