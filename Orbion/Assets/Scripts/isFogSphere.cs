using UnityEngine;
using System.Collections;

public class isFogSphere : MonoBehaviour {

	private float timer;
	private float timerCD;
	private Rigidbody clone;

	public Rigidbody fog;
	public IsFogEater fogScript;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		timerCD = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer > timerCD){
			timer = 0.0f;
			popSphere();

		}
	
	}
	public void popSphere(){

		int count = fogScript.fogCount;

		for(int i = 0; i <= count; i++){

			clone = Instantiate(fog, transform.position, Quaternion.identity) as Rigidbody;

		}
		Destroy(gameObject);
	}
}
