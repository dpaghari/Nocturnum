using UnityEngine;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour {

	

	private AvatarController ac;


	void DetectMovement(){
		Vector3 moveDirection = Vector3.zero;

		if( Input.GetKey( KeyCode.W) )
			moveDirection += Vector3.forward;

		if( Input.GetKey( KeyCode.S) )
			moveDirection += Vector3.back;

		if( Input.GetKey( KeyCode.D) )
			moveDirection += Vector3.right;

		if( Input.GetKey( KeyCode.A) )
			moveDirection += Vector3.left;

		if( Input.GetKey( KeyCode.LeftShift) && ac.shootScript.reloading == false)
			ac.Dash( moveDirection);
		else
			ac.Run( moveDirection.normalized );
		
	}


	void ResetLevel(){
		ResManager.Reset();
		TechManager.Reset();
		MetricManager.Reset();
		AutoFade.LoadLevel(Application.loadedLevel, 1.0f, 1.0f, Color.black);
	}


	// Use this for initialization
	void Start () {
		ac = GameManager.AvatarContr;
	}
	

	void FixedUpdate(){
		if( animation.IsPlaying("Dash")) return;
		DetectMovement();
	}

	// Update is called once per frame
	void Update () {


		if( Input.GetKeyDown( KeyCode.F9)) ResetLevel();


		//Quit Game
		if( Input.GetKeyDown( KeyCode.F11)) Application.Quit();



		if( animation.IsPlaying("Dash")) return;
		if( Input.GetMouseButton( 0)) ac.Shoot( Utility.GetMouseWorldPos( transform.position.y));
		if( Input.GetMouseButtonDown(1)) ac.ActivateEquip( Utility.GetMouseWorldPos( transform.position.y));
		if( Input.GetKeyDown( KeyCode.R)) ac.Reload();
		if( Input.GetKeyDown( KeyCode.E)) ac.ActivateEquip( Utility.GetMouseWorldPos( transform.position.y));

	}
}
