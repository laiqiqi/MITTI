using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigStrikeState : MonoBehaviour, AIState {
    private readonly StatePatternAI enemy;
    private Vector3 attackTarget;
    private Vector3 moveToTarget;
    private float speed;
    private float timer;
    private float seekTime;
    private GameObject circle;
	public string name{ get;}

    public DigStrikeState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
		name = "s";
	}

    public void StartState()
    {
        Debug.Log("Dig Start");
        enemy.currentState = enemy.digStrikeState;
        attackTarget = attackTarget = new Vector3(enemy.player.transform.position.x,
                                enemy.player.transform.position.y - 0.7f,
                                enemy.player.transform.position.z);
        moveToTarget = new Vector3(attackTarget.x,
                                 attackTarget.y + 20f,
                                 attackTarget.z);
        speed = 30f;
        timer = 0f;
        seekTime = Random.Range(3f, 5f);

        // circle = Instantiate(enemy.magicCircle[1], attackTarget, Quaternion.identity);
        enemy.effectManager.CreateDigStrikeCircle(attackTarget);
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
        //Animation sound and effect play;
        if(enemy.transform.position != moveToTarget){
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, moveToTarget, speed * Time.deltaTime);
        }
        else {
            enemy.prepareSlamState.StartState();
        }
    }
}
