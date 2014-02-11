using UnityEngine;
using System.Collections;

public class IsUnderConstruction : MonoBehaviour {

	float constructionCountdown;
	public float totalConstruction = 1000;
	public Rigidbody toBuild;
	private bool lit = false;
	private Rigidbody clone;
	private Vector3 height;
	private int rotationSpeed = 1;

	// Use this for initialization
	void Start () {
		constructionCountdown = totalConstruction - 100;
	}
	
	// Update is called once per frame
	void Update () {
		if(!lit){
			constructionCountdown += Time.deltaTime * 100;
			if(constructionCountdown > totalConstruction){
				ResManager.AddLumen(toBuild.gameObject.GetComponent<Buildable>().cost);
				ResManager.RmUsedEnergy(toBuild.gameObject.GetComponent<Buildable>().energyCost);
				Destroy(this.gameObject);
			}
		}

		height = this.gameObject.transform.localScale;
		height.y = 5 * (constructionCountdown / totalConstruction);
		this.gameObject.transform.localScale = height;
	}

	void OnTriggerStay(Collider other){
		if(other.tag == "lightsource"){
			this.gameObject.transform.Rotate(new Vector3(0, 1, 0));
			constructionCountdown -= Time.deltaTime * 50;
			lit = true;
		}
		

		if(constructionCountdown <= 1){
			clone = Instantiate(toBuild, this.transform.position, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as Rigidbody;
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "playerBullet"){
			constructionCountdown -= 10;
			this.gameObject.transform.Rotate(new Vector3(0, 2, 0));
		}
	}
}
