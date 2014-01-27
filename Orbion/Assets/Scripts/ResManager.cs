using UnityEngine;
using System.Collections;

public class ResManager : Singleton<ResManager> {

	protected ResManager() {} // guarantee this will be always a singleton only - can't use the constructor!

	public float LightRes {get; protected set;} // not named Light because that's a Unity class
	public int MaxEnergy {get; protected set;}
	public int UsedEnergy {get; protected set;}


	//Have both Add and Remove functions since costs are likely to be stored
	//as a positive number so adding a negative cost reads awkwardly.
	public static void AddLight( float amt) { Instance.LightRes += amt;}
	public static void RmLight( float amt) { Instance.LightRes -= amt;}
	public static void AddMaxEnergy( int amt) { Instance.MaxEnergy += amt;}
	public static void RmMaxEnergy( int amt) { Instance.MaxEnergy -= amt;}
	public static void AddUsedEnergy( int amt) { Instance.UsedEnergy += amt;}
	public static void RmUsedEnergy( int amt) { Instance.UsedEnergy -= amt;}


	public void Reset () {
		Instance.LightRes = 0;
		Instance.MaxEnergy = 0;
		Instance.UsedEnergy = 0;
	}



	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	*/
}
