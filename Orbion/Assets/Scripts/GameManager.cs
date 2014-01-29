using UnityEngine;
using System.Collections;


//Global stuff for miscellanous objects
//If a lot of functionality is being added to a certain object in here,
//it should be moved to it's own separate singleton class
public class GameManager : Singleton<GameManager> {

	protected GameManager() {} // guarantee this will be always a singleton only - can't use the constructor!

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


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
