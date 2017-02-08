using UnityEngine;
using System.Collections;

public class ParryAIState : AIState {
	private readonly StatePatternAI enemy;
	public string name{ get;}
	private float timeCount;


	public ParryAIState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
		name = "parryAIState";
		timeCount = 0;
	}

	public void StartState(){
		enemy.currentState = enemy.parryState;
		timeCount = 0;
	}

	public void UpdateState(){
//		StartCoroutine(ChangeToFloating());
		timeCount += Time.deltaTime;
		if (timeCount > 2.5f) {
			enemy.escapeState.StartState ();
		}

	}

	public void EndState(){

	}

	public void StateChangeCondition(){
		
	}

//	public IEnumerator ChangeToFloating() {
//		Debug.Log ("Start");
//		yield return new WaitForSeconds(5);
//		Debug.Log ("Test");
//		enemy.floatingState.StartState ();
//	}
}
