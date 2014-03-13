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
			if( !HasPlayer()){
				Object foundObj = GameObject.FindObjectOfType( typeof( AvatarController));
				if( foundObj)
					Instance._Player = ( foundObj as AvatarController).gameObject;
				else
					Debug.LogWarning("Could not find a Player object.");
			}
			return Instance._Player;
		}
	}

	public static AvatarController AvatarContr{
		get {return Player.GetComponent<AvatarController>(); }
	}




	private GameObject _MainGenerator;

	public static bool HasMainGenerator() { return Instance._MainGenerator != null; }

	public static GameObject MainGenerator{
		get {
			if( !HasMainGenerator()){
				Object foundObj = GameObject.FindObjectOfType( typeof( IsMainGenerator));
				if( foundObj)
					Instance._MainGenerator = ( foundObj as IsMainGenerator).gameObject;
				else
					Debug.LogWarning("Could not find a Main Generator object.");
			}
			return Instance._MainGenerator;
		}
	}

	public static IsMainGenerator MainGenScript() { return MainGenerator.GetComponent<IsMainGenerator>(); }




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
