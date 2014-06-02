using UnityEngine;
using System.Collections;

public class IsPinkCube : MonoBehaviour {

	private DumbTimer killSelf;
	public float killInterval = 1.0f;
	
	

	// Use this for initialization
	void Start () {
		killSelf = DumbTimer.New( killInterval);
		
	}
	
	// Update is called once per frame
	void Update () {

		if(killSelf.Finished()){
			Destroy(this.gameObject);
			//killSelf.Reset();
		}
		else{
			killSelf.Update();
		}
	
	}
}
