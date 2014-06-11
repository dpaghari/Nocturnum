using UnityEngine;
using System.Collections;

//TODO: doesn't automatically correct changes on prereq information

public class UpgradeMenuButtons : MonoBehaviour {
	//public dfPanel _panel;
	//private bool menuUp;
	public GameObject canResearchRef;
	public bool researchTimeOn;
	public dfControl hudProgress; //dfControl for the HUD progress.
	public CanResearch researchScript; //to handle shortcuts.
	public dfSprite _hudIcon; //Sprite on the HUD.

	public AudioClip buttonSound;
	// Use this for initialization

	public Tech techType = Tech.none;

	private dfLabel descriptionLabel;
	private dfLabel energyLabel;
	private dfLabel lumenLabel;
	//private dfLabel nameLabel;

	
	
	
	void SetLabel(){
		foreach( Transform child in transform){
			switch( child.name){
			case "Description":
				descriptionLabel = child.GetComponent<dfLabel>();
				break;
				
			case "Energy":
				energyLabel = child.GetComponent<dfLabel>();
				break;
				
			case "Lumen":
				lumenLabel = child.GetComponent<dfLabel>();
				break;
				
			default:
				break;
			}
		}
	}


	void UpdateCosts(){
		if( TechManager.IsUpgrade( techType)){
			UpgradeCostStruct upgradeInfo = TechManager.GetUpgradeCosts( techType);
			energyLabel.Text = string.Format("{0}", upgradeInfo.energy);
			lumenLabel.Text =  string.Format("{0}", upgradeInfo.lumen);
		}
		else 
			Debug.LogWarning( string.Format( "{0} has non upgrade techType: {1}", this.name, techType)); 
	}

	void Start () {
		//menuUp = false;
		//_panel.IsVisible = false;
		canResearchRef = GameObject.Find ("player_prefab");
		researchScript = GameManager.AvatarContr.GetComponent<CanResearch>();
		_hudIcon.IsVisible = false;

		SetLabel();


	}
	
	// Update is called once per frame
	void Update () {
		UpdateCosts();

		//Shortcuts.
		if(researchScript.MenuUp){
			if(Input.GetKeyDown(KeyCode.Alpha1)){
				CallScattershot();
			}
			else if(Input.GetKeyDown(KeyCode.Alpha2)){
				CallClipSize();
			}
			
			else if(Input.GetKeyDown(KeyCode.Alpha3)){
				CallLightFist();
			}

			else if(Input.GetKeyDown(KeyCode.Alpha4)){
				CallLightGrenade();
			}
			
			else if(Input.GetKeyDown(KeyCode.Alpha5)){
				CallSeeker ();
			}
			
			else if(Input.GetKeyDown(KeyCode.Alpha6)){
				CallRicochet();
			}
		}

	}

	//Different call methods for upgrades.
	public void CallScattershot(){
		if(canResearchRef.GetComponent<CanResearch>().MeetsRequirement(Tech.scatter)){
			audio.PlayOneShot(buttonSound, 0.2f);
			canResearchRef.GetComponent<CanResearch>().GetScattershot();
			_hudIcon.IsVisible = true;
			StartCoroutine( showCooldown(TechManager.GetUpgradeTime( Tech.scatter)) );
		} else {
			canResearchRef.GetComponent<CanResearch>().GetScattershot();
		}
	}

	public void CallClipSize(){
		if(canResearchRef.GetComponent<CanResearch>().MeetsRequirement(Tech.clipSize)){
			audio.PlayOneShot(buttonSound, 0.2f);
			canResearchRef.GetComponent<CanResearch>().GetClipSize();
			_hudIcon.IsVisible = true;
			StartCoroutine( showCooldown(TechManager.GetUpgradeTime( Tech.clipSize)) );
		} else {
			canResearchRef.GetComponent<CanResearch>().GetClipSize();
		}
	}

	public void CallLightFist(){
		if(canResearchRef.GetComponent<CanResearch>().MeetsRequirement(Tech.lightFist)){
			audio.PlayOneShot(buttonSound, 0.2f);
			canResearchRef.GetComponent<CanResearch>().GetLightFist();
			_hudIcon.IsVisible = true;
			StartCoroutine( showCooldown(TechManager.GetUpgradeTime( Tech.lightFist)) );
		} else {
			canResearchRef.GetComponent<CanResearch>().GetLightFist();
		}
	}

	public void CallLightGrenade(){
		if(canResearchRef.GetComponent<CanResearch>().MeetsRequirement(Tech.lightGrenade)){
			audio.PlayOneShot(buttonSound, 0.2f);
			canResearchRef.GetComponent<CanResearch>().GetLightGrenade();
			_hudIcon.IsVisible = true;
			StartCoroutine( showCooldown(TechManager.GetUpgradeTime( Tech.lightGrenade)) );
		} else {
			canResearchRef.GetComponent<CanResearch>().GetLightGrenade();
		}
	}

	public void CallSeeker(){
		if(canResearchRef.GetComponent<CanResearch>().MeetsRequirement(Tech.seeker)){
			audio.PlayOneShot(buttonSound, 0.2f);
			canResearchRef.GetComponent<CanResearch>().GetSeeker();
			_hudIcon.IsVisible = true;
			StartCoroutine( showCooldown(TechManager.GetUpgradeTime( Tech.seeker)) );
		} else {
			canResearchRef.GetComponent<CanResearch>().GetSeeker();
		}
	}

	public void CallRicochet(){
		if(canResearchRef.GetComponent<CanResearch>().MeetsRequirement(Tech.ricochet)){
			audio.PlayOneShot(buttonSound, 0.2f);
			canResearchRef.GetComponent<CanResearch>().GetRicochet();
			_hudIcon.IsVisible = true;
			StartCoroutine( showCooldown(TechManager.GetUpgradeTime( Tech.ricochet)) );
		} else {
			canResearchRef.GetComponent<CanResearch>().GetRicochet();
		}
	}

	//Code from the example for the radial upgrade time.
	//Visual to show upgrade time.
	private IEnumerator showCooldown(float t)
	{
		
		researchTimeOn = true;
		
		//var assignedSpell = SpellDefinition.FindByName( this.Spell );

		//Sprite on Upgrade menu
		var sprite = GetComponent<dfControl>().Find( "Researching" ) as dfSprite;
		sprite.IsVisible = true;
		//Sprite on HUD
		var spriteHUD = hudProgress.Find("Researching") as dfSprite;
		spriteHUD.IsVisible = true;

		var startTime = Time.realtimeSinceStartup;
		var endTime = startTime + t + 2;
		
		while( Time.realtimeSinceStartup < endTime )
		{
			
			var elapsed = Time.realtimeSinceStartup - startTime;
			var lerp = 1f - elapsed / t;
			
			sprite.FillAmount = lerp;
			spriteHUD.FillAmount = lerp;
			
			yield return null;
			
		}
		
		sprite.FillAmount = 1f;
		sprite.IsVisible = false;
		spriteHUD.FillAmount = 1f;
		spriteHUD.IsVisible = false;
		
		researchTimeOn = false;
		
	}
}
