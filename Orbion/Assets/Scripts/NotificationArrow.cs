using UnityEngine;
using System.Collections;
/*
TODO: 
Make arrows invisible if players is too close since they will draw ontop of things
Maybe make the spawning function now spawn an arrow if its too close to another one?
Make a delayed death and have it turn invisible
*/

public class NotificationArrow : MonoBehaviour {

	public Texture2D arrow;
	public Vector2 size = new Vector2(64, 64);
	public Vector2 pos = new Vector2(0, 0);
	public float rotationOffset = 0; //if the base image is not pointing to the right
	public float duration = 1f;

	private Vector2 posNoClamp = Vector2.zero;
	private Rect drawRect = new Rect(0, 0, 0, 0);
	private Vector2 posOffset = Vector2.zero;
	private DumbTimer durationTimer;


	//Prevents the arrow from being drawn outside of the screen
	void ClampPosToScreen() {
		pos.x = Mathf.Clamp( posNoClamp.x, posOffset.x, Camera.main.pixelWidth - posOffset.x);
		pos.y = Mathf.Clamp( posNoClamp.y, posOffset.y, Camera.main.pixelHeight - posOffset.y);
	}

	float FindRotation() {
		Vector2 arrowDir;

		if( posNoClamp == pos){
			Vector2 playerPos =  Camera.main.WorldToScreenPoint( GameManager.Player.transform.position);
			playerPos.y =  Camera.main.pixelHeight - playerPos.y;
			arrowDir = posNoClamp - playerPos;
		}
		else 
			arrowDir = posNoClamp - pos;

		float angleDegree = Mathf.Atan2(arrowDir.y, arrowDir.x) * Mathf.Rad2Deg;
		return angleDegree - rotationOffset;
	}

	//updates the transformation of the arrow
	void UpdateArrowTransform() {
		posOffset.x = size.x / 2;
		posOffset.y = size.y / 2;

		//WorldToScreenPoint's gives a y is in the opposite direction that the draw expects
		posNoClamp =  Camera.main.WorldToScreenPoint( transform.position);
		posNoClamp.y = Camera.main.pixelHeight - posNoClamp.y; 
		ClampPosToScreen();

		drawRect = new Rect( pos.x - posOffset.x, pos.y - posOffset.y, size.x, size.y);
		GUIUtility.RotateAroundPivot( FindRotation(), pos);
	}


	// Use this for initialization
	void Start () {
		durationTimer = DumbTimer.New(duration);
	}
	
	// Update is called once per frame
	void Update () {
		if( durationTimer.Finished()) GameObject.Destroy(gameObject);

		durationTimer.Update();
	}

	void OnGUI() {
		UpdateArrowTransform();
		GUI.DrawTexture( drawRect, arrow);
	}

}
