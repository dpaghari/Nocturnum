using UnityEngine;
using System.Collections;

public class OrbionMainMenu : MonoBehaviour {


	private int degrees = 10;
	public Transform target;
	private Vector3 direction;
	private Vector3 position;
	
	float currHeight;
	float amplitude;
	float speed;
	// Use this for initialization
	void Start () {
		amplitude = 1.0f;
		speed = 2.0f;
		currHeight = transform.position.y + 1.3f;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(ResManager.LGEnergy >= ResManager.LGMaxEnergy)
			light.enabled = true;
		else
			light.enabled = false;
		*/
	}
	
	void LateUpdate(){
		//position = target.transform.position;
		//position.y += 2;
		//position.z -= 2;
		//position.x -= 2;
		Vector3 pos = target.transform.position;
		pos.y = currHeight+amplitude*Mathf.Sin(speed*Time.time);
		pos.z += 2;
		pos.x += 2;
		
		transform.position = pos;
		
		direction = Utility.GetMouseWorldPos(transform.position.y);
		transform.forward = direction - transform.position;
		
		
	}
	
	
	
}

