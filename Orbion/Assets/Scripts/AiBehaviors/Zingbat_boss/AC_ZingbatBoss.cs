using UnityEngine;
using System.Collections;

public class AC_ZingbatBoss : AiController {

	public CanMove moveScript;

	protected void Start() {
		moveScript = GetComponent<CanMove>();
		CurrBehavior.OnBehaviorEnter();
	}

	protected void FixedUpdate () {
		CurrBehavior.FixedUpdateAB();
	}
	
	
	protected void Update () {
		CurrBehavior.UpdateAB();
	}

}
