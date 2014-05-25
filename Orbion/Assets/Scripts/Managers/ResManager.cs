using UnityEngine;
using System.Collections;

public class ResManager : Singleton<ResManager> {

	protected ResManager() {} // guarantee this will be always a singleton only - can't use the constructor!

	private float _Lumen = 0;
	private int _Energy = 0;
	//private int _MaxEnergy = 0;
	//private int _UsedEnergy = 0;
	private int _Collectible = 0;
	private int _MaxCollectible = 25;
	private int _LGEnergy = 0;
	private int _LGMaxEnergy = 300;
	private int _LGCoreCharges = 0;
	private int _LGCoreMaxCharges = 10;




	public static float Lumen{ get { return Instance._Lumen;}}
	public static int Collectible{ get { return Instance._Collectible;}}
	public static int MaxCollectible{ get { return Instance._MaxCollectible;}}
	public static int Energy { get {return Instance._Energy;}}
	public static int LGEnergy { get {return Instance._LGEnergy;}}
	public static int LGMaxEnergy { get {return Instance._LGMaxEnergy;}}
	public static int LGCoreCharges { get {return Instance._LGCoreCharges;}}
	public static int LGCoreMaxCharges { get {return Instance._LGCoreMaxCharges;}}
	//public static int MaxEnergy{ get { return Instance._MaxEnergy;}}
	//public static int UsedEnergy{ get { return Instance._UsedEnergy;}}

	//Have both Add and Remove functions since costs are likely to be stored
	//as a positive number so adding a negative cost reads awkwardly.
	public static void AddLumen( float amt) { Instance._Lumen += amt;}
	public static void AddCollectible( int amt) { 
		Instance._Collectible += amt;
		if(Instance._Collectible >= Instance._MaxCollectible){
			TechManager.hasWolves = true;
		}
	
	}

	public static void AddLGEnergy(int amt){
		if(ResManager.applicationIsQuitting) return;
		if(Instance._LGEnergy < Instance._LGMaxEnergy){
		Instance._LGEnergy += amt;
		}

	}

	
	public static void AddLGCoreCharge(int amt){
		if(ResManager.applicationIsQuitting) return;
		if(Instance._LGCoreCharges < Instance._LGCoreMaxCharges){
			Instance._LGCoreCharges += amt;
		}

		
	}

	public static void AddEnergy(int amt) {
		if(ResManager.applicationIsQuitting) return;

		Instance._Energy += amt;
	
	}
	//public static void AddMaxEnergy( int amt) { Instance._MaxEnergy += amt;}
	//public static void AddUsedEnergy( int amt) { Instance._UsedEnergy += amt;}


	public static void CheckObj(){
	

	}

	public static void RmLumen( float amt) {
		Instance._Lumen -= amt;
		if (Instance._Lumen < 0) Instance._Lumen = 0;
	}
	public static void RmEnergy( int amt) {
		if(ResManager.applicationIsQuitting) return;
		Instance._Energy -= amt;
		if (Instance._Energy < 0) Instance._Energy = 0;
		
	}

	public static void ResetLGContainer(){
		if(ResManager.applicationIsQuitting) return;
		Instance._LGEnergy = 0;
		//Debug.Log(Instance._LGCoreCharges);
	}




	/*
	public static void RmMaxEnergy( int amt) {
		if(ResManager.applicationIsQuitting) return;
		Instance._MaxEnergy -= amt;
		if (Instance._MaxEnergy < 0) Instance._MaxEnergy = 0;
	}
	public static void RmUsedEnergy( int amt) {
		if(ResManager.applicationIsQuitting) return;
		Instance._UsedEnergy -= amt;
		if (Instance._UsedEnergy < 0) Instance._UsedEnergy = 0;

	}
	*/



	public static void Reset () {
		Instance._Collectible = 0;
		Instance._Lumen = 0;
		Instance._Energy = 0;

		Instance._LGEnergy = 0;
		//Instance._LGMaxEnergy = 0;
		Instance._LGCoreCharges = 0;
		//Instance._LGCoreMaxCharges = 0;
		//Instance._MaxEnergy = 0;
		//Instance._UsedEnergy = 0;
	}


}
