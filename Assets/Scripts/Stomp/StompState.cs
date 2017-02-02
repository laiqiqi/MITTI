using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompState : MonoBehaviour, AIState {
    private readonly StatePatternAI enemy;
    private Vector3 attackTarget;
    private Vector3 dirtPos;
    private Vector3 attackEndPos;
    private float speed;
    private bool isCreateDirt;
	public string name{ get;}

    public StompState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
	}
    public StompState(StatePatternAI statePatternAI, Vector3 target){
		enemy = statePatternAI;
        attackTarget = target;
	}

    public void StartState()
    {
        Debug.Log("Stomp Start");
        enemy.currentState = enemy.stompState;
        isCreateDirt = false;
        attackTarget = new Vector3(enemy.player.transform.position.x,
                                //enemy.player.transform.position.y - 0.7f,
                                0.3f,
                                enemy.player.transform.position.z);
        
        attackEndPos = new Vector3(attackTarget.x,
                                attackTarget.y + 5,
                                attackTarget.z);

        dirtPos = new Vector3(enemy.player.transform.position.x,
                                //enemy.player.transform.position.y - 2f,
                                -1f,
                                enemy.player.transform.position.z);
        speed = 35f;

        enemy.effectManager.CreateStompCircle(attackTarget);
        enemy.effectManager.PlayChargeStompAnim();
    }

    public void UpdateState()
    {
        Stomp();
    }

    public void EndState()
    {
        Debug.Log("Stomp End");
    }

    public void StateChangeCondition()
    {

    }

    void Stomp(){
        //Play Stomp animation
        if(enemy.effectManager.CheckBodyAnimState(0 ,"NoAnimation")){
            enemy.ResetBody();
            if(enemy.transform.position.y > attackTarget.y && !isCreateDirt){
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, attackTarget, speed * Time.deltaTime);
            }
            else{
                if(!isCreateDirt){
                    enemy.effectManager.DestroyStompCircle();
                    enemy.effectManager.CreateDirtBlast(dirtPos);
                    isCreateDirt = true;
                }
                
                if(enemy.transform.position.y < attackEndPos.y){
                    enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, attackEndPos, 1.5f * Time.deltaTime);
                }
                else{
                    isCreateDirt = false;
                    enemy.prepareDigStrikeState.StartState();
                }
            }
        }
        else if(enemy.effectManager.CheckBodyAnimState(0, "ChargeStomp")){
           enemy.effectManager.StopSeekAnim();
        }
    }
}
