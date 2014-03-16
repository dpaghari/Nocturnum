using UnityEngine;
using System.Collections;

public class GiveLumen : MonoBehaviour {
	
	
	public GameObject risingShard;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void spawnLumen(){
		if(risingShard != null){
			Vector3 temp2 = transform.position;
			Instantiate (risingShard, temp2, this.transform.rotation);
		}
	}
}