using UnityEngine;
using System.Collections;

public class SendSpriteToBack : MonoBehaviour {
	public dfSprite sprite;
	// Use this for initialization
	void Start () {
		sprite.BringToFront ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
