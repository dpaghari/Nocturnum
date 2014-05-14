using UnityEngine;
using System.Collections;

public class isPorcupinePlant : MonoBehaviour {


	public CanShoot shootScript;
	private Vector3[] pos;
	private int numSpines;
	private DumbTimer timerScript;
	// Use this for initialization
	void Start () {

		numSpines = 8;
		pos = new Vector3[numSpines];
		timerScript = DumbTimer.New(3.0f, 1.0f);

	}
	
	// Update is called once per frame
	void Update () {
		timerScript.Update();
	}

	void OnCollisionEnter(Collision other){

		if(timerScript.Finished() == true){
			if(other.gameObject.tag == "playerBullet" || other.gameObject.tag == "enemyBullet"){
				for(int i = 0;i < numSpines;i++){
					pos[i] = transform.position;
				}
				pos[0].z += 30;
				pos[1].z -= 30;
				pos[2].x += 30;
				pos[3].x -= 30;

				pos[4].x -= 30;
				pos[4].z -= 30;

				pos[5].x += 30;
				pos[5].z += 30;

				pos[6].x += 30;
				pos[6].z -= 30;

				pos[7].x -= 30;
				pos[7].z += 30;

				for(int i = 0; i < numSpines;i++){
				shootScript.Shoot(pos[i]);
				}
				//shootScript.Shoot(pos2);
				//shootScript.Shoot(pos3);
				//shootScript.Shoot(pos4);

				timerScript.Reset();
			}

		}
		if(other.gameObject.tag == "Player" || other.gameObject.GetComponent<IsEnemy>()){
			other.gameObject.GetComponent<Killable>().damage(20);
		}

	}
}
