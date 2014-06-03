using UnityEngine;
using System.Collections;

public class IsVisibleUnderObjects : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction =  transform.position - Camera.main.transform.position;
		RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, direction);


		for( int i=0; i<hits.Length; i++){
			IsInvisibleOntopObjects visibilityScript = hits[i].collider.gameObject.GetComponent<IsInvisibleOntopObjects>();
			if( visibilityScript != null) visibilityScript.Hide();
		}
	}
}
