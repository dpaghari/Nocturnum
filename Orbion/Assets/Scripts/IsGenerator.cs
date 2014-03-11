using UnityEngine;
using System.Collections;

public class IsGenerator : MonoBehaviour {
	public AudioClip genHum;

	//public GameObject player;
	//public GameObject vislight;

	//stores the amount unloaded
	public int CurrLumen = 0;

	//stores the total amount to completely fill generator
	public int MaxLumen = 10;
	public bool isCharged = false;
	public bool cantCharge = false;
	//references to child objects
	public GameObject ringModel;
	public GameObject sphereModel;
	public GameObject debugText;
	public GameObject lightSource;
	private TextMesh debugTextComp;


	private int prevLumen;

	// Use this for initialization
	void Start () {
		debugTextComp = debugText.GetComponent<TextMesh>();
		//player = GameObject.Find("player_prefab");
	}

	void OnDestroy() {

	}
	
	// Update is called once per frame
	void Update () {


		//Debug.Log (vislight.light.range);
		if(CurrLumen == MaxLumen && !cantCharge){
			isCharged = true;
			cantCharge = true;
		}


		if(CurrLumen != prevLumen){
		lightSource.light.range += 2;

		}
		if(isCharged){
			GameManager.AvatarContr.pointLight.light.range += 1;
			GameManager.AvatarContr.lightconeObj.light.range += 2;
			GameManager.AvatarContr.lightconeObj.light.spotAngle += 10;
			float temp = GameManager.AvatarContr.lightconeObj.GetComponent<BoxCollider>().size.x;
			temp += 2;



			isCharged = false;
		}

		audio.PlayOneShot(genHum, 1.0f);

		ringModel.transform.Rotate(new Vector3(0, 0, 1));
		sphereModel.transform.Rotate(new Vector3(0, 0, 1));

		debugTextComp.text = string.Format("{0}/{1}", CurrLumen, MaxLumen);

		prevLumen = CurrLumen;

	}
}
