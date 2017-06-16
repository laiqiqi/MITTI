using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorDashState : AIState {

	public string name{ get; }
    private readonly TutorialAIStatePattern AI;
    public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}

    private string[] talkScript;
    private bool isWaitToNext, isCountDown, isDashToAllTarget;
    private float counter, timeLimit;
    private int talkCounter;

	public TutorDashState(TutorialAIStatePattern AI){
        this.AI = AI;
        choice = new List<AIState>();
    }

	// Use this for initialization
	public void StartState()
    {
        isWaitToNext = false;
        isCountDown = false;
        talkScript = new string[]{"Hello, Arcane transmutor.", //0
								"Shoot me with the arrow, if this is not the first time.", //1
								"It seem like you had lost all of your memory.", //2
								"I will show you how to use your power.", //3
								"Look at the gauge around your feet.", //4
								"The green one show your health and orange show your stamina.", //5
								"Let’s start with special movement.", //6
								"You can press any touchpad to quickly dash forward.", //7
								"Go to yellow light that appear around.", //8
								"Good job, now you know how to evade and move around."}; //9

        AI.currentState = AI.tutorDashState;
        talkCounter = 0;
        Debug.Log(talkScript[talkCounter]);
        Talk();
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
        if(talkCounter == 9){
            if(isDashToAllTarget){
                Countdown(0.5f);
            }
            else if(AI.gameCon.GetComponent<TutorPartOneGameController>().killCount == 0){
                AI.dashTarget[0].SetActive(true);
            }
            else if(AI.gameCon.GetComponent<TutorPartOneGameController>().killCount == 1){
                AI.dashTarget[1].SetActive(true);
            }
            else if(AI.gameCon.GetComponent<TutorPartOneGameController>().killCount == 2){
                AI.dashTarget[2].SetActive(true);
            }
            else if(AI.gameCon.GetComponent<TutorPartOneGameController>().killCount == 3){
                isDashToAllTarget = true;
            }
        }
        else {
            Countdown(2f);
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
