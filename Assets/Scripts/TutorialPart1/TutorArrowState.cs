using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorArrowState : AIState {

	public string name{ get; }
    private readonly TutorialAIStatePattern AI;
    public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}

    private string[] talkScript;
    private bool isWaitToNext, isCountDown, isHoldBow;
    private float counter, timeLimit;
    private int talkCounter;

	public TutorArrowState(TutorialAIStatePattern AI){
        this.AI = AI;
        choice = new List<AIState>();
    }

	// Use this for initialization
	public void StartState()
    {
        isWaitToNext = false;
        isCountDown = false;
        talkScript = new string[]{"Next, I will show you how to draw a bow from your back.", //0
								"Move your controller behind your neck.", //1
								"When a controller is behind your neck it will vibrate, then press the trigger button.", //2
								"Good, put arrow at the bow, hold trigger and pull back.", //3
								"Release to shoot, now shoot at the capsule-shaped monsters.", //4
								"Good job, you can choose arrow skills.", //5
                                "By touching the touch pad on arrow controller side.", //6
								"Skilled-arrow must be fully charged to shoot", //7
								"They have cooldown time. Be aware of it.", //8
                                "Now try to shoot a fire arrow.", //9
                                "Very good, Let's move to next area."}; //10

        talkCounter = 0;
        // AI.currentState = AI.tutorArrowState;
    }

	public void StateChangeCondition()
    {
        
    }

    public void UpdateState()
    {
        Talk();
    }

	public void EndState()
    {

	}

    void Talk() {
        if(!isWaitToNext) {
            AI.UpdateText(talkScript[talkCounter]);
            talkCounter++;
            isWaitToNext = true;
        }
        else if(talkCounter == 3){
            if(isHoldBow) {
                Countdown(2f);
            }
        }
        else if(talkCounter == 5){
            AI.minions[0].SetActive(true);
        }
        else {
            Countdown(1f);
        }
    }

    void Countdown(float sec){
        if(!isCountDown){
            Debug.Log("wait");
            timeLimit = sec;
            counter = 0;
            isWaitToNext = true;
            isCountDown = true;
        }
        else{
            Debug.Log("count down");
            if(counter < timeLimit){
                counter += 0.01f;
            }
            else{
                isWaitToNext = false;
                isCountDown = false;
            }
        }
    }
}
