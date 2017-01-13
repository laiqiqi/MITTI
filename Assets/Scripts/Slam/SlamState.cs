using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamState : AIState {
    private readonly StatePatternAI enemy;
    private Vector3 attackTarget;
    private Vector3 moveToTarget;
    private float speed;
    private GameObject body;
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
        speed = 25f;
        this.attackTarget = attackTarget;
        moveToTarget = this.attackTarget + enemy.transform.forward*15f;
        body = enemy.transform.GetChild(0).gameObject;
    }

    public void UpdateState()
    {
        Slam();
        // Debug.Log(enemy.bodyColInfo);
    }

    public void EndState()
    {
        Debug.Log("Slam End");
        enemy.seekState.StartState();
    }

    public void StateChangeCondition()
    {

    }

    void Slam(){
        if(enemy.transform.position != moveToTarget){
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position,
                                                    moveToTarget,
                                                    speed * Time.deltaTime);
        }
        else{
            EndState();
        }
    }
}
