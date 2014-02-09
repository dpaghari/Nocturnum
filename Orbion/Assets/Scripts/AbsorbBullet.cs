using UnityEngine;
using System.Collections;

public class AbsorbBullet : MonoBehaviour {

	public CanShoot shootScript;
	public Rigidbody shield;
	public Rigidbody clone;
	public GameObject player;
	public Vector3 dir;

	private bool isOut;
	private float timer = 0.0f;
	private float cooldown = 2.0f;

	// Use this for initialization
	void Start () {
		isOut = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(isOut){
			clone.transform.position = transform.position + dir * 2;
			timer += Time.deltaTime;
		}
			if(timer > cooldown){
			Destroy (clone.gameObject);
			isOut = false;
			timer = 0.0f;
		}
	
	}
	public void AbsorbShot(Vector3 target){
		dir = target - transform.position;
		dir.Normalize();


		
		if( shootScript.FinishCooldown() && !isOut){
			
			clone = Instantiate(shield, transform.position + dir * 2, Quaternion.LookRotation(dir, Vector3.up)) as Rigidbody;
			//clone.transform.position = transform.position + dir * 2;
			shootScript.ResetFiringTimer();
			isOut = true;

			
			
		}
		
	}


	
}
