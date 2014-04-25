using UnityEngine;
using System.Collections;

public class isPoisonCloud : MonoBehaviour {

	public int cloudDmg = 20;
	private float cloudLifetime = 10.0f;
	public DumbTimer timerScript;
	public bool alive = false;
	// Use this for initialization
	void Start () {
		timerScript = DumbTimer.New (cloudLifetime, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {

		if(alive == true){
		//Debug.Log("YO");
		timerScript.Update();
		}
		if (timerScript.Finished() == true) {
		//	Debug.Log("destroying");
			Destroy(gameObject);

		}
	}

	void OnCollisionEnter(Collision other){
		//Debug.Log ("colliding with things");
		if(other.gameObject.GetComponent<Killable>() != null){
			//Debug.Log ("colliding with killablestuff");
			other.gameObject.GetComponent<Killable>().damage(cloudDmg);
			Destroy (gameObject);
		}

	}
	public void beginLifetime(){
		alive = true;
	}

}
