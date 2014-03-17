using UnityEngine;
using System.Collections;

public enum CorruptType {
	none = 0,
	generator,
	mainGenerator,
	luna
}


//replace killable with this in generator prefab
public class Corruptable : MonoBehaviour {

	//Corruption goes from Min to Max
	//Something is corrupted if it is greater than 0, and can be corrupted up to the maximum
	//Something is not corrupted if it is less than or equal to 0, and can be illumnated to the minimum
	public float Corruption = 0;
	public float MinCorruption = -80.0f;
	public float MaxCorruption = 80.0f;

	//Whether the object is currently corrupted
	public bool IsCorrupted { get; protected set; }

	//Used to pick special corruption behaviors
	public CorruptType corruptType = CorruptType.none;

	public bool ShouldBeCorrupted() { return Corruption > 0; }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EnterCorruption(){

		IsCorrupted = true;
	}

	public void ExitCorruption(){

		IsCorrupted = false;
	}

	public void Corrupt(float amt){
		Corruption += amt;
		Corruption = Mathf.Clamp (Corruption, MinCorruption, MaxCorruption);

 		if ( !IsCorrupted && ShouldBeCorrupted() )
			EnterCorruption();
		else if ( IsCorrupted && !ShouldBeCorrupted() )
			ExitCorruption();
	}



}
