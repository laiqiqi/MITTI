using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareDigStrikeState : AIState {
    private readonly StatePatternAI AI;
    private Vector3 attackTarget;
    private Vector3 moveToTarget;
    private float speed;
    private float timer;
    private float seekTime;
	public string name{ get; }
    public List<AIState> choice{ get;set; }

    public PrepareDigStrikeState(StatePatternAI statePatternAI){
		AI = statePatternAI;
        choice = new List<AIState>();
	}
    public void StartState()
    {
        Debug.Log("Prepare Dig Start");
        AI.currentState = AI.prepareDigStrikeState;
        attackTarget = AI.player.transform.position;
        speed = 30f;
        moveToTarget = new Vector3(AI.transform.position.x,
                                 AI.transform.position.y-15f,
                                 AI.transform.position.z);
        timer = 0f;
        seekTime = Random.Range(3f, 5f);

        AI.animationManager.StopChargeStompAnim();

        AI.EditMagnet(0, 0);
    }

    public void UpdateState()
    {
        if(AI.transform.position.y > moveToTarget.y){
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
        // AI.digStrikeState.StartState();
        AI.NextState();
    }

    public void StateChangeCondition()
    {

    }

    void Dig(){
        AI.transform.position = Vector3.MoveTowards(AI.transform.position, moveToTarget, speed * Time.deltaTime);
    }

    void Targeting(){
        attackTarget = new Vector3(AI.player.transform.position.x,
                                 AI.player.transform.position.y-17f,
                                 AI.player.transform.position.z);
        AI.transform.position = Vector3.MoveTowards(AI.transform.position, attackTarget, speed * Time.deltaTime);
    }
}
