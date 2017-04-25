using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParryAIState : AIState {
	private readonly StatePatternAI enemy;
	public string name{ get;}
	private float timeCount;
	public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}

	public ParryAIState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
		name = "parryAIState";
		timeCount = 0;
		choice = new List<AIState>();
	}

	public void StartState(){
//		enemy.currentState = enemy.parryState;
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
