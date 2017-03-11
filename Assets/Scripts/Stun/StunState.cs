using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class StunState : AIState {
    public string name{ get; }
    private readonly StatePatternAI AI;
    private float stunTime, counter;

    public StunState(StatePatternAI AI){
        this.AI = AI;
    }

    public void StartState()
    {
        Debug.Log("Start Stun with default 3 sec stunTime");
        this.stunTime = 3f;
        counter = 0f;
        AI.currentState = AI.stunState;
        AI.RagdollMode();
    }
    public void StartState(float stunTime)
    {
        Debug.Log("Start Stun");
        this.stunTime = stunTime;
        counter = 0f;
        AI.currentState = AI.stunState;
        AI.RagdollMode();
    }
    public void StartState(float stunTime, Vector3 force)
    {
        Debug.Log("Start Stun");
        this.stunTime = stunTime;
        counter = 0f;
        AI.currentState = AI.stunState;
        AI.RagdollMode();
        AI.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }

    public void StateChangeCondition()
    {
        
    }

    public void UpdateState()
    {
        StunTimer();
    }

    public void EndState()
    {
        Debug.Log("End Stun");
        AI.NoRagdollMode();
        AI.seekState.StartState();
    }

    void StunTimer(){
        Debug.Log("Now Stun");
        if(counter < stunTime){
            counter += 1*Time.fixedDeltaTime;
        }
        else{
            EndState();
        }
    }
}