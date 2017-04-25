using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SlamState : AIState {
    private readonly StatePatternAI AI;
    private Vector3 attackTarget;
    private Vector3 moveToTarget;
    public bool isStop, isStun, isEnd;
    private GameObject slamCol;
	public string name{ get;}
    public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}

    public SlamState(StatePatternAI statePatternAI){
		AI = statePatternAI;
        choice = new List<AIState>();
	}
    public void StartState()
    {
        Debug.Log("Slam Start");
        AI.currentState = AI.slamState;
        isStop = false;
        isStun = false;
        isEnd = false;
        AI.speed = 30f;
        this.attackTarget = AI.player.transform.position + (Vector3.up*0.5f);
        moveToTarget = this.attackTarget + AI.transform.forward*30f;

        AI.effectManager.DestroyCircleByName(MagicCircleName.SLAM_CIRCLE);
        AI.effectManager.RemoveCircleFromDictByName(MagicCircleName.SLAM_CIRCLE);
        slamCol = AI.effectManager.CreateAndReturnEffectByName(EffectName.SLAM_COLLIDER ,AI.transform.position + AI.transform.forward + AI.transform.up*0.05f);
        slamCol.transform.SetParent(AI.transform);
        AI.GetComponent<SphereCollider>().enabled = true;
        AI.body.GetComponent<SphereCollider>().isTrigger = true;

        AI.EditMagnet(1000, 100);
    }
    public void StartState(Vector3 attackTarget)
    {
        Debug.Log("Slam Start");
        AI.currentState = AI.slamState;
        isStop = false;
        isStun = false;
        isEnd = false;
        AI.speed = 30f;
        this.attackTarget = attackTarget;
        moveToTarget = this.attackTarget + AI.transform.forward*30f;

        AI.effectManager.DestroyCircleByName(MagicCircleName.SLAM_CIRCLE);
        AI.effectManager.RemoveCircleFromDictByName(MagicCircleName.SLAM_CIRCLE);
        slamCol = AI.effectManager.CreateAndReturnEffectByName(EffectName.SLAM_COLLIDER ,AI.transform.position + AI.transform.forward + AI.transform.up*0.05f);
        slamCol.transform.SetParent(AI.transform);
        AI.GetComponent<SphereCollider>().enabled = true;
        AI.body.GetComponent<SphereCollider>().isTrigger = true;

        AI.EditMagnet(1000, 100);
    }

    public void UpdateState()
    {
        Slam();
    }

    public void EndState()
    {
        AI.effectManager.DestroyEffectByName(EffectName.SLAM_COLLIDER);
        AI.effectManager.RemoveEffectFromDictByName(EffectName.SLAM_COLLIDER);
        // Destroy(slamCol);
        // if(Player.instance.GetComponent<PlayerStat>().isHitSlam == true){
        //     Player.instance.GetComponent<Rigidbody>().AddForce(AI.transform.forward*10f, ForceMode.Impulse);
        // }
        Player.instance.GetComponent<PlayerStat>().isHitSlam = false;
        Player.instance.GetComponent<Rigidbody>().isKinematic = true;
        Player.instance.transform.SetParent(null);

        AI.GetComponent<SphereCollider>().enabled = false;
        AI.body.GetComponent<SphereCollider>().isTrigger = false;
        
        // AI.prepareSlamState.StartState();
        AI.NextState();
    }

    public void StateChangeCondition()
    {

    }

    void Slam(){
        if(isStun){
            AI.effectManager.DestroyEffectByName(EffectName.SLAM_COLLIDER);
            AI.effectManager.RemoveEffectFromDictByName(EffectName.SLAM_COLLIDER);
            isEnd = true;
            // Destroy(slamCol);
            // Player.instance.GetComponent<PlayerControl>().isHitSlam = false;
            // AI.GetComponent<Rigidbody>().velocity = Vector3.zero;
            AI.stunState.StartState(5f, AI.transform.forward * AI.speed * 2f);
        }
        else if(Vector3.Distance(AI.transform.position, moveToTarget) > 0.1f && !isStop){
            // Debug.Log("Moveeeee");
            AI.transform.position = Vector3.MoveTowards(AI.transform.position,
                                                    moveToTarget,
                                                    AI.speed * Time.deltaTime);
                                                    // (speed += (speed/1.25f*Time.deltaTime)) * Time.deltaTime);
        }
        else{
            EndState();
        }
    }

    void MagnetControl(){
        
    }
}
