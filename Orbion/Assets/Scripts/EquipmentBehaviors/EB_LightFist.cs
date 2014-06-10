//PURPOSE:  Equipment Behavior function that plays the animation, sound and creates the invisible instance of the fist shockwave of the groundpunch
// Upon Pressing the Right Mouse Button this will Instantiate the fistShockwave gameobject that pushes back enemies and applies damage.

using UnityEngine;
using System.Collections;

public class EB_LightFist : EquipmentBehavior {


	public Rigidbody punchObj;
	private Rigidbody clone;
	public GameObject flare;
	public GameObject vfx;
	public AudioClip fistSound;

	private bool isPlayingAudio = false;

	public override void Action ( Vector3 cursor){
		animation.CrossFade("Groundpunch"); 
		audio.PlayOneShot(fistSound, 0.2f);
		StartCoroutine(DelayedPunch());
	}


	public void Shockwave(){

		Vector3 temp = transform.position;
		temp.y += 0.5f;
		clone = Instantiate (punchObj, transform.position, Quaternion.identity) as Rigidbody;
		Instantiate(flare, temp, Quaternion.identity);
		Instantiate(vfx, temp, Quaternion.identity);
		isPlayingAudio = false;
	}

	public override void FixedUpdateEB (){}
	public override void UpdateEB (){}
	public override void OnSwitchEnter (){
	}
	public override void OnSwitchExit (){return;}

	IEnumerator DelayedPunch(){
		for(;;){
			if( animation.IsPlaying("Groundpunch") == false ) break;

			if( animation["Groundpunch"].normalizedTime > 0.55 ){
				Shockwave();
				break;
			}

			yield return new WaitForFixedUpdate();
			
		}
	}


	
	
	
}
