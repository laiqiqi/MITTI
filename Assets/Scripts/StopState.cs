using UnityEngine;
using System.Collections;

public class StopState : AIState {
	private readonly StatePatternAI enemy;
	public string name{ get;}

	public StopState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
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
