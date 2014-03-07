using UnityEngine;
using System.Collections;

public enum Corruptable {
	none = 0,
	generator,
	enemyMelee,
	enemyRanged
}


//replace killable with this in generator prefab
public class Corruption : MonoBehaviour {

	public float corruption = 0.0f;
	private float corruptLimit = 80.0f;
	private bool active = true;
	public Corruptable corruptType = Corruptable.none;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void damage(float dmg){
		if(active){
			corruption += dmg;
			if(corruption >= corruptLimit){
				deactivate();
			}

		}
	}

	public void deactivate(){
		active = false;
		if(corruptType == Corruptable.generator) Debug.Log ("DEAD");
	}


}
