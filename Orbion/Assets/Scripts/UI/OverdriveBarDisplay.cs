using UnityEngine;
using System.Collections;

public class OverdriveBarDisplay : MonoBehaviour {

	public GameObject playerRef;
	public dfProgressBar _overdriveFill;
	public float fillBar;
	// Use this for initialization
	void Start () {
		playerRef = GameObject.Find ("player_prefab");
		fillBar = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		fillBar = playerRef.GetComponent<hasOverdrive>().overdriveCount/playerRef.GetComponent<hasOverdrive>().overdriveLimit;
		_overdriveFill.Value = fillBar;
	}
}
