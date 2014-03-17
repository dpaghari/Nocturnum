using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]

public class CanCorrupt : MonoBehaviour {

	public float cooldown;
	public float corruptDamage;
	private HashSet<Corruptable> targets;
	private DumbTimer corruptTimer;

	void Awake (){
		targets = new HashSet<Corruptable>();
		corruptTimer = DumbTimer.New( cooldown);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(	corruptTimer.Finished()){
			foreach( Corruptable corruptScript in targets)
				if (corruptScript != null) corruptScript.Corrupt(corruptDamage);
			corruptTimer.CurrTime = corruptTimer.MaxTime;
		}
			

		//Debug.Log(corruptTimer.CurrTime);
		corruptTimer.Update();

	}

	void OnTriggerEnter(Collider other){
		Corruptable corruptScript = other.GetComponent<Corruptable>();
		if( corruptScript != null) targets.Add(corruptScript);
	}

	void OnTriggerExit(Collider other){
		Corruptable corruptScript = other.GetComponent<Corruptable>();
		if( corruptScript != null) targets.Remove(corruptScript);
	}

	

	

}
