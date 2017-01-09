using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigStrikeState : AIState {
    private readonly StatePatternAI enemy;
    private Vector3 attackTarget;
    private Vector3 moveToTarget;
    private float speed;
    private float timer;
    private float seekTime;

    public DigStrikeState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
	}

    public void StartState()
    {
        Debug.Log("Dig Start");
        enemy.currentState = enemy.digStrikeState;
        attackTarget = enemy.player.transform.position;
        speed = 30f;
        moveToTarget = new Vector3(attackTarget.x,
                                 attackTarget.y + 20f,
                                 attackTarget.z);
        timer = 0f;
        seekTime = Random.Range(3f, 5f);
    }

    public void UpdateState()
    {
        Strike();
    }

    public void EndState()
    {
        Debug.Log("Dig Strike End");
    }

    public void StateChangeCondition()
    {

    }

    void Strike(){
        //Animation sound and effect play;
        if(enemy.transform.position != moveToTarget){
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, moveToTarget, speed * Time.deltaTime);
        }
        else {
            enemy.prepareSlamState.StartState();
        }
    }
}
