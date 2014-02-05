using UnityEngine;
using System.Collections;

public class MetricManager : Singleton<MetricManager> {
	
	protected MetricManager() {} // guarantee this will be always a singleton only - can't use the constructor!
	
	private int _shotsFired = 0;

	public static void AddShots(int amt){ Instance._shotsFired += amt;}
	public static int ShotsFired{ get { return Instance._shotsFired;}}

	/*
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	*/
}
