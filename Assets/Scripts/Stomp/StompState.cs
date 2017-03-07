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

    public void StartState()
    {
        Debug.Log("Stomp Start");
        enemy.currentState = enemy.stompState;
        enemy.ResetBody();
        isCreateDirt = false;

        attackTarget = new Vector3(enemy.player.transform.position.x,
                                //enemy.player.transform.position.y - 0.7f,
                                0.1f,
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
        enemy.animationManager.PlayChargeStompAnim();
    }

    public void UpdateState()
    {
        Stomp();
    }

    public void EndState()
    {
        Debug.Log("Stomp End");
        enemy.prepareDigStrikeState.StartState();
    }

    public void StateChangeCondition()
    {

    }

    void Stomp(){
        if(enemy.animationManager.CheckBodyAnimState(0 ,"NoAnimation")){
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
                    EndState();
                }
            }
        }
        else if(enemy.animationManager.CheckBodyAnimState(0, "ChargeStomp")){
           enemy.animationManager.StopSeekAnim();
        }
    }
}
