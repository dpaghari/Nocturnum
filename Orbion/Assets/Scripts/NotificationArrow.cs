//Purpose: Creatures the GUI notifcation and handles its positioning + drawing

using UnityEngine;
using System.Collections;
/*
TODO: 
Make arrows invisible if players is too close since they will draw ontop of things
Maybe make the spawning function now spawn an arrow if its too close to another one?
Make a delayed death and have it turn invisible
*/

public class NotificationArrow : MonoBehaviour {

	//The sprite for the arrow
	public Texture2D arrowTexture;
	
	//Draw size of the sprite
	public Vector2 size = new Vector2(64, 64);

	//distance away from the center of the screen
	//based on a fraction of 1/2 of the smallest dimension of the screen
	public float radiusRatio = 0.3f;

	//Object must be at or greater than this dist to draw the arrow
	public float minDrawDist = 10;

	//The lifetime(sec) of the notifcation
	public float duration = 1f;

	//Used to draw the sprite with unity API
	private Rect drawRect = new Rect(0, 0, 0, 0);

	private DumbTimer durationTimer;



	void Start(){
		durationTimer = DumbTimer.New(duration);
	}


	void Update(){
		if( durationTimer.Finished()) GameObject.Destroy(gameObject);
		

		durationTimer.Update();
	}


	float GetIntialDrawHeight(){
		return Screen.height/2 - size.y/2;
	}


	float GetInitialDrawWidth(){
		float smallestDimension = Mathf.Min( Screen.height, Screen.width);
		return Screen.width/2 + smallestDimension/2*radiusRatio - size.x/2;
		
	}


	//If player is not found, returns 0deg
	//Reversing the z direction because screen space goes from top,down 0-> positive
	float FindRotation(){
		if( GameManager.Player == null) return 0;
		Vector3 direction = this.transform.position - GameManager.Player.transform.position;
		float angleRad = Mathf.Atan2( -direction.z, direction.x);
		return angleRad * Mathf.Rad2Deg;
		
	}


	void OnGUI() {
		if( GameManager.Player == null) return;

		if( Utility.FindDistNoY( this.gameObject, GameManager.Player) > minDrawDist){
			drawRect = new Rect( GetInitialDrawWidth(), GetIntialDrawHeight(), size.x, size.y);
			GUIUtility.RotateAroundPivot( FindRotation(), new Vector2( Screen.width/2, Screen.height/2));
			GUI.DrawTexture( drawRect, arrowTexture);
		}
	}


}
