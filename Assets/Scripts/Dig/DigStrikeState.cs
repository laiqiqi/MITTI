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
    private GameObject circle;
    private bool hasRSSummoner;
	public string name{ get;}
    public List<AIState> choice{ get;set; }

    public DigStrikeState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
        choice = new List<AIState>();
	}

    public void StartState()
    {
        Debug.Log("Dig Start");
        enemy.animationManager.PlayChargeDigAnim();
        hasRSSummoner = false;
        enemy.currentState = enemy.digStrikeState;
        attackTarget = new Vector3(enemy.player.transform.position.x,
                                //enemy.player.transform.position.y - 0.7f,
                                0.1f,
                                enemy.player.transform.position.z);
       
        moveToTarget = new Vector3(attackTarget.x,
                                 attackTarget.y + 3f,
                                 attackTarget.z);
        speed = 25f;
        timer = 0f;
        seekTime = Random.Range(3f, 5f);

        enemy.effectManager.CreateCircleByName(MagicCircleName.DIG_CIRCLE ,attackTarget);
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
        if(enemy.animationManager.CheckBodyAnimState(0, "DigStrike")) {
            enemy.animationManager.StopChargeDigAnim();
            enemy.animationManager.PlayDigStrikeAnim();

            if(enemy.transform.position != moveToTarget){
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, moveToTarget, speed * Time.deltaTime);
            }
            else{                
                if(!hasRSSummoner){
                    enemy.effectManager.CreateEffectByName(EffectName.ROCKSPIKE ,attackTarget);
                    hasRSSummoner = true;
                }
            }
        }
        else if(enemy.animationManager.CheckBodyAnimState(0, "NoAnimation") && hasRSSummoner){
            enemy.effectManager.DestroyCircleByName(MagicCircleName.DIG_CIRCLE);
            enemy.effectManager.RemoveCircleFromDictByName(MagicCircleName.DIG_CIRCLE);
            enemy.effectManager.DestroyEffectByName(EffectName.ROCKSPIKE);
            enemy.effectManager.RemoveEffectFromDictByName(EffectName.ROCKSPIKE);
            enemy.animationManager.StopDigStrikeAnim();

            enemy.prepareSlamState.StartState();
        }
    }
}
