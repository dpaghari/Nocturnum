using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
	
	public GameObject cameraTarget; // object to look at or follow
	//public GameObject Player; // player object for moving
	
	public float smoothTime = 0.1f;    // time for dampen
	public bool cameraFollowX = true; // camera follows on horizontal
	public bool cameraFollowZ = true; // camera follows on vertical
	public bool cameraFollowHeight = true; // camera follow CameraTarget object height
	public float cameraHeight = 2.5f; // height of camera adjustable
	public Vector3 velocity; // speed of camera movement
	
	private Transform thisTransform; // camera Transform
	
	// Use this for initialization
	void Start()
	{
		//cameraTarget = GameObject.Find("player_prefab");
		thisTransform = transform;
	}
	
	// Update is called once per frame
	void Update()
	{

		if (cameraFollowX)
		{
			thisTransform.position = new Vector3(Mathf.SmoothDamp(thisTransform.position.x, cameraTarget.transform.position.x, ref velocity.x, smoothTime), thisTransform.position.y, thisTransform.position.z);
		}
		if (cameraFollowZ)
		{
			thisTransform.position = new Vector3(thisTransform.position.x, thisTransform.position.y, Mathf.SmoothDamp(thisTransform.position.z, cameraTarget.transform.position.z - 14, ref velocity.z, smoothTime));
		}
		if (!cameraFollowX & cameraFollowHeight)
		{
			// to do
		}

		if (cameraTarget) {
			Utility.LerpLook( this.gameObject, cameraTarget, 1, false);
		}
		
		//gameObject.transform.position += velocity;

	}
}