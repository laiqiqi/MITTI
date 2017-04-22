using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigStrikeState : AIState {
    private readonly StatePatternAI AI;
    private Vector3 attackTarget;
    private Vector3 moveToTarget;
    private float speed;
    private float timer;
    private float seekTime;
    private GameObject circle;
    private bool hasRSSummoner;
	public string name{ get;}
    public List<AIState> choice{ get;set; }

    public DigStrikeState(StatePatternAI statePatternAI){
		AI = statePatternAI;
        choice = new List<AIState>();
	}

    public void StartState()
    {
        Debug.Log("Dig Start");
        AI.animationManager.PlayChargeDigAnim();
        hasRSSummoner = false;
        AI.currentState = AI.digStrikeState;
        attackTarget = new Vector3(AI.player.transform.position.x,
                                //AI.player.transform.position.y - 0.7f,
                                0.1f,
                                AI.player.transform.position.z);
       
        moveToTarget = new Vector3(attackTarget.x,
                                 attackTarget.y + 3f,
                                 attackTarget.z);
        speed = 25f;
        timer = 0f;
        seekTime = Random.Range(3f, 5f);

        AI.effectManager.CreateCircleByName(MagicCircleName.DIG_CIRCLE ,attackTarget);

        AI.EditMagnet(1000, 100);
        AI.magnet.GetComponent<ContinuousExplosionForce>().force = -100f;
    }

    public void UpdateState()
    {
        Strike();
    }

    public void EndState()
    {
        Debug.Log("Dig Strike End");
        AI.NextState();
    }

    public void StateChangeCondition()
    {

    }

    void Strike(){
        if(AI.animationManager.CheckBodyAnimState(0, "DigStrike")) {
            AI.animationManager.StopChargeDigAnim();
            AI.animationManager.PlayDigStrikeAnim();

            if(AI.transform.position != moveToTarget){
                AI.transform.position = Vector3.MoveTowards(AI.transform.position, moveToTarget, speed * Time.deltaTime);
            }
            else{                
                if(!hasRSSummoner){
                    AI.effectManager.CreateEffectByName(EffectName.ROCKSPIKE ,attackTarget);
                    hasRSSummoner = true;
                }
            }
        }
        else if(AI.animationManager.CheckBodyAnimState(0, "NoAnimation") && hasRSSummoner){
            AI.effectManager.DestroyCircleByName(MagicCircleName.DIG_CIRCLE);
            AI.effectManager.RemoveCircleFromDictByName(MagicCircleName.DIG_CIRCLE);
            AI.effectManager.DestroyEffectByName(EffectName.ROCKSPIKE);
            AI.effectManager.RemoveEffectFromDictByName(EffectName.ROCKSPIKE);
            AI.animationManager.StopDigStrikeAnim();

            EndState();
        }
    }
}
