using UnityEngine;
using System.Collections;

public class CanResearch : MonoBehaviour {

	private bool menuUp = false;

	// Use this for initialization
	void Start () {
	
	}
	

	void OnGUI() {
		if(menuUp){
			GetComponent<CanShoot>().ResetFiringTimer();
			GUI.Box(new Rect (10,10,100,90), "Research Menu");

			if(GUI.Button(new Rect(20,40,80,20), "Scattershot")) {
				if(TechManager.IsTechAvaliable(Tech.scatter)){
					TechManager.Research(Tech.scatter);
					menuUp = false;
				}
			}

			if(GUI.Button(new Rect(20,60,80,20), "Orbshot")) {
				if(TechManager.IsTechAvaliable(Tech.orbshot)){
					TechManager.Research(Tech.orbshot);

					//Just slapping it here for now until we have a way to to manage bullets for the player.
					//Should make an event manager to broadcast that a upgrade was researched later
					GameManager.AvatarContr.shootScript.bullet = GameManager.AvatarContr.orbBullet;
					menuUp = false;
				}
			}


			if(GUI.Button(new Rect(20,80,80,20), "Light Grenade")) {
				if(TechManager.IsTechAvaliable(Tech.lightGrenade)){
					TechManager.Research(Tech.lightGrenade);
					menuUp = false;
				}
			}

			if(GUI.Button(new Rect(20,100,80,20), "Bullet Absorber")) {
				if(TechManager.IsTechAvaliable(Tech.bulletAbsorber)){
					TechManager.Research(Tech.bulletAbsorber);
					menuUp = false;
				}
			}

			if(GUI.Button(new Rect(20,120,80,20), "Clip Size")) {
				if(TechManager.IsTechAvaliable(Tech.clipSize)){
					TechManager.Research(Tech.clipSize);

					//here until we have a event manager for upgrades
					GameManager.AvatarContr.shootScript.clipSize += 10 * TechManager.GetUpgradeLv(Tech.clipSize);
					menuUp = false;
				}
			}



		}
	
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.V)){
			menuUp = !menuUp;
		}
	
	}
}
