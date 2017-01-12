using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompState : AIState {
    private readonly StatePatternAI enemy;
    private Vector3 attackTarget;
    private float speed;
	public string name{ get;}

    public StompState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
	}
    public StompState(StatePatternAI statePatternAI, Vector3 target){
		enemy = statePatternAI;
        attackTarget = target;
	}

    public void StartState()
    {
        Debug.Log("Stomp Start");
        enemy.currentState = enemy.stompState;
        attackTarget = enemy.player.transform.position;
        speed = 30f;
    }

    public void UpdateState()
    {
        Stomp();
    }

    public void EndState()
    {
        Debug.Log("Stomp End");
    }

    public void StateChangeCondition()
    {

    }

    void Stomp(){
        //Play Stomp animation
        if(enemy.transform.position != attackTarget){
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, attackTarget, speed * Time.deltaTime);
        }
        else{
            enemy.prepareDigStrikeState.StartState();
        }
    }
}
