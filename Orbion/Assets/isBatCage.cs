using UnityEngine;
using System.Collections;

public class isBatCage : MonoBehaviour {
	private int degrees = 10;
	public GameObject target;
	private Vector3 direction;
	private Vector3 position;
	
	float currHeight;
	float amplitude;
	float speed;
	// Use this for initialization
	void Start () {
		target = GameManager.AvatarContr.gameObject;
		amplitude = 1.0f;
		speed = 2.0f;
		currHeight = transform.position.y + 1.3f;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void LateUpdate(){

		Vector3 pos = target.transform.position;
		pos.y = currHeight+amplitude*Mathf.Sin(speed*Time.time);
		pos.z -= 4;
		pos.x += 4;
		
		transform.position = pos;
		
		//direction = Utility.GetMouseWorldPos(transform.position.y);
		//transform.forward = direction - transform.position;
		
		
	}

	void OnTriggerEnter(Collider other){

		if(other.GetComponent<isLunaShip>() != null){
			TechManager.transportedBat = true;
			Destroy(gameObject);
			//other.collider.enabled = false;
			
		}
	}
	
	
	
}

