using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {
	public ProjectileBehavior CurrBehavior; 
	//public PB_Linear CurrBehavior;

	void Start () {
	}
	
	void FixedUpdate() {
		CurrBehavior.FixedPerform();
	}

	void Update () {
		CurrBehavior.Perform();
	}

	void OnCollisionEnter(Collision other){
		CurrBehavior.OnImpactEnter(other);
	}

	void OnCollisionStay(Collision other){
		CurrBehavior.OnImpactStay(other);
	}

	void OnCollisionExit(Collision other){
		CurrBehavior.OnImpactExit(other);
	}

}