using UnityEngine;
using System.Collections;

public class ResManager : Singleton<ResManager> {

	protected ResManager() {} // guarantee this will be always a singleton only - can't use the constructor!

	private float _Lumen = 0;
	private int _MaxEnergy = 0;
	private int _UsedEnergy = 0;


	public static float Lumen{ get { return Instance._Lumen;}}
	public static int MaxEnergy{ get { return Instance._MaxEnergy;}}
	public static int UsedEnergy{ get { return Instance._UsedEnergy;}}

	//Have both Add and Remove functions since costs are likely to be stored
	//as a positive number so adding a negative cost reads awkwardly.
	public static void AddLumen( float amt) { Instance._Lumen += amt;}
	public static void AddMaxEnergy( int amt) { Instance._MaxEnergy += amt;}
	public static void AddUsedEnergy( int amt) { Instance._UsedEnergy += amt;}

	public static void RmLumen( float amt) {
		Instance._Lumen -= amt;
		if (Instance._Lumen < 0) Instance._Lumen = 0;
	}

	public static void RmMaxEnergy( int amt) {
		Instance._MaxEnergy -= amt;
		if (Instance._MaxEnergy < 0) Instance._MaxEnergy = 0;
	}
	public static void RmUsedEnergy( int amt) {
		Instance._UsedEnergy -= amt;
		if (Instance._UsedEnergy < 0) Instance._UsedEnergy = 0;

	}


	public static void Reset () {
		Instance._Lumen = 0;
		Instance._MaxEnergy = 0;
		Instance._UsedEnergy = 0;
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
