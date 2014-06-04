using UnityEngine;
using System.Collections;

public class canTeleport : MonoBehaviour {

	public float teleportCharge;
	public float teleportThresh;
	private bool isTeleporting;
	private CanMove moveScript;
	public GameObject spotlightPos;


	// Use this for initialization
	void Start () {
		spotlightPos = GameObject.Find("Spotlight");
		moveScript = GetComponent<CanMove>();
		isTeleporting = false;
		teleportCharge = 0.0f;
		teleportThresh = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.H)){
			isTeleporting = true;
		}


		if(Input.GetKey(KeyCode.H)){
			Debug.Log(teleportCharge);
			if(TechManager.IsTechAvaliable(Tech.spotlight)){
				teleportCharge += Time.deltaTime;
			}
		}
		if(Input.GetKeyUp(KeyCode.H)){
			isTeleporting = false;
			teleportCharge = 0;

		}

		if(teleportCharge >= teleportThresh){
			if(TechManager.IsTechAvaliable(Tech.spotlight)){
				transform.position = spotlightPos.transform.position;

			}

		}


	
	}
}
