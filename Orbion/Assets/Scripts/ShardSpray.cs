using UnityEngine;
using System.Collections;



public class ShardSpray : MonoBehaviour {

	public CanShootReload shootScript {get; private set;}


	void Start () {
		shootScript = GetComponent<CanShootReload>();
	}

	void Update () {
		if( shootScript.FinishCooldown())
			shootScript.Shoot( transform.forward - transform.position);

		if( shootScript.currentAmmo <= 0)
			GameObject.Destroy( this.gameObject);
	}
}
