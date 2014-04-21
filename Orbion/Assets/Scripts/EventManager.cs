using UnityEngine;
using System.Collections;


//Global events should be defined here.
//Note that any script that adds a listener to an event should also handle the removal of it
//Failure to remove a listener will likely cause bugs, errors, and memory leaks.

//If you use the EventHandlers themselves, you should be checking if they are null


public class EventManager : Singleton<EventManager> {

	protected EventManager() {} // guarantee this will be always a singleton only - can't use the constructor!

	


	public delegate void BuildingEventHandler( Killable killableScript);
	public static event BuildingEventHandler DamagingBuilding;
	public static void OnDamagingBuilding( Killable killableScript){
		if( DamagingBuilding != null) DamagingBuilding( killableScript);
	}



	public delegate void ResearchedEventHandler( Tech theUpgrade);
	public static event ResearchedEventHandler ResearchedUpgrade;
	public static void OnResearchedUpgrade( Tech theUpgrade){
		if( ResearchedUpgrade != null) ResearchedUpgrade( theUpgrade);
	}








	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
