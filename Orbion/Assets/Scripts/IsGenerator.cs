using UnityEngine;
using System.Collections;

public class IsGenerator : MonoBehaviour {
	public AudioClip genHum;

	//stores the amount unloaded
	public int CurrLumen = 0;

	//stores the total amount to completely fill generator
	public int MaxLumen = 100;

	//references to child objects
	public GameObject ringModel;
	public GameObject sphereModel;
	public GameObject debugText;
	private TextMesh debugTextComp;

	// Use this for initialization
	void Start () {
		debugTextComp = debugText.GetComponent<TextMesh>();
	}

	void OnDestroy() {

	}
	
	// Update is called once per frame
	void Update () {
		audio.PlayOneShot(genHum, 1.0f);

		ringModel.transform.Rotate(new Vector3(0, 0, 1));
		sphereModel.transform.Rotate(new Vector3(0, 0, 1));

		debugTextComp.text = string.Format("{0}/{1}", CurrLumen, MaxLumen);

	}
}
