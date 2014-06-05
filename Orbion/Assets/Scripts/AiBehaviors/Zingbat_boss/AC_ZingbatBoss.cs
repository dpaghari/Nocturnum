using UnityEngine;
using System.Collections;

public class AC_ZingbatBoss : AiController {

	public CanMove moveScript;
	public int minDefaultHits = 4;
	public int aoeRangeActivate = 10;
	public int currDefaultHits {get; set;}

	public AB_ZingbatBoss_Default defaultBehavior {get; private set;}
	public AB_ZingbatBoss_Aoe aoeBehavior {get; private set;}


	public bool ShouldAoe(){
		if( CurrBehavior != defaultBehavior || defaultBehavior.IsAttacking())
			return false;

		if( currDefaultHits < minDefaultHits)
			return false;

		GameObject player = GameManager.Player;
		if( player == null)
			return false;

		if( Utility.FindDistNoY( this.gameObject, player) <= aoeRangeActivate)
			return true;

		return false;
	}	


	protected void Start() {
		moveScript = GetComponent<CanMove>();
		defaultBehavior = GetComponent<AB_ZingbatBoss_Default>();
		aoeBehavior = GetComponent<AB_ZingbatBoss_Aoe>();


		CurrBehavior.OnBehaviorEnter();

		currDefaultHits = 0;
	}

	protected void FixedUpdate () {
		CurrBehavior.FixedUpdateAB();
	}
	
	
	protected void Update () {
		if(	ShouldAoe())
			SwitchBehavior( aoeBehavior);

		CurrBehavior.UpdateAB();
	}

	void OnCollisionEnter(Collision other){
		int layerMask = 1 << other.collider.gameObject.layer;
		int flyOverObjectsMask = Utility.Building_PLM | Utility.Enemy_PLM | Utility.Plant_PLM | Utility.Quest_PLM;
		if( (layerMask & flyOverObjectsMask) != 0)
			Physics.IgnoreCollision( this.collider, other.gameObject.collider);
	}

}
