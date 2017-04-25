using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class StunState : AIState {
    public string name{ get; }
    private readonly StatePatternAI AI;
    private float stunTime, counter;
    public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}

    public StunState(StatePatternAI AI){
        this.AI = AI;
        choice = new List<AIState>();
    }

    public void StartState()
    {
       InitBasic();
       this.stunTime = 3f;
    }
    public void StartState(float stunTime)
    {
        InitBasic();
        this.stunTime = stunTime;
    }
    public void StartState(float stunTime, Vector3 force)
    {
        InitBasic();
        this.stunTime = stunTime;
        AI.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }

    void InitBasic(){
        Debug.Log("Start Stun");
        Player.instance.transform.SetParent(null);
        AI.currentState = AI.stunState;
        AI.RagdollMode();
        counter = 0f;
        AI.effectManager.CreateEffectByName(EffectName.STUN_5_SEC, AI.transform.position + (Vector3.up*2f));
        AI.effectManager.tempEffects[EffectName.STUN_5_SEC].transform.rotation = Quaternion.Euler(90f, 0, 0);
        AI.DisableMagnet();
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
        AI.effectManager.DestroyEffectByName(EffectName.STUN_5_SEC);
        AI.effectManager.RemoveEffectFromDictByName(EffectName.STUN_5_SEC);
        AI.EditMagnet(1000f, 100);
        AI.magnet.transform.parent = AI.body.transform;
        AI.GetComponent<SphereCollider>().enabled = false;
        AI.body.GetComponent<SphereCollider>().isTrigger = false;
        AI.magnet.GetComponent<ContinuousExplosionForce>().force = -1000f;
        
        AI.NextState();
    }

    void StunTimer(){
        // Debug.Log("Now Stun");
        AI.effectManager.tempEffects[EffectName.STUN_5_SEC].transform.position = AI.body.transform.position + (Vector3.up*2.5f);
        if(counter < stunTime){
            counter += 1*Time.fixedDeltaTime;
        }
        else{
            EndState();
        }
    }

    void MagnetDown(){
        AI.magnet.GetComponent<ContinuousExplosionForce>().size = 0;
        AI.magnet.SetActive(false);
    }

    void MagnetUp(){
        AI.magnet.GetComponent<ContinuousExplosionForce>().size = 15;
    }
}