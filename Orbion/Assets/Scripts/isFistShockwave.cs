using UnityEngine;
using System.Collections;

public class isFistShockwave : MonoBehaviour {




	public float pushForce;
	public ForceMode pushForceMode = ForceMode.Impulse;
	public int fistDamage;
	//public DumbTimer timerScript;
	// Use this for initialization
	void Start () {
		//timerScript = DumbTimer.New(0.2f, 1.0f);

		fistDamage = 20;
	
	}

	void OnCollisionEnter(Collision other)
	{
		//audio.PlayOneShot(lgsound, 1.0f);
		if(other.gameObject.tag == "ground"){
			//Debug.Log ("hit ground");




			//Destroy(gameObject);
		}
	
	}

	void OnTriggerEnter(Collider other){
		//Debug.Log(other);
		
		
		CanMove moveScript = other.GetComponent<CanMove>();
		Killable killScript = other.GetComponent<Killable>();


			if (killScript != null && moveScript != null) {
					if (other.tag == "Enemy" || other.tag == "EnemyRanged") {
								
							if(killScript) killScript.damage(fistDamage);

							Vector3 dir = (other.transform.position - transform.position).normalized;
							//Debug.Log("Should push things");
							
							other.rigidbody.AddForce (dir * pushForce, pushForceMode);
					
			
						
					}
			}

	}
	
	// Update is called once per frame
	void Update () {
	



	
	}
}
