using UnityEngine;
using System.Collections;

public class IsFogEater : MonoBehaviour {

	public bool isActive;
	public int fogCount;
	private float timer;
	private float timerCD;
	public int sphereCount;

	public Rigidbody fogSphere;
	private Rigidbody clone;



	// Use this for initialization
	void Start () {
		isActive = false;
		fogCount = 0;
		timer = 0.0f;
		timerCD = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log(sphereCount);
		if(fogCount > 0){
			timer += Time.deltaTime;

			if(timer >= timerCD && sphereCount < 1){
				createSphere();

				sphereCount++;
				fogCount = 0;
				timer = 0.0f;
				isActive = true;
			}
		}

		if(isActive){
			timer += Time.deltaTime;
			if(timer >= timerCD){
				animation.CrossFade("close");
				timer = 0.0f;
				isActive = false;
				sphereCount--;
			
			}
		}

	
	}

	void createSphere(){
		animation.CrossFade("open");

		Vector3 temp = transform.position;
		temp.y = 3;
		clone = Instantiate(fogSphere, temp, Quaternion.identity) as Rigidbody;
		clone.GetComponent<isFogSphere>().fogCounter = fogCount;




	}
}
