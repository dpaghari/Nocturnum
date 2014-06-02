//Purpose : Special functinality of generators

using UnityEngine;
using System.Collections;

public class IsGenerator : MonoBehaviour {
	public AudioClip genHum;

	//Rate of energy production
	public int EnergyPerPulse = 1;
	public float PulseInterval = 5.0f;
	private DumbTimer energyAddTimer;

	//Parts of the model we're rotating
	public GameObject GeneratorRing;
	public GameObject GeneratorSphere;
	



	void Start () {
		energyAddTimer = DumbTimer.New( PulseInterval);

	}


	void Update () {
		audio.PlayOneShot(genHum, 1.0f);

		if(energyAddTimer.Finished()){
			ResManager.AddEnergy( EnergyPerPulse);
			energyAddTimer.Reset();
		}
		else
			energyAddTimer.Update();

	}


	void FixedUpdate(){
		GeneratorRing.transform.Rotate(new Vector3(0, 0, 1));
		GeneratorSphere.transform.Rotate(new Vector3(0, 0, 1));
	}


}
