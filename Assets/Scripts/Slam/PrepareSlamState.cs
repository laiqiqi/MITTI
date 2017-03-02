using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareSlamState : AIState {
    private readonly StatePatternAI enemy;
    private Vector3 attackTarget;
    private Vector3 AIFrontPos;
    private Vector3 moveToPos;
    private float speed;
    private float timer;
    private float seekTime;
    private bool hasCircle;
	public string name{ get; }

    public PrepareSlamState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
	}
    public void StartState()
    {
        enemy.ResetBody();
        Debug.Log("Prepare Slam Start");
        hasCircle = false;
        enemy.currentState = enemy.prepareSlamState;
        speed = 20f;
        timer = 0f;
        seekTime = 5f;
        attackTarget = new Vector3(enemy.player.transform.position.x,
                                    enemy.player.transform.position.y + 1.5f,
                                    enemy.player.transform.position.z);
        AIFrontPos = enemy.transform.position + enemy.body.transform.forward * 1.05f;
        moveToPos = new Vector3 (enemy.transform.position.x, 2f, enemy.transform.position.z);
    }

    public void UpdateState()
    {
        MoveToTarget();
    }

    public void EndState()
    {
        Debug.Log("Prepare Slam End");
        enemy.slamState.StartState(this.attackTarget);
    }

    public void StateChangeCondition()
    {

    }

     void MoveToTarget() {
        if(enemy.transform.position.y > 2.5f){
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, moveToPos, (speed/2)*Time.deltaTime);
        }
        else{
            if(!hasCircle){
                enemy.effectManager.CreateSlamCircle(AIFrontPos);
                hasCircle = true;
            }
            if(timer <= seekTime){
                timer += Time.deltaTime;
                Targeting();
            }
            else{
                //Play animation
                EndState();
            }
        }
    }

    void Targeting(){
        attackTarget = new Vector3(enemy.player.transform.position.x,
                                   enemy.player.transform.position.y + 1.2f,
                                   enemy.player.transform.position.z);
        enemy.transform.LookAt(attackTarget);

        AIFrontPos = enemy.transform.position + enemy.body.transform.forward*1.05f;
        enemy.effectManager.UpdatePosSlamCircle(AIFrontPos, attackTarget);
    }
}
