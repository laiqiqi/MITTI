using UnityEngine;
using System.Collections;

public class ShootAIState : AIState {
	private readonly StatePatternAI enemy;

	public ShootAIState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
	}

	public void StartState(){
		enemy.currentState = enemy.shootState;
	}

	public void UpdateState(){
		GameObject.Instantiate (enemy.bullet.gameObject, enemy.transform.position, enemy.transform.rotation);
		StateChangeCondition ();
	}

	public void EndState(){
		
	}

	public void StateChangeCondition(){
		enemy.floatingState.StartState ();
	}
}
