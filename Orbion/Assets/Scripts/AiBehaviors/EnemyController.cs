using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public AiBehavior CurrBehavior;
	public Rigidbody CurrTarget;


	void Start () {
		CurrBehavior.OnBehaviorEnter();
	}
	

	void FixedUpdate () {
		CurrBehavior.FixedUpdateAB();
	}



	void Update () {
		CurrBehavior.UpdateAB();
	}

	
	


	public void SwitchBehavior( AiBehavior newBehavior){
		CurrBehavior.OnBehaviorExit();
		CurrBehavior = newBehavior;
		CurrBehavior.OnBehaviorEnter();
	}

}
