using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrepareSlashingAIState : AIState {
	private readonly AITest AI;
	public string name{ get;}
	private float speed;
	private Vector3 randomVector;
	private int subState;
	public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}

	public PrepareSlashingAIState(AITest statePatternAI){
		AI = statePatternAI;
		choice = new List<AIState>();
	}

	public void StartState(){
		AI.currentState = AI.prepareSlashingAIState;
		speed = 200;
		//		AI.currentState = AI.stopState;
		//		AI.GetComponent<Rigidbody>().isKinematic = true;

		//		foreach(GameObject sc in AI.swordController){
		////			sc.transform.GetChild (0).LookAt (sc.transform.position);
		//			GameObject sword = sc.transform.GetChild (0);
		//			sword.transform.rotation = Quaternion.RotateTowards(sword.transform.rotation, sword.transform.LookAt(sc.transform.position), speed);
		//
		////			sc.AddComponent<FixedJoint>();
		////			sc.GetComponent<FixedJoint>().connectedBody = sc.transform.GetChild(0).GetComponent<Rigidbody>();
		//
		////			sc.transform.Rotate (0f, speed * Time.deltaTime, 0f);
		//		}

		randomVector = new Vector3 (Random.Range(-5f,5f), Random.Range(0f,5f), Random.Range(2f,5f));
		subState = 0;
	}

	public void UpdateState(){
		foreach(GameObject sc in AI.swordController){
			//			sc.transform.GetChild (0).LookAt (sc.transform.position);
			//			GameObject sword = sc.transform.GetChild (0);
			//			sc.transform.GetChild (0).transform.rotation = Quaternion.RotateTowards(sc.transform.GetChild (0).transform.rotation, sc.transform.GetChild (0).transform.LookAt(sc.transform.position), speed);

			//			sc.AddComponent<FixedJoint>();
			//			sc.GetComponent<FixedJoint>().connectedBody = sc.transform.GetChild(0).GetComponent<Rigidbody>();

			//			sc.transform.Rotate (0f, speed * Time.deltaTime, 0f);
			if(Mathf.Abs(sc.transform.eulerAngles.y) > 170f){
				subState = 1;
			}
			if (subState == 0) {
				Debug.Log ("state     0");
				sc.transform.Rotate (0f, speed * Time.deltaTime, 0f);
				//				sc.GetComponent<Rigidbody> ().AddTorque (-AI.transform.up * speed*Time.deltaTime);
			}else{
				Debug.Log ("state     1");
				Transform sword = sc.transform.GetChild (0);
				Vector3 relativePos = sc.transform.position - sword.transform.position;
				Quaternion rotation = Quaternion.LookRotation(relativePos);
				sword.rotation = Quaternion.RotateTowards(sword.rotation, rotation, speed*Time.deltaTime);

				float distance = Vector3.Distance (AI.transform.position, AI.player.transform.position);
				sword.localPosition = Vector3.MoveTowards (sword.localPosition, randomVector.normalized*distance, speed/50f*Time.deltaTime);
				//			sword.loo
			}
		}
	}

	public void EndState(){
		AI.GetComponent<Rigidbody>().isKinematic = false;
	}

	public void StateChangeCondition(){

	}
}
