using UnityEngine;
using System.Collections;

public class SlashAIState : AIState {
	private readonly StatePatternAI enemy;

	public SlashAIState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
	}

	public void StartState(){
		enemy.currentState = enemy.slashState;
	}

	public void UpdateState(){
		Transform swordTransform = enemy.transform.FindChild("Sword");
		swordTransform.transform.localPosition = Vector3.Lerp(swordTransform.localPosition, new Vector3(2, 0, 2), 0.1f);
//		StateChangeCondition ();
	}

	public void EndState(){

	}

	public void StateChangeCondition(){
		enemy.floatingState.StartState ();
	}
}
