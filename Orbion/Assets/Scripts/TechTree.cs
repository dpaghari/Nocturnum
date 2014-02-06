using UnityEngine;
using System.Collections;
using System.Collections.Generic;



//Currently only supports one prerequisite for any given tech. Also does not differentiate between upgrade levels.
//This class uses a dictionary to represent our Tech Prerequisites for a given tech.
//The key represents a tech and the value represents the prereq for that tech.
//Tech.none represents having no requirement and is the root of our tech tree.
//This means if we trace any given tech through the PreReqs, we will eventually find Tech.none.
//If this is not true, then we have a set of requirements that can never be satisfied by the player.
public class TechTree : ScriptableObject {
	
	protected Dictionary<Tech, Tech> PreReqs;



	public static TechTree InitTree(){
		TechTree newTree = ScriptableObject.CreateInstance<TechTree>();
		newTree.PreReqs = new Dictionary<Tech, Tech>();

		//only tech that should be it's own prereq is our root: none.	
		newTree.PreReqs[Tech.none] = Tech.none; 

		return newTree;
	}



	//ensures that our dictionary always has a value for our key
	public Tech GetReq( Tech theTech){
		if ( !PreReqs.ContainsKey( theTech)) PreReqs[theTech] = Tech.none;
		return PreReqs[theTech];
	}




	//helper function for HasLoop
	protected bool HasLoopRecur ( Tech startingTech, Tech theTech){
		if ( GetReq( theTech) == startingTech) return true;
		if ( GetReq( theTech) == Tech.none) return false;
		return HasLoopRecur ( startingTech, GetReq( theTech));
	}

	//checks whether there is a loop
	protected bool HasLoop( Tech theTech){
		return HasLoopRecur ( theTech, theTech);
	}



	//removes theTech's prereq
	public void RmReq( Tech theTech){
		PreReqs.Remove( theTech);
	}



	//Sets theTech's prereq as TheReq. Will overwrite any previous prereq.
	//You are not allowed to add a prereq that creates a loop back to itself.
	//For example, A req B, B req C, and C req A, which is impossible for the player to satisfy.
	public void SetReq( Tech theTech, Tech theReq){
		//PreReqs.Add(theTech, theReq);
		PreReqs[theTech] = theReq;
		if ( HasLoop(theTech)){
			string errMsg = string.Format("Requirement loop detected when setting {0} with preq {1}. The prereq was not set.", theTech, theReq);
			Debug.LogError( errMsg);
			RmReq( theTech);
		}

	}



	//the default techtree we want for our game
	public static TechTree MakeDefault(){
		
		TechTree defaultTree = InitTree();

		//set our prereqs here
		defaultTree.SetReq( Tech.scatter, Tech.ballistics);

		return defaultTree;
	}



}
