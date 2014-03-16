using UnityEngine;
using System.Collections;

public class IsFogEater : MonoBehaviour {

	public bool isActive;
	public int fogCount;
	private float timer;
	private float timerCD;
	private int sphereCount;

	public Rigidbody fogSphere;
	private Rigidbody clone;



	// Use this for initialization
	void Start () {
		isActive = false;
		fogCount = 0;
		timer = 0.0f;
		timerCD = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log(fogCount);
		if(fogCount > 0){
			timer += Time.deltaTime;
			isActive = true;
			if(timer >= timerCD && sphereCount < 1){
				createSphere();
				sphereCount++;
				fogCount = 0;
				timer = 0.0f;
			}
		}
	
	}

	void createSphere(){
		animation.CrossFade("open");

		Vector3 temp = transform.position;
		temp.y = 3;
		clone = Instantiate(fogSphere, temp, Quaternion.identity) as Rigidbody;

	}
}
