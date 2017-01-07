using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareDigStrikeState : AIState {
    private readonly StatePatternAI enemy;
    private Vector3 attackTarget;
    private Vector3 moveToTarget;
    private float speed;
    private float timer;
    private float seekTime;

    public PrepareDigStrikeState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
	}
    public void StartState()
    {
        Debug.Log("Prepare Dig Start");
        enemy.currentState = enemy.prepareDigStrikeState;
        attackTarget = enemy.player.transform.position;
        speed = 30f;
        moveToTarget = new Vector3(enemy.transform.position.x,
                                 enemy.transform.position.y-10f,
                                 enemy.transform.position.z);
        timer = 0f;
        seekTime = Random.Range(3f, 5f);
    }

    public void UpdateState()
    {
        if(enemy.transform.position.y > moveToTarget.y){
            Dig();
        }
        else{
            if(timer <= seekTime){
                timer += Time.deltaTime;
                Targeting();
            }
            else {
                EndState();
            }
        }
    }

    public void EndState()
    {
        Debug.Log("Prepare Dig End");
        enemy.digStrikeState.StartState();
    }

    public void StateChangeCondition()
    {

    }

    void Dig(){
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, moveToTarget, speed * Time.deltaTime);
    }

    void Targeting(){
        attackTarget = new Vector3(enemy.player.transform.position.x,
                                 enemy.player.transform.position.y-10f,
                                 enemy.player.transform.position.z);
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, attackTarget, speed * Time.deltaTime);
    }
}
