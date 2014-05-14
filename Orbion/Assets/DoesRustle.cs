using UnityEngine;
using System.Collections;

public class DoesRustle : MonoBehaviour {

	private bool goLeft = false;
	private bool goLeft2 = false;
	private bool goRight = false;
	private bool noRustle = true;
	private int rustleTimer = 0;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!noRustle) {
			if(goLeft){
				transform.Rotate(-1*Vector3.up);
				rustleTimer--;
				if(rustleTimer <= 0){
					goLeft = false;
					goRight = true;
					rustleTimer = 20;
				}
			}
			if(goRight){
				transform.Rotate(Vector3.up);
				rustleTimer--;
				if(rustleTimer <= 0){
					goLeft2 = true;
					goRight = false;
					rustleTimer = 10;
				}
			}
			if(goLeft2){
				transform.Rotate(-1*Vector3.up);
				rustleTimer--;
				if(rustleTimer <= 0){
					goLeft2 = false;
					noRustle = true;
				}
			}

		}
	}

	public void OnTriggerEnter(Collider other){
		noRustle = false;
		goLeft = true;
		rustleTimer = 10;
	}
}
