using UnityEngine;
using System.Collections;


public enum UseType{
	none,
	rotateWithUser,
	lightgeyserUse,
	depositLGEnergy

}

public class Useable : MonoBehaviour {

	public UseType useBehaviorType = UseType.none;
	public float rotationSpeed = 1;

	public GameObject item;
	private GameObject clone;

	
	
	private bool IsToggling = false;
	
	private GameObject user;
	private CanUse userUseScript;

	private delegate void ActionHandler( GameObject user);
	private event ActionHandler actionBehavior;
	private void OnUpdateAction( GameObject user){
		if ( actionBehavior != null)
			actionBehavior( user);
	}

	public void RotateWithUser( GameObject user){
		Vector3 vectorToTarget = user.transform.position - transform.position;
		vectorToTarget.y = 0; //prevent looking down/upwards
		Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget); 
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
	}
	public void lightgeyserUse( GameObject user){

		Vector3 pos = transform.position;
		pos.y += 5;
		if(ResManager.LGEnergy < ResManager.LGMaxEnergy)
		clone = Instantiate(item, pos, Quaternion.identity) as GameObject;
	
	}

	public void depositLGEnergy(GameObject user){
		if(ResManager.LGEnergy >= ResManager.LGMaxEnergy){
			//Debug.Log (ResManager.LGCoreCharges);
			ResManager.AddLGCoreCharge(1);
			ResManager.ResetLGContainer();
		}

	}

	public void Activate(CanUse useScript){
		userUseScript = useScript;
		user = useScript.gameObject;
		IsToggling = !IsToggling;
		switch( useBehaviorType){

			case UseType.rotateWithUser :
				actionBehavior += RotateWithUser;
				break;

			case UseType.lightgeyserUse :
				actionBehavior += lightgeyserUse;
				break;

			case UseType.depositLGEnergy :
				actionBehavior += depositLGEnergy;
				break;	

			case UseType.none :
				actionBehavior = null;
				break;

			default:
				actionBehavior = null;
				Debug.LogError(string.Format("Unspecified useBeheaviorType for {0} in {1}.cs", gameObject.name, this.GetType()) );
				break;
		}

	}

	public bool UserInRange( float range){
		Vector3 distanceVector = user.transform.position - transform.position;
		distanceVector.y = 0; //don't let the height difference be factored in
		return distanceVector.magnitude <= userUseScript.useRange;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if( IsToggling) {
			OnUpdateAction( user);
			if ( !UserInRange( userUseScript.useRange)) IsToggling = false;
		}
	}
}
