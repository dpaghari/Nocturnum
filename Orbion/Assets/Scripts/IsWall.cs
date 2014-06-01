using UnityEngine;
using System.Collections;

public class IsWall : MonoBehaviour {

	public Vector3 rotation = Vector3.up;
	public GameObject model;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		model.transform.Rotate(rotation);
	}

	void OnCollisionEnter( Collision other){
			int otherLayerMask = 1 << other.gameObject.layer;
			if( otherLayerMask == Utility.Player_PLM)
				Physics.IgnoreCollision( this.collider, other.collider);
	}

}
