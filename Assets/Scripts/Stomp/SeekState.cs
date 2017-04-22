using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekState : AIState {
    private readonly StatePatternAI AI;
    private float seekTime;
    private Vector3 target;
    private float timer;
    public float speed;
    public float frequency;
    private float timerBeforeTarget;
	public string name{ get; }
    public List<AIState> choice{ get;set; }

    public SeekState(StatePatternAI statePatternAI){
		AI = statePatternAI;
        choice = new List<AIState>();
	}

    public void StartState()
    {
        Debug.Log("Seek Start");
        AI.currentState = AI.seekState;
        speed = 10f;
        frequency = 3f;
        seekTime = Random.Range(3f, 5f);
        timer = 0;
        timerBeforeTarget = 0;
		target = AI.player.transform.position;
        AI.transform.rotation = new Quaternion(0, 0, 0, 0);
        
        AI.EditMagnet(1000, 100);
        AI.magnet.GetComponent<ContinuousExplosionForce>().force = -100f;
    }

    public void UpdateState()
    {
        Seek();
    }

    public void EndState()
    {
        Debug.Log("Seek End");
        AI.stompState.StartState();
    }

    public void StateChangeCondition()
    {

    }

    void Seek(){ 
        if(timer <= seekTime){
            timer += Time.deltaTime;
            Targeting();
        }
        else{
            EndState();
        }
    }

    void Look(){
        Vector3 lookPos = new Vector3(AI.player.transform.position.x,
                                        AI.player.transform.position.y + 8.5f,
                                        AI.player.transform.position.z);
        AI.transform.LookAt(lookPos);
    }

    void Targeting(){
        AI.animationManager.PlaySeekAnim();
        target = new Vector3(AI.player.transform.position.x,
                                    AI.player.transform.position.y + 10f,
                                    AI.player.transform.position.z);
        
        AI.transform.position = Vector3.MoveTowards(AI.transform.position, target, speed * Time.deltaTime);
    }
}
