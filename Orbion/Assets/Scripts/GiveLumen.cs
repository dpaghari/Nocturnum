using UnityEngine;
using System.Collections;

public class GiveLumen : MonoBehaviour {

	public GameObject lumenShard;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void spawnLumen(){
		if(lumenShard != null){
			Vector3 temp = transform.position;
			temp.y += 4;
			Instantiate (lumenShard, temp, this.transform.rotation);
		}
	}
}
