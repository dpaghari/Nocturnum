using UnityEngine;
using System.Collections;

public class ResearchTime : MonoBehaviour {

	public bool researchTimeOn;
	public float researchDelay;

	// Use this for initialization
	void Start () {
		researchDelay = 5.0f;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private IEnumerator showCooldown()
	{
		
		researchTimeOn = true;
		
		//var assignedSpell = SpellDefinition.FindByName( this.Spell );
		
		var sprite = GetComponent<dfControl>().Find( "CoolDown" ) as dfSprite;
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
