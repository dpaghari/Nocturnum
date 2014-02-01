using UnityEngine;
using System.Collections;

public class AvatarController : MonoBehaviour {

	public CanMove moveScript;
	public CanShootReload shootScript;


	private float ScatterSpread = 45f; //max angle that the scatter shot spreads to


	//Input.mousePosition gives you a screen position, not world position of the map.
	//To get the world position, we create a vertical ray from the screen position,
	//raycast it and if it hits something (like our floor), we take the position of
	//the point we hit. Note that we could easily raycast onto an object above the
	//floor and as such the y value (height) of this function is volatile and generally
	//unreliable/irrelevant. The function allows you to set the y value of the retuned
	// vector3 directly.
	protected Vector3 GetMouseWorldPos(float yvalue = -Mathf.Infinity){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);
		Vector3 mousePos = hit.point;
		if ( yvalue > -Mathf.Infinity) mousePos.y = yvalue;
		return mousePos;
	}


	//Shoots a scatter shot of bullets around the center direction: dir
	//   going from dir - ScatterSpread/2 to dir + ScatterSpread/2.
	//Works even if we're just shooting 1 bullet.
	protected void Scattershot(Vector3 target){
		Vector3 dir = target - transform.position;
		if( shootScript.FinishCooldown()){
			//Vector3 hitAngle = adjustedHit - transform.position;
			Vector3 leftBound = Quaternion.Euler( 0, -ScatterSpread/2, 0) * dir;
			int ScatterCount = TechManager.GetUpgradeLv( Tech.scatter) + 1;
			for ( int i = 1; i <= ScatterCount; i++){
				float angleOffset = i * ( ScatterSpread / ( ScatterCount + 1));
				Vector3 BulDir = Quaternion.Euler( 0, angleOffset, 0) * leftBound ;
				shootScript.SetFiringTimer( 1.0f);
				shootScript.ShootDir( BulDir);
			}

		}
		
	}



	// Use this for initialization
	void Start () {
	
	}



	void FixedUpdate() {
		if( Input.GetKey( KeyCode.W))
			moveScript.Move( Vector3.forward);
		
		if( Input.GetKey( KeyCode.S))
			moveScript.Move( Vector3.back);
		
		if( Input.GetKey( KeyCode.D))
			moveScript.Move( Vector3.right);
		
		if( Input.GetKey( KeyCode.A))
			moveScript.Move( Vector3.left);

		if( Input.GetKeyDown( KeyCode.R))
			shootScript.Reload();

	}
	


	// Update is called once per frame
	void Update () {

		//Uses the CanShootReload component to shoot at the cursor
		if( Input.GetMouseButton( 0)){
			Scattershot( GetMouseWorldPos( transform.position.y));
		}

	}
}
