using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	public GameObject playerRef;
	public dfProgressBar _overdriveFill;
	public float fillBar;
	public Killable killScript;
	private float duration = 0.2F;



	// Use this for initialization
	void Start () {
		killScript = GameManager.AvatarContr.GetComponent<Killable>();
		playerRef = GameObject.Find ("player_prefab");
		fillBar = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		_overdriveFill.MaxValue = killScript.baseHP;
		fillBar = killScript.currHP;
		_overdriveFill.Value = fillBar;
		if(killScript.currHP <= (killScript.baseHP * 0.30f)){
			Color color1 = Color.white;
			Color color2 = Color.clear;
			
			float t = Mathf.PingPong(Time.time, duration) / duration;
			_overdriveFill.ProgressColor = Color.Lerp(color1, color2, t);
			
		}
	
	}
}
