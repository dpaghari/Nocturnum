using UnityEngine;
using System.Collections;

public class IsGenerator : MonoBehaviour {
	public int energyGeneration = 50;
	public AudioClip genHum;

	public int EnergyPerPulse = 1;
	public float PulseInterval = 5.0f;
	private DumbTimer energyAddTimer;


	// Use this for initialization
	void Start () {
		//ResManager.AddEnergy(energyGeneration);
		energyAddTimer = DumbTimer.New( PulseInterval);

	}

	void OnDestroy() {
		//ResManager.RmEnergy(energyGeneration);
	}
	
	// Update is called once per frame
	void Update () {
		audio.PlayOneShot(genHum, 1.0f);
		transform.GetChild(1).transform.Rotate(new Vector3(0, 0, 1));

		transform.GetChild(transform.childCount - 3).transform.Rotate(new Vector3(0, 0, 1));

		if(energyAddTimer.Finished()){
			ResManager.AddEnergy( EnergyPerPulse);
			energyAddTimer.Reset();
		}
		else
			energyAddTimer.Update();

	}
}
