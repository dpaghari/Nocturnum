//Purpose: Attach to objects that should fade when inbetween player and camera

//Note: This script only swaps shaders

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent (typeof (Collider))]

public class IsInvisibleOntopObjects : MonoBehaviour {

	//Shader to use when hiding the object
	public Shader hideShader;

	//Duration that the object is hidden after it no longer blocks the player
	public float hiddenDuration = 1;

	public bool IsHidden {get; private set;}

	private DumbTimer hiddenDurationTimer;

	//Store references and original shaders to revert to
	private Renderer[] theRenderers;
	private Shader[] originalShaders;



	public void Hide(){
		for(int i = 0; i < theRenderers.Length; i++){
			Material theMat = theRenderers[i].material;
			if( theMat != null) theMat.shader = hideShader;
			if( theMat != null) theRenderers[i].material.shader = hideShader;
		}

		IsHidden = true;
		hiddenDurationTimer.Reset();
	}



	public void Show(){
		for(int i = 0; i < theRenderers.Length; i++){
			Material theMat = theRenderers[i].material;
			if( theMat != null) theMat.shader = originalShaders[i];
		}
		IsHidden = false;
		hiddenDurationTimer.Reset();
	}



	private void GetShaderAndRenderers( ){
		theRenderers = GetComponentsInChildren<Renderer>();
		originalShaders = new Shader[theRenderers.Length];
		
		for(int i = 0; i < theRenderers.Length; i++){
			Material theMat = theRenderers[i].material;
			if( theMat != null) originalShaders[i] = theMat.shader;
		}

	}



	void Start () {
		hiddenDurationTimer = DumbTimer.New( hiddenDuration);
		GetShaderAndRenderers( );
	}
	


	void Update () {
		if( hiddenDurationTimer.Finished()) Show();
		hiddenDurationTimer.Update();
	}



}
