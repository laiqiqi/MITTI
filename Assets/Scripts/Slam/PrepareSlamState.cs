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
    private Light circleLight;
	public string name{ get; }
    public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}

    public PrepareSlamState(StatePatternAI statePatternAI){
		AI = statePatternAI;
        choice = new List<AIState>();
	}
    public void StartState()
    {
        Debug.Log("Prepare Slam Start");
        hasCircle = false;
        AI.currentState = AI.prepareSlamState;
        speed = 20f;
        timer = 0f;
        seekTime = 5f;
        AIFrontPos = AI.transform.position + AI.body.transform.forward * 1.05f;
        moveToPos = new Vector3 (AI.transform.position.x, 2f, AI.transform.position.z);

        AI.EditMagnet(1000, 100);
        AI.magnet.GetComponent<ContinuousExplosionForce>().force = -100f;
    }

    public void UpdateState()
    {
        Prepare();
    }

    public void EndState()
    {
        Debug.Log("Prepare Slam End");
        // AI.slamState.StartState(this.attackTarget);
        AI.NextState();
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
                slamCircle.transform.SetParent(AI.body.transform);
                circleLight = slamCircle.transform.GetChild(0).transform.GetChild(0).GetComponent<Light>();
                hasCircle = true;
            }
            else if(timer <= seekTime && !slamCircle.GetComponent<SlamCircle>().isBreak){
                timer += Time.deltaTime;
                if(timer < 3f){
                    // circleLight.range += seekTime*3f*Time.deltaTime;
                }
                else if(timer < 4.5f){
                    // circleLight.range += seekTime*50f*Time.deltaTime;
                }
                else if(timer < 4.975f){
                    // circleLight.range -= seekTime*200f*Time.deltaTime;
                }
                Targeting();
            }
            else if(slamCircle.GetComponent<SlamCircle>().isBreak){
                AI.GetComponent<SphereCollider>().enabled = true;
                AI.body.GetComponent<SphereCollider>().isTrigger = true;
                AI.effectManager.DestroyCircleByName(MagicCircleName.SLAM_CIRCLE);
                AI.effectManager.RemoveCircleFromDictByName(MagicCircleName.SLAM_CIRCLE);
                AI.stunState.StartState(5f);
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
