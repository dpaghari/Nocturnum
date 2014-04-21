using UnityEngine;
using System.Collections;

public class isFistShockwave : MonoBehaviour {



	public GameObject flare;
	public float pushForce;
	public ForceMode pushForceMode = ForceMode.Impulse;
	public bool hitGround;
	// Use this for initialization
	void Start () {
		hitGround = false;
	
	}

	void OnCollisionEnter(Collision other)
	{
		//audio.PlayOneShot(lgsound, 1.0f);
		if(other.gameObject.tag == "ground"){
				Debug.Log ("hit ground");

			Vector3 temp = transform.position;
			temp.y += 1;

			Instantiate(flare, transform.position, Quaternion.identity);
			hitGround = true;
			Destroy(gameObject);
		}
	
	}

	void OnTriggerEnter(Collider other){
		
		
		CanMove moveScript = other.GetComponent<CanMove>();
		Killable killScript = other.GetComponent<Killable>();

		if (hitGround) {
			if (killScript != null && moveScript != null) {
					if (other.tag == "Enemy" || other.tag == "EnemyRanged") {
						if(killScript) killScript.damage(20);
			
						Vector3 dir = (other.transform.position - transform.position).normalized;
				
						other.rigidbody.AddForce (dir * pushForce, pushForceMode);
						//moveScript.Move(-dir, pushForceMode);
					}
			}
			hitGround = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
