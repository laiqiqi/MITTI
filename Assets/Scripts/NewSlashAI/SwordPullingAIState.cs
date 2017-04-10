using UnityEngine;
using System.Collections;

public class SwordPullingAIState : AIState {
	private readonly AITest AI;
	public string name{ get;}
	private float speed;

	public SwordPullingAIState(AITest statePatternAI){
		AI = statePatternAI;
	}

	public void StartState(){
		AI.currentState = AI.swordPullingAIState;
		speed = 10;
//		AI.currentState = AI.stopState;
		//		AI.GetComponent<Rigidbody>().isKinematic = true;
	}

	public void UpdateState(){
//		foreach(GameObject sc in AI.swordController){
//			sc.transform.Rotate (0f, speed * Time.deltaTime, 0f);	
//		}
	}

	public void EndState(){
//		AI.GetComponent<Rigidbody>().isKinematic = false;
	}

	public void StateChangeCondition(){

	}
}
