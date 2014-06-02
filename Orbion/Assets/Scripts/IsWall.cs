//Purpose: Spins wall model and allows player to walk through them

using UnityEngine;
using System.Collections;

public class IsWall : MonoBehaviour {

	public Vector3 rotation = Vector3.up;
	public GameObject model;

	void FixedUpdate(){
		model.transform.Rotate(rotation);
	}

	void OnCollisionEnter( Collision other){
			int otherLayerMask = 1 << other.gameObject.layer;
			if( otherLayerMask == Utility.Player_PLM)
				Physics.IgnoreCollision( this.collider, other.collider);
	}

}
