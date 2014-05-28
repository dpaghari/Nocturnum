using UnityEngine;
using System.Collections;

public class UpgradeMenuButtons : MonoBehaviour {
	//public dfPanel _panel;
	//private bool menuUp;
	public GameObject canResearchRef;
	public bool researchTimeOn;
	public dfControl hudProgress;
	public CanResearch researchScript;
	public dfSprite _hudIcon;
	// Use this for initialization
	void Start () {
		//menuUp = false;
		//_panel.IsVisible = false;
		canResearchRef = GameObject.Find ("player_prefab");
		researchScript = GameManager.AvatarContr.GetComponent<CanResearch>();
		_hudIcon.IsVisible = false;

	}
	
	// Update is called once per frame
	void Update () {
	
		if(researchScript.MenuUp){
			if(Input.GetKeyDown(KeyCode.Alpha1)){
				CallScattershot();
			}
			else if(Input.GetKeyDown(KeyCode.Alpha2))
				CallClipSize();
			
			else if(Input.GetKeyDown(KeyCode.Alpha3))
				CallLightFist();

			else if(Input.GetKeyDown(KeyCode.Alpha4))
				CallLightGrenade();
			
			else if(Input.GetKeyDown(KeyCode.Alpha5))
				CallSeeker ();
			
			else if(Input.GetKeyDown(KeyCode.Alpha6))
				CallRicochet();
		}

	}

	public void CallScattershot(){
		if(canResearchRef.GetComponent<CanResearch>().MeetsRequirement(Tech.scatter)){
			canResearchRef.GetComponent<CanResearch>().GetScattershot();
			_hudIcon.IsVisible = true;
			StartCoroutine( showCooldown(TechManager.GetUpgradeTime( Tech.scatter)) );
		} else {
			canResearchRef.GetComponent<CanResearch>().GetScattershot();
		}
	}

	public void CallClipSize(){
		if(canResearchRef.GetComponent<CanResearch>().MeetsRequirement(Tech.clipSize)){
			canResearchRef.GetComponent<CanResearch>().GetClipSize();
			_hudIcon.IsVisible = true;
			StartCoroutine( showCooldown(TechManager.GetUpgradeTime( Tech.clipSize)) );
		} else {
			canResearchRef.GetComponent<CanResearch>().GetClipSize();
		}
	}

	public void CallLightFist(){
		if(canResearchRef.GetComponent<CanResearch>().MeetsRequirement(Tech.lightFist)){
			canResearchRef.GetComponent<CanResearch>().GetLightFist();
			_hudIcon.IsVisible = true;
			StartCoroutine( showCooldown(TechManager.GetUpgradeTime( Tech.lightFist)) );
		} else {
			canResearchRef.GetComponent<CanResearch>().GetLightFist();
		}
	}

	public void CallLightGrenade(){
		if(canResearchRef.GetComponent<CanResearch>().MeetsRequirement(Tech.lightGrenade)){
			canResearchRef.GetComponent<CanResearch>().GetLightGrenade();
			_hudIcon.IsVisible = true;
			StartCoroutine( showCooldown(TechManager.GetUpgradeTime( Tech.lightGrenade)) );
		} else {
			canResearchRef.GetComponent<CanResearch>().GetLightGrenade();
		}
	}

	public void CallSeeker(){
		if(canResearchRef.GetComponent<CanResearch>().MeetsRequirement(Tech.seeker)){
			canResearchRef.GetComponent<CanResearch>().GetSeeker();
			_hudIcon.IsVisible = true;
			StartCoroutine( showCooldown(TechManager.GetUpgradeTime( Tech.seeker)) );
		} else {
			canResearchRef.GetComponent<CanResearch>().GetSeeker();
		}
	}
	public void CallRicochet(){
		if(canResearchRef.GetComponent<CanResearch>().MeetsRequirement(Tech.ricochet)){
			canResearchRef.GetComponent<CanResearch>().GetRicochet();
			_hudIcon.IsVisible = true;
			StartCoroutine( showCooldown(TechManager.GetUpgradeTime( Tech.ricochet)) );
		} else {
			canResearchRef.GetComponent<CanResearch>().GetRicochet();
		}
	}

	private IEnumerator showCooldown(float t)
	{
		
		researchTimeOn = true;
		
		//var assignedSpell = SpellDefinition.FindByName( this.Spell );
		
		var sprite = GetComponent<dfControl>().Find( "Researching" ) as dfSprite;
		sprite.IsVisible = true;
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
