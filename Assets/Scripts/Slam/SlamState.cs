using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamState : AIState {
    private readonly StatePatternAI enemy;
    private Vector3 attackTarget;
    private Vector3 moveToTarget;
    private float speed;
    private bool isStop;
	public string name{ get;}

    public SlamState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
	}
    public void StartState()
    {
        
    }
    public void StartState(Vector3 attackTarget)
    {
        Debug.Log("Slam Start");
        enemy.currentState = enemy.slamState;
        isStop = false;
        speed = 20f;
        this.attackTarget = attackTarget;
        moveToTarget = this.attackTarget + enemy.transform.forward*30f;

        enemy.effectManager.DestroySlamCircle();
    }

    public void UpdateState()
    {
        Slam();
    }

    public void EndState()
    {
        Debug.Log("Slam End");
        // enemy.floatingState.StartState();
        enemy.seekState.StartState();
    }

    public void StateChangeCondition()
    {

    }

    void Slam(){
        if(enemy.transform.position != moveToTarget && !isStop){
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position,
                                                    moveToTarget,
                                                    speed * Time.deltaTime);
        }
        else{
            EndState();
        }
    }
}
