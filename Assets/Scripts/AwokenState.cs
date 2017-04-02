using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwokenState : AIState {
	private readonly StatePatternAI AI;
	private Vector3 upPos;
	public List<AIState> choice;
	public string name{ get; }

	public AwokenState(StatePatternAI statePatternAI){
		AI = statePatternAI;
		choice = new List<AIState>();
	}

	public void StartState()
    {
		Debug.Log("Awoken Start");
		AI.speed = 5f;
		upPos = AI.transform.position + (Vector3.up * 15f);
		AI.currentState = AI.awokenState;
	}

	public void UpdateState()
    {
		FloatUp();
    }

	 public void EndState()
    {
        Debug.Log("Awoken End");
		AI.NextState();
    }

    public void StateChangeCondition()
    {
		
    }

	void FloatUp()
    {
		if(Vector3.Distance(AI.transform.position, upPos) > 0.1f){
			AI.transform.position = Vector3.MoveTowards(AI.transform.position, upPos, AI.speed * Time.deltaTime);
		}
        else{
			EndState();
		}
    }
}
