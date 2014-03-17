using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanMove))]
public class IsFog : MonoBehaviour {

	//Current Target the fog is heading towards
	public GameObject currTarget;

	//The range at which the fog searches for entities other than the MainGenerator
	public float searchRange = 10;

	private Vector3 dir;
	private CanMove moveScript;

	public static bool IsValidTarget( GameObject gobj){
		
		if ( Utility.GoHasComponent<IsGenerator>( gobj)){
		
			Corruption corruptScript = gobj.GetComponent<Corruption>();

			if(corruptScript.active){
				return true;	
			}

		}
		//plant condition
		return false;
	}

	public GameObject FindTarget(){
		
		GameObject target = null;
		
		target = Utility.GetClosestWith(rigidbody.position, searchRange, IsValidTarget);

		if( target == null) target = GameManager.MainGenerator;

		return target;
	}

	// Use this for initialization
	void Start() {
		MetricManager.AddFog(1);
		
		if (currTarget == null)
			currTarget = GameManager.MainGenerator;
		dir = Utility.FindDirNoY(transform.position, currTarget.transform.position);
		moveScript = GetComponent<CanMove>();
	}
	


	// Update is called once per frame
	void Update() {
		currTarget = FindTarget();
	}

	void FixedUpdate() {
		dir = Utility.FindDirNoY(transform.position, currTarget.transform.position);
		Debug.DrawRay(transform.position, dir*2);
		moveScript.Move(dir);
	}
	
	void OnTriggerStay(Collider collide){
		if(collide.tag == "Plant"){
			Vector3 targ = collide.transform.position;
			Vector3 direction = targ - transform.position;
			direction.Normalize ();
			transform.position += direction * 15.0F * Time.deltaTime;
		}
	}
	void OnTriggerEnter(Collider collide){
		if(collide.gameObject.tag == "Plant"){
		
			collide.gameObject.GetComponent<IsFogEater>().fogCount++;
			//Debug.Log("dead on collision");
			MetricManager.AddFog(-1);
			
			Destroy (gameObject);

		}
	}

}
