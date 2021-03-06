﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompState : AIState {
    private readonly StatePatternAI AI;
    private Vector3 attackTarget;
    private Vector3 dirtPos;
    private Vector3 attackEndPos;
    private bool isCreateDirt, isPassVertex, isPlayStomping;
	public string name{ get;}
    public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}

    public StompState(StatePatternAI statePatternAI){
		AI = statePatternAI;
        choice = new List<AIState>();
	}

    public void StartState()
    {
        Debug.Log("Stomp Start");
        AI.currentState = AI.stompState;
        isCreateDirt = false;
        isPassVertex = false;
        isPlayStomping = false;

        attackTarget = new Vector3(AI.transform.position.x,
                                //enemy.player.transform.position.y - 0.7f,
                                0.1f,
                                AI.transform.position.z);
        
        attackEndPos = new Vector3(attackTarget.x,
                                attackTarget.y + 5f,
                                attackTarget.z);

        dirtPos = new Vector3(AI.transform.position.x,
                                //enemy.player.transform.position.y - 2f,
                                -1f,
                                AI.transform.position.z);
        AI.speed = 35f;
        if(AI.isRage){
            AI.speed = 45f;
        }

        AI.animationManager.PlayChargeStompAnim();
        AI.effectManager.CreateCircleByName(MagicCircleName.STOMP_CIRCLE ,attackTarget);
    }

    public void UpdateState()
    {
        Stomp();
    }

    public void EndState()
    {
        Debug.Log("Stomp End");
        AI.effectManager.RemoveEffectFromDictByName(EffectName.DIRTBLAST);
        // AI.prepareDigStrikeState.StartState();
        AI.NextState();
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
                    AI.effectManager.DestroyCircleByName(MagicCircleName.STOMP_CIRCLE);
                    AI.effectManager.RemoveCircleFromDictByName(MagicCircleName.STOMP_CIRCLE);
                    AI.effectManager.CreateEffectByName(EffectName.DIRTBLAST ,dirtPos);
                    isCreateDirt = true;
                }
                if(Vector3.Distance(AI.transform.position, attackEndPos) > 0.1f){
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

        if(AI.body.transform.localPosition.y >= 14){
            isPassVertex = true;
        }
        if(isPassVertex) {
            if(AI.body.transform.localPosition.y <= 14){
                if(!isPlayStomping){
                    AI.effectManager.PlaySoundByName(AISoundName.STOPMING_SOUND);
                    isPlayStomping = true;
                }
            }
        }
    }
}
