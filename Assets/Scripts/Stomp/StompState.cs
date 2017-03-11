using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompState : MonoBehaviour, AIState {
    private readonly StatePatternAI AI;
    private Vector3 attackTarget;
    private Vector3 dirtPos;
    private Vector3 attackEndPos;
    private bool isCreateDirt;
	public string name{ get;}

    public StompState(StatePatternAI statePatternAI){
		AI = statePatternAI;
	}

    public void StartState()
    {
        Debug.Log("Stomp Start");
        AI.currentState = AI.stompState;
        AI.ResetBody();
        isCreateDirt = false;

        attackTarget = new Vector3(AI.transform.position.x,
                                //enemy.player.transform.position.y - 0.7f,
                                0.1f,
                                AI.transform.position.z);
        
        attackEndPos = new Vector3(attackTarget.x,
                                attackTarget.y + 5,
                                attackTarget.z);

        dirtPos = new Vector3(AI.transform.position.x,
                                //enemy.player.transform.position.y - 2f,
                                -1f,
                                AI.transform.position.z);
        AI.speed = 35f;

        AI.animationManager.PlayChargeStompAnim();
        AI.effectManager.CreateStompCircle(attackTarget);
    }

    public void UpdateState()
    {
        Stomp();
    }

    public void EndState()
    {
        Debug.Log("Stomp End");
        AI.prepareDigStrikeState.StartState();
        // AI.seekState.StartState();
    }

    public void StateChangeCondition()
    {

    }

    void Stomp(){
        if(AI.animationManager.CheckBodyAnimState(0 ,"NoAnimation")){
            AI.animationManager.StopChargeStompAnim();
            if(AI.transform.position.y > attackTarget.y && !isCreateDirt){
                AI.transform.position = Vector3.MoveTowards(AI.transform.position, attackTarget, AI.speed * Time.deltaTime);
            }
            else{
                if(!isCreateDirt){
                    AI.effectManager.DestroyStompCircle();
                    AI.effectManager.CreateDirtBlast(dirtPos);
                    isCreateDirt = true;
                }
                
                if(AI.transform.position.y < attackEndPos.y){
                    AI.transform.position = Vector3.MoveTowards(AI.transform.position, attackEndPos, 1.5f * Time.deltaTime);
                }
                else{
                    isCreateDirt = false;
                    EndState();
                }
            }
        }
        else if(AI.animationManager.CheckBodyAnimState(0, "ChargeStomp")){
           AI.animationManager.StopSeekAnim();
        }
    }
}
