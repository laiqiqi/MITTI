using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareSlamState : AIState {
    private readonly StatePatternAI AI;
    private Vector3 attackTarget;
    private Vector3 AIFrontPos;
    private Vector3 moveToPos;
    private float speed;
    private float timer;
    private float seekTime;
    private bool hasCircle;
    private GameObject slamCircle;
	public string name{ get; }

    public PrepareSlamState(StatePatternAI statePatternAI){
		AI = statePatternAI;
	}
    public void StartState()
    {
        AI.ResetBody();
        Debug.Log("Prepare Slam Start");
        hasCircle = false;
        AI.currentState = AI.prepareSlamState;
        speed = 20f;
        timer = 0f;
        seekTime = 5f;
        AIFrontPos = AI.transform.position + AI.body.transform.forward * 1.05f;
        moveToPos = new Vector3 (AI.transform.position.x, 2f, AI.transform.position.z);
    }

    public void UpdateState()
    {
        Prepare();
    }

    public void EndState()
    {
        Debug.Log("Prepare Slam End");
        AI.slamState.StartState(this.attackTarget);
    }

    public void StateChangeCondition()
    {

    }

     void Prepare() {
        if(AI.transform.position.y > 2.5f || AI.transform.position.y < 1.9f){
            AI.transform.position = Vector3.MoveTowards(AI.transform.position, moveToPos, (speed/2)*Time.deltaTime);
        }
        else{
            if(!hasCircle){
                AIFrontPos = AI.transform.position + AI.body.transform.forward * 1.05f;
                slamCircle = AI.effectManager.CreateAndReturnCircleByName(MagicCircleName.SLAM_CIRCLE ,AIFrontPos);
                slamCircle.transform.SetParent(AI.transform);
                hasCircle = true;
            }
            if(timer <= seekTime){
                timer += Time.deltaTime;
                if(timer < 3f){
                    slamCircle.transform.GetChild(0).transform.GetChild(0).GetComponent<Light>().range += seekTime*3f*Time.deltaTime;
                }
                else if(timer < 4.5f){
                    slamCircle.transform.GetChild(0).transform.GetChild(0).GetComponent<Light>().range += seekTime*50f*Time.deltaTime;
                }
                else if(timer < 4.975f){
                    slamCircle.transform.GetChild(0).transform.GetChild(0).GetComponent<Light>().range -= seekTime*200f*Time.deltaTime;
                }
                Targeting();
            }
            else{
                EndState();
            }
        }
    }

    void Targeting(){
        // Debug.Log("Targeting");
        attackTarget = AI.player.transform.position + (Vector3.up*0.5f);
        AI.transform.LookAt(attackTarget);
    }
}
