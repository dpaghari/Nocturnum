using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class IsLightGrenade : MonoBehaviour {

	//public AIController enemyStat;
	public GameObject flare;
	public AudioClip lgsound;
	public GameObject vfx;

	void Start(){

	}



	void OnCollisionEnter(Collision other)
	{
		audio.PlayOneShot(lgsound, 1.0f);
		if(other.gameObject.tag == "ground"){
		//	Debug.Log ("hit ground");

			Vector3 temp = transform.position;
			temp.y += 1;
			Instantiate(flare, temp, Quaternion.identity);
			Instantiate(vfx, transform.position,  Quaternion.identity);


		}
		if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyRanged"){
		Killable killScript = other.gameObject.GetComponent<Killable>();
		if(killScript) killScript.damage(30);
		}

		Destroy(gameObject);
		
	}

	void FixedUpdate(){
		//rigidbody.AddForce(Vector3.up * 100, ForceMode.Impulse);
	}

	void Update(){


	}



}
