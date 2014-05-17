using UnityEngine;
using System.Collections;

public class EB_LightFist : EquipmentBehavior {


	public Rigidbody punchObj;
	private Rigidbody clone;
	public GameObject flare;
	public GameObject vfx;
	public AudioClip fistSound;


	public override void Action ( Vector3 cursor){
		GameManager.KeysEnabled = false;
		//Vector3 temp = transform.position;
		//temp.y += 4;
		audio.PlayOneShot(fistSound, 0.5f);
		animation.Play("Groundpunch"); 
		StartCoroutine(WaitAndCallback(animation["Groundpunch"].length));
		//animation.CrossFade("Groundpunch");	




	}
	public override void FixedUpdateEB (){
	
	}
	
	
	public override void UpdateEB ()
	{
	
		
	}

	public override void OnSwitchEnter ()
	{

	}
	
	public override void OnSwitchExit (){return;}
	IEnumerator WaitAndCallback(float waitTime){
		yield return new WaitForSeconds(waitTime - 0.6f); 
		Vector3 temp = transform.position;
		temp.y += 2;
		clone = Instantiate (punchObj, transform.position, Quaternion.identity) as Rigidbody;
		Instantiate(flare, temp, Quaternion.identity);
		Instantiate(vfx, temp, Quaternion.identity);
		GameManager.KeysEnabled = true;
		//Debug.Log("Thishappened");
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
