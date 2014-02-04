using UnityEngine;
using System.Collections;

public class MetricManager : Singleton<MetricManager> {
	
	protected MetricManager() {} // guarantee this will be always a singleton only - can't use the constructor!
	
	private static int shotsFired = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void incrementShots(){
		shotsFired++;
	}

	public static int getShots(){
		return shotsFired;
	}

}
