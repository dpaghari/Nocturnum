using UnityEngine;
using System.Collections;

public class UpgradeMenuButtons : MonoBehaviour {
	//public dfPanel _panel;
	//private bool menuUp;
	public GameObject canResearchRef;
	public bool researchTimeOn;
	public float researchDelay;

	// Use this for initialization
	void Start () {
		//menuUp = false;
		//_panel.IsVisible = false;
		canResearchRef = GameObject.Find ("player_prefab");
		researchDelay = 12.0f;
	}
	
	// Update is called once per frame
	void Update () {
		/*if(Input.GetKeyDown(KeyCode.B)){
				menuUp = !menuUp;
		}
		if(menuUp){
			_panel.IsVisible = true;
		} else {
			_panel.IsVisible = false;
		}*/


	}

	public void CallScattershot(){
		if(canResearchRef.GetComponent<CanResearch>().MeetsRequirement(Tech.scatter)){
			canResearchRef.GetComponent<CanResearch>().GetScattershot();
			StartCoroutine( showCooldown() );
		} else {
			canResearchRef.GetComponent<CanResearch>().GetScattershot();
		}
	}
	public void CallClipSize(){
		canResearchRef.GetComponent<CanResearch>().GetClipSize();
	}
	public void CallLightFist(){
		canResearchRef.GetComponent<CanResearch>().GetLightFist();
	}
	public void CallLightGrenade(){
		canResearchRef.GetComponent<CanResearch>().GetLightGrenade();
	}
	public void CallSeeker(){
		canResearchRef.GetComponent<CanResearch>().GetSeeker();
	}
	public void CallRicochet(){
		canResearchRef.GetComponent<CanResearch>().GetRicochet();
	}

	private IEnumerator showCooldown()
	{
		
		researchTimeOn = true;
		
		//var assignedSpell = SpellDefinition.FindByName( this.Spell );
		
		var sprite = GetComponent<dfControl>().Find( "Researching" ) as dfSprite;
		sprite.IsVisible = true;
		
		var startTime = Time.realtimeSinceStartup;
		var endTime = startTime + researchDelay;
		
		while( Time.realtimeSinceStartup < endTime )
		{
			
			var elapsed = Time.realtimeSinceStartup - startTime;
			var lerp = 1f - elapsed / researchDelay;
			
			sprite.FillAmount = lerp;
			
			yield return null;
			
		}
		
		sprite.FillAmount = 1f;
		sprite.IsVisible = false;
		
		researchTimeOn = false;
		
	}
}
