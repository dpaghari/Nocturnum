using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IsMainGenerator : MonoBehaviour {

	//the child object that holds our debug text
	public GameObject debugTextObj;



	//array of IsGenerator scripts from our generators
	private IsGenerator[] generators;

	//the textmesh component of our debugTextObj
	private TextMesh debugText;
	


	//total amount of lumen the generators have
	public int CurrLumen {get; protected set;}

	//total amount of lumen we need to win
	public int MaxLumen {get; protected set;}



	// Use this for initialization
	void Start () {
		generators = FindObjectsOfType<IsGenerator>();
		if (generators.Length == 0)
			Debug.LogWarning("Scene has a main generator but no regular generators could be found.");


		CurrLumen = 0;
		MaxLumen = 0;

		for (int i = 0; i < generators.Length; i++)
			MaxLumen += generators[i].MaxLumen;
			
		debugText = debugTextObj.GetComponent<TextMesh>();
	}
	

	// Update is called once per frame
	void Update () {
		CurrLumen = 0;
		for (int i = 0; i < generators.Length; i++)
			CurrLumen += generators[i].CurrLumen;

		debugText.text = string.Format("{0}/{1}", CurrLumen, MaxLumen);
	}

}
