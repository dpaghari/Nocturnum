using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IsLightSource : MonoBehaviour {

	public static Dictionary<int, IsLightSource> LightSources { get; private set;}
	public SphereCollider lightArea;



	// Use this for initialization
	void Start () {
		lightArea = GetComponent<SphereCollider>();
	}
	
	void OnEnable() {
		if( LightSources == null) LightSources = new Dictionary<int, IsLightSource>();
		LightSources[this.GetInstanceID()] = this; 
	}

	void OnDisable(){
		LightSources.Remove(this.GetInstanceID());
	}

	// Update is called once per frame
	void Update () {
	}

	
	void OnTriggerStay(Collider other){
		WeakensInLight weakenScript = other.GetComponent<WeakensInLight>();
		if(weakenScript) weakenScript.Weaken();
	}
}
