using UnityEngine;
using System.Collections;
/*
TODO: 
Make arrows invisible if players is too close since they will draw ontop of things
Maybe make the spawning function now spawn an arrow if its too close to another one?
Make a delayed death and have it turn invisible
*/

public class NotificationArrow : MonoBehaviour {

	public Texture2D arrowTexture;
	public Vector2 size = new Vector2(64, 64);
	public Vector2 pos = new Vector2(0, 0);
	public float rotationOffset = 0; //if the base image is not pointing to the right
	public float duration = 1f;
	public float visibleMarginRatio = 1; //should at least be 1

	//World Range that prevents this object from instantiating if it is too close to another arrow
	//public float duplicateRange;


	private Vector2 posNoClamp = Vector2.zero;
	private Rect drawRect = new Rect(0, 0, 0, 0);
	private Vector2 posOffset = Vector2.zero;
	private DumbTimer durationTimer;




	//Uses values that UpdateArrowTransform updates, should run after it
	public bool ShouldDraw(){
		//If it clamped, then it was forced to the edge and we should draw it
		if( posNoClamp != pos) return true;

		float undrawableWidth = Camera.main.pixelWidth - 2 * visibleMarginRatio * size.x;
		float undrawableHeight = Camera.main.pixelHeight - 2 * visibleMarginRatio * size.y;
		Rect undrawableMargin = new Rect( visibleMarginRatio * size.x,  visibleMarginRatio * size.y , undrawableWidth, undrawableHeight);

		return !undrawableMargin.Overlaps( drawRect);
	}
/*
	//Prevents the arrow from being drawn outside of the screen
	void ClampPosToScreen() {
		pos.x = Mathf.Clamp( posNoClamp.x, posOffset.x, Camera.main.pixelWidth - posOffset.x);
		pos.y = Mathf.Clamp( posNoClamp.y, posOffset.y, Camera.main.pixelHeight - posOffset.y);

	}
*/
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


	Vector2 ClampToScreen(){
		//WorldToScreenPoint's gives a y is in the opposite direction that the draw expects
		Vector2 screenDrawPos =  Camera.main.WorldToScreenPoint( transform.position);
		screenDrawPos.y = Camera.main.pixelHeight - screenDrawPos.y;


		//WorldToScreenPoint and WorldToViewportPoint don't give reliable values when out of bound for our camera
		//So we only use them to check if we're out of bounds and then manually check the direction we're out of bound
		//using the object and camera's 3d position
		if( screenDrawPos.x < posOffset.x || screenDrawPos.x > Camera.main.pixelWidth - posOffset.x){
			if( transform.position.x < Camera.main.transform.position.x)
				screenDrawPos.x = posOffset.x;
			else
				screenDrawPos.x = Camera.main.pixelWidth - posOffset.x;
		}
			
		if( screenDrawPos.y < posOffset.y || screenDrawPos.y > Camera.main.pixelHeight - posOffset.y){
			if( transform.position.z < Camera.main.transform.position.z)
				screenDrawPos.y = Camera.main.pixelHeight - posOffset.y;
			else
				screenDrawPos.y = posOffset.y;
		}
		

		return screenDrawPos;
	}


	//updates the transformation of the arrow
	void UpdateArrowTransform() {
		posOffset.x = size.x / 2;
		posOffset.y = size.y / 2;

		//WorldToScreenPoint's gives a y is in the opposite direction that the draw expects
		posNoClamp =  Camera.main.WorldToScreenPoint( transform.position);
		posNoClamp.y = Camera.main.pixelHeight - posNoClamp.y;
		pos = ClampToScreen();

		drawRect = new Rect( pos.x - posOffset.x, pos.y - posOffset.y, size.x, size.y);

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
		if ( ShouldDraw()) {
			GUIUtility.RotateAroundPivot( FindRotation(), pos);
			GUI.DrawTexture( drawRect, arrowTexture);
		}
	}

}
