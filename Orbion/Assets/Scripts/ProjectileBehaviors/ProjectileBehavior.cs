using UnityEngine;
using System.Collections;

//Supposed to be an interface to implement but we can't use the inspector for interface
//variables so it's just easier to make it an abstract class that inherits from
//MonoBehavior since all our behaviors are likely to do so anyways.


//Every projectile behavior should inherit from this base class
//The ProjectileController holds which behavior it wants to perform and calls the
//abstract declared functions below.

//**Warning** Child classes should not implement any functions that Unity automatically calls.
//For example, FixedUpdated, Update, and OnCollision functions.
//This is to prevent behaviors from running code when they are not selected by ProjectileController.




public abstract class ProjectileBehavior : MonoBehaviour {

	//The fixed updated of the behavior
	abstract public void FixedPerform();
	
	//The update of the behavior
	abstract public void Perform();


	//What should the projectile behavior do when it Enters/Stays/Exits collision  
	abstract public void OnImpactEnter( Collision other);
	abstract public void OnImpactStay( Collision other);
	abstract public void OnImpactExit( Collision other);

}
