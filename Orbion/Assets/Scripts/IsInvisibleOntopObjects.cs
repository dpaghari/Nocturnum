using UnityEngine;
using System.Collections;

public class IsInvisibleOntopObjects : MonoBehaviour {

	private Shader originalShader;
	public Shader hideShader;
	private DumbTimer hiddenDurationTimer;
	public float hiddenDuration = 1;
	public bool IsHidden {get; private set;}

	public void Hide(){
		//Debug.Log(string.Format("Should hide {0}.", name));
		renderer.material.shader = hideShader;
		IsHidden = true;
		hiddenDurationTimer.Reset();
	}

	public void Show(){
		renderer.material.shader = originalShader;
		IsHidden = false;
		hiddenDurationTimer.Reset();
	}

	// Use this for initialization
	void Start () {
		originalShader = renderer.material.shader;
		hiddenDurationTimer = DumbTimer.New( hiddenDuration);
		hiddenDurationTimer.Reset();
	}
	
	// Update is called once per frame
	void Update () {
		if( hiddenDurationTimer.Finished()) Show();
		hiddenDurationTimer.Update();
	}
}
