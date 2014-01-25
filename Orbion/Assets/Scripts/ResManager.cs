using UnityEngine;
using System.Collections;

public class ResManager : Singleton<ResManager> {

	protected ResManager() {} // guarantee this will be always a singleton only - can't use the constructor!

	public float LightRes {get; protected set;} // not named Light because that's a Unity class
	public int MaxEnergy {get; protected set;}
	public int UsedEnergy {get; protected set;}


	//Have both Add and Remove functions since costs are likely to be stored
	//as a positive number so adding a negative cost reads awkwardly.
	public void AddLight( float amt) { LightRes += amt;}
	public void RmLight( float amt) { LightRes -= amt;}
	public void AddMaxEnergy( int amt) { MaxEnergy += amt;}
	public void RmMaxEnergy( int amt) { MaxEnergy -= amt;}
	public void AddUsedEnergy( int amt) { UsedEnergy += amt;}
	public void RmUsedEnergy( int amt) { UsedEnergy -= amt;}


	public void Reset () {
		LightRes = 0;
		MaxEnergy = 0;
		UsedEnergy = 0;
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
