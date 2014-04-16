using UnityEngine;
using System.Collections;
using System.Collections.Generic;



//Currently does not differentiate between upgrade levels.
//This class uses a dictionary with Tech as keys to the set of prerequisites.
//An empty set is something with no requirement. Therefore if we trace through
//all prereqs of a given tech, we should reach the empty set for every path.
//If this is not true, then we have a set of requirements that form a loop and
//can never be satisfied by the player.
public class TechTree : ScriptableObject {
	
	protected Dictionary<Tech, PreReqSet> PreReqs;



	public static TechTree InitTree(){
		TechTree newTree = ScriptableObject.CreateInstance<TechTree>();
		newTree.PreReqs = new Dictionary<Tech, PreReqSet>();

		return newTree;
	}


	//Returns the set of Prerequisites of theTech
	//Ensures that our dictionary always has a value for our key
	//so we don't need to explicitly check if a value is null everywhere
	public PreReqSet GetReq( Tech theTech){
		if ( !PreReqs.ContainsKey( theTech)) PreReqs[theTech] = new PreReqSet();
		return PreReqs[theTech];
	}




	//helper function for HasLoop
	protected bool HasLoopRecur ( Tech startingTech, Tech theTech){
		if (startingTech == theTech) return true;

		PreReqSet SetOfPreReqs = GetReq(theTech);
		foreach (Tech PreReq in SetOfPreReqs)
			if (HasLoopRecur ( startingTech, PreReq) ) return true;

		return false;
	}

	//checks whether there is a loop of requirements
	protected bool HasLoop( Tech theTech){

		PreReqSet SetOfPreReqs = GetReq(theTech);
		foreach (Tech PreReq in SetOfPreReqs)
			if (HasLoopRecur ( theTech, PreReq) ) return true;

		return false;
	}



	//removes the thePreReq off of theTech
	public void RmReq( Tech theTech, Tech thePreReq){
		GetReq(theTech).Remove(thePreReq);
	}



	//Adds thePreReq onto theTech
	//You are not allowed to add a prereq that creates a loop back to itself.
	//For example, A req B, B req C, and C req A, which is impossible for the player to satisfy.
	public void AddReq( Tech theTech, Tech thePreReq){
		GetReq(theTech).Add(thePreReq);
		if ( HasLoop(theTech)){
			string errMsg = string.Format("Requirement loop detected when setting {0} with {1} as prereq. The prereq was not set.", theTech, thePreReq);
			Debug.LogError( errMsg);
			RmReq( theTech, thePreReq);
		}

	}



	//the default techtree we want for our game
	public static TechTree MakeDefault(){
		
		TechTree defaultTree = InitTree();



		//================ Set our prereqs here ================
		//Some  Prereqs disabled since we can't build everything yet

		//Ballistics
		defaultTree.AddReq( Tech.ballistics, Tech.generator);
		defaultTree.AddReq( Tech.clipSize, Tech.ballistics);
		defaultTree.AddReq( Tech.scatter, Tech.ballistics);
		defaultTree.AddReq( Tech.reloadSpeed, Tech.ballistics);



		//fortification
		//defaultTree.AddReq( Tech.fortification, Tech.generator);
		//defaultTree.AddReq( Tech.bulletAbsorber, Tech.fortification);
		//defaultTree.AddReq( Tech.buildingHp, Tech.fortification);
		//defaultTree.AddReq( Tech.spotlightResearch, Tech.fortification);
		//defaultTree.AddReq( Tech.turretResearch, Tech.fortification);


		//Generator has no Prereq


		//Genome
		//defaultTree.AddReq( Tech.genome, Tech.medbay);
		//defaultTree.AddReq( Tech.mindControl, Tech.genome);
		//defaultTree.AddReq( Tech.moveSpeed, Tech.genome);
		//defaultTree.AddReq( Tech.playerHp, Tech.genome);
		//defaultTree.AddReq( Tech.stealth, Tech.genome);


		//Incendiary
		defaultTree.AddReq( Tech.incendiary, Tech.ballistics);
		defaultTree.AddReq( Tech.searing, Tech.incendiary);
		defaultTree.AddReq( Tech.lightGrenade, Tech.incendiary);



		//Medbay
		defaultTree.AddReq( Tech.medbay, Tech.generator);


		//Photon
		//defaultTree.AddReq( Tech.photon, Tech.fortification);
		defaultTree.AddReq( Tech.orbshot, Tech.photon);
		defaultTree.AddReq( Tech.lightPath, Tech.photon);


		//Refraction
		//defaultTree.AddReq( Tech.refraction, Tech.generator);
		//defaultTree.AddReq( Tech.seeker, Tech.refraction);
		//defaultTree.AddReq( Tech.buildingLightRange, Tech.refraction);
		//defaultTree.AddReq( Tech.lightFist, Tech.refraction);



		//Spotlight
		//defaultTree.AddReq( Tech.spotlight, Tech.refraction);
		//defaultTree.AddReq( Tech.spotlight, Tech.spotlightResearch);


		//Turret
		//defaultTree.AddReq( Tech.turret, Tech.turretResearch);


		//Wall has no Prereq
		

		return defaultTree;
	}



}





//A set of Techs used to store the set of prerequisites needed for some Tech
public class PreReqSet : HashSet<Tech> {
	
	public bool IsEmpty(){
		return base.Count == 0;
	}
	
}