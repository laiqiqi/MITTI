using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StopState : AIState {
	private readonly StatePatternAI enemy;
	public string name{ get;}
	public List<AIState> choice{ get;set; }

	public StopState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
		choice = new List<AIState>();
	}

	public void StartState(){
		enemy.currentState = enemy.stopState;
		enemy.GetComponent<Rigidbody>().isKinematic = true;
	}

	public void UpdateState(){

	}

	public void EndState(){
		enemy.GetComponent<Rigidbody>().isKinematic = false;
	}

	public void StateChangeCondition(){

	}
}
