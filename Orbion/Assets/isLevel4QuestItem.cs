using UnityEngine;
using System.Collections;

public class isLevel4QuestItem : MonoBehaviour {


	public float numCharges;
	public float maxCharges;
	private DumbTimer timerScript;
	// Use this for initialization
	void Start () {

		numCharges = 0;
		maxCharges = 500.0f;
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(numCharges);
		numCharges += Time.deltaTime;
	
	}
}
