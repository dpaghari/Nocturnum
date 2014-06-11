//PURPOSE:  Equipment Behavior function that plays the animation, sound and creates the invisible instance of the fist shockwave of the groundpunch
// Upon Pressing the Right Mouse Button this will Instantiate the fistShockwave gameobject that pushes back enemies and applies damage.

using UnityEngine;
using System.Collections;

public class EB_LightFist : EquipmentBehavior {

	public int fistDamage = 20;
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


	public void UpdateLevelAdjustments(GameObject punchObj, GameObject flareObj, GameObject vfxObj){
		int fistLevel = TechManager.GetUpgradeLv(Tech.lightFist);
		float sizeScale = 1 + 0.1f * fistLevel;
		float pushScale = 1 + 0.5f * fistLevel;
		isFistShockwave fistScript = punchObj.GetComponent<isFistShockwave>();
		fistScript.fistDamage = fistDamage + 5 * fistLevel;
		fistScript.pushForce *= pushScale;
		punchObj.transform.localScale *= sizeScale;
		flareObj.particleSystem.startSize *= sizeScale;
	}

	public void Shockwave(){

		Vector3 temp = transform.position;
		temp.y += 0.5f;
		clone = Instantiate (punchObj, transform.position, Quaternion.identity) as Rigidbody;
		GameObject flareClone = Instantiate(flare, temp, Quaternion.identity) as GameObject;
		GameObject vfxClone = Instantiate(vfx, temp, Quaternion.identity) as GameObject;
		UpdateLevelAdjustments( clone.gameObject, flareClone, vfxClone);
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
