using UnityEngine;
using System.Collections;

public class isPoisonBall : MonoBehaviour {
	
		private float timer;
		private float timerCD;
		private Rigidbody clone;
		public bool popped;

		//public Rigidbody thePlant;
		//public isPoisonPlant plantScript;
		public int cloudCount;
		public Rigidbody poisonCloud;
		
	
		// Use this for initialization
		void Start () {
			popped = false;	
			timer = 0.0f;
			timerCD = 3.0f;
			cloudCount = 3;
		}
		
			// Update is called once per frame
		void Update () {
			timer += Time.deltaTime;
				if(timer > timerCD){
						timer = 0.0f;
						popped = true;
						popSphere();
					
					
						
				}
			
		}
		public void popSphere(){
		//plantScript.isActive = false;


				for(int i = 0; i <= cloudCount; i++){
			Vector3 temp = transform.position;
			temp.y -= 5;
						clone = Instantiate(poisonCloud, temp, Quaternion.identity) as Rigidbody;
						clone.GetComponent<isPoisonCloud>().beginLifetime();
				}
				
				
			

				
		}

		public void destroySelf(){

		Destroy (gameObject);
		}
	}