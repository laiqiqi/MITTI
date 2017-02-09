using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareSlamState : AIState {
    private readonly StatePatternAI enemy;
    private Vector3 attackTarget;
    private float speed;
    private float timer;
    private float seekTime;
	public string name{ get; }

    public PrepareSlamState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
	}
    public void StartState()
    {
        enemy.ResetBody();
        Debug.Log("Prepare Slam Start");
        enemy.currentState = enemy.prepareSlamState;
        speed = 20f;
        timer = 0f;
        seekTime = Random.Range(3f, 5f);
        attackTarget = enemy.player.transform.position;
    }

    public void UpdateState()
    {
        if(timer <= seekTime){
            timer += Time.deltaTime;
            Targeting();
        }
        else{
            //Play animation
            EndState();
        }
    }

    public void EndState()
    {
        Debug.Log("Prepare Slam End");
        enemy.slamState.StartState(this.attackTarget);
    }

    public void StateChangeCondition()
    {

    }

    void Targeting(){
        attackTarget = enemy.player.transform.position;
        enemy.transform.LookAt(attackTarget);
    }
}
