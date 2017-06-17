using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorArrowState : AIState {

	public string name{ get; }
    private readonly StatePatternAI AI;
    private float stunTime, counter;
    public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}

	public TutorArrowState(StatePatternAI AI){
        this.AI = AI;
        choice = new List<AIState>();
    }

	// Use this for initialization
	public void StartState()
    {
       
    }

	public void StateChangeCondition()
    {
        
    }

    public void UpdateState()
    {
       
    }

	public void EndState()
    {

	}
}
