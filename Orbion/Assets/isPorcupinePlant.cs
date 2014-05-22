using UnityEngine;
using System.Collections;

public class isPorcupinePlant : MonoBehaviour {


	public CanShoot shootScript;
	private DumbTimer timerScript;
	// Use this for initialization
	void Start () {
		timerScript = DumbTimer.New(2.0f, 1.0f);

	}
	
	// Update is called once per frame
	void Update () {
		timerScript.Update();
	}

	void OnCollisionEnter(Collision other){

		if(timerScript.Finished() == true){
			if(other.gameObject.tag == "playerBullet" || other.gameObject.tag == "enemyBullet"){
				shootScript.Shoot(transform.position + Vector3.right);
				timerScript.Reset();
			}

		}
		if(other.gameObject.tag == "Player" || other.gameObject.GetComponent<IsEnemy>()){
			other.gameObject.GetComponent<Killable>().damage(20);
		}

	}
}
