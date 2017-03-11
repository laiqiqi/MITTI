using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SlamState : MonoBehaviour, AIState {
    private readonly StatePatternAI AI;
    private Vector3 attackTarget;
    private Vector3 moveToTarget;
    public bool isStop;
    public bool isStun;
    private GameObject slamCol;
	public string name{ get;}

    public SlamState(StatePatternAI statePatternAI){
		AI = statePatternAI;
	}
    public void StartState()
    {
        
    }
    public void StartState(Vector3 attackTarget)
    {
        Debug.Log("Slam Start");
        AI.currentState = AI.slamState;
        isStop = false;
        isStun = false;
        AI.speed = 50f;
        this.attackTarget = attackTarget;
        moveToTarget = this.attackTarget + AI.transform.forward*30f;

        AI.effectManager.DestroySlamCircle();
        slamCol = AI.effectManager.CreateSlamCollider(AI.transform.position + AI.transform.forward*1.05f);
        slamCol.transform.SetParent(AI.transform);
    }

    public void UpdateState()
    {
        Slam();
    }

    public void EndState()
    {
        Debug.Log("Slam End");
        AI.effectManager.DestroySlamCollider();
        Destroy(slamCol);
        // AI.seekState.StartState();
        if(Player.instance.GetComponent<PlayerStat>().isHitSlam == true){
            Player.instance.GetComponent<Rigidbody>().AddForce(AI.transform.forward*10f, ForceMode.Impulse);
        }
        Player.instance.GetComponent<PlayerStat>().isHitSlam = false;
        Player.instance.transform.SetParent(null);
        AI.prepareSlamState.StartState();
    }

    public void StateChangeCondition()
    {

    }

    void Slam(){
        if(isStun){
            Destroy(slamCol);
            // Player.instance.GetComponent<PlayerControl>().isHitSlam = false;
            AI.stunState.StartState(5f, AI.transform.forward*AI.speed*2);
        }
        else if(AI.transform.position != moveToTarget && !isStop){
            Debug.Log("Moveeeee");
            AI.transform.position = Vector3.MoveTowards(AI.transform.position,
                                                    moveToTarget,
                                                    AI.speed * Time.deltaTime);
                                                    // (speed += (speed/1.25f*Time.deltaTime)) * Time.deltaTime);
        }
        else{
            EndState();
        }
    }
}
