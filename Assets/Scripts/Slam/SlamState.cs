using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamState : MonoBehaviour, AIState {
    private readonly StatePatternAI enemy;
    private Vector3 attackTarget;
    private Vector3 moveToTarget;
    private float speed;
    private bool isStop;
    private GameObject slamCol;
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
        isStop = false;
        speed = 20f;
        this.attackTarget = attackTarget;
        moveToTarget = this.attackTarget + enemy.transform.forward*30f;

        enemy.effectManager.DestroySlamCircle();
        slamCol = enemy.effectManager.CreateSlamCollider(enemy.transform.position + enemy.transform.forward*1.05f);
        slamCol.transform.SetParent(enemy.transform);
    }

    public void UpdateState()
    {
        Slam();
    }

    public void EndState()
    {
        Debug.Log("Slam End");
        enemy.effectManager.DestroySlamCollider();
        Destroy(slamCol);
        enemy.floatingState.StartState();
    }

    public void StateChangeCondition()
    {

    }

    void Slam(){
        if(enemy.transform.position != moveToTarget && !isStop){
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position,
                                                    moveToTarget,
                                                    speed * Time.deltaTime);
        }
        else{
            EndState();
        }
    }
}
