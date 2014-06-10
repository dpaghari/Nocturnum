//Purpose: Spins wall model and allows player to walk through them

using UnityEngine;
using System.Collections;

public class IsWall : MonoBehaviour {

	public Vector3 rotation = Vector3.up;
	public GameObject model;


	public Killable killScript {get; private set;}
	public Color lowHealthTint = Color.white;
	private Color originalColor;
	private Material wallMat;

	void Start () {
		killScript = GetComponent<Killable>();
		MeshRenderer renderer = model.GetComponent<MeshRenderer>();
		if( renderer != null){
			wallMat = renderer.material;
			originalColor = wallMat.GetColor("_TintColor");
		}
	}

	void FixedUpdate(){
		model.transform.Rotate(rotation);
		Color newColor = Color.Lerp( lowHealthTint, originalColor, (float)killScript.currHP/killScript.baseHP);
		wallMat.SetColor("_TintColor", newColor);
	}

	void OnCollisionEnter( Collision other){
			int otherLayerMask = 1 << other.gameObject.layer;
			if( otherLayerMask == Utility.Player_PLM)
				Physics.IgnoreCollision( this.collider, other.collider);
	}

}
