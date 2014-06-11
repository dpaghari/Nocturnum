using UnityEngine;
using System.Collections;

public class isLevelOption : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void goTutorial(){
		AutoFade.LoadLevel("tutorial", 2.0f, 2.0f, Color.black);
	}
	public void golevel1(){
		AutoFade.LoadLevel("level1", 2.0f, 2.0f, Color.black);
	}
	public void golevel2(){
		AutoFade.LoadLevel("level2", 2.0f, 2.0f, Color.black);
	}
	public void golevel3(){
		AutoFade.LoadLevel("level3", 2.0f, 2.0f, Color.black);	
	}
	public void golevel4(){
		AutoFade.LoadLevel("level4", 2.0f, 2.0f, Color.black);
	}
	public void golevel5(){
		AutoFade.LoadLevel("BatBossBattle", 2.0f, 2.0f, Color.black);
	}
	public void goEndless(){
		AutoFade.LoadLevel("EndlessMode", 2.0f, 2.0f, Color.white);
	}
}
