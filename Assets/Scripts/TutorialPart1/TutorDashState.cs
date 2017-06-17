using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TutorDashState : AIState {

	public string name{ get; }
    private readonly TutorialAIStatePattern AI;
    public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}

    private string[] talkScript;
    private bool isWaitToNext, isCountDown, isCountDownScene;
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
        isCountDownScene = false;
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

        talkCounter = 0;
        AI.currentState = AI.tutorDashState;
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
        AI.tutorArrowState.StartState();
	}

    void Talk() {
        if(!isWaitToNext) {
            AI.UpdateText(talkScript[talkCounter]);
            talkCounter++;
            isWaitToNext = true;
        }
        else if(talkCounter == 2){ 
            if(AI.isEndTutor){
                CountdownChangeScene(4f);
            }
            else{
                Countdown(5f);
            }
        }
        else if(talkCounter == 3){ 
            AI.isTutor = true;
            Countdown(2f);
        }
        else if(talkCounter == 8){
            Player.instance.GetComponent<PlayerControl>().isDashable=true;
            Countdown(2f);
        }
        else if(talkCounter == 9){   
            if(AI.gameCon.GetComponent<TutorPartOneGameController>().dashCount == 0){
                AI.dashTarget[0].SetActive(true);
            }
            else if(AI.gameCon.GetComponent<TutorPartOneGameController>().dashCount == 1){
                AI.dashTarget[1].SetActive(true);
            }
            else if(AI.gameCon.GetComponent<TutorPartOneGameController>().dashCount == 2){
                AI.dashTarget[2].SetActive(true);
            }
            else if(AI.gameCon.GetComponent<TutorPartOneGameController>().dashCount == 3){
                EndState();
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
            // Debug.Log("count down");
            if(counter < timeLimit){
                counter += 0.01f;
            }
            else{
                isWaitToNext = false;
                isCountDown = false;
            }
        }
    }

    void CountdownChangeScene(float sec){
        if(!isCountDownScene){
            AI.UpdateText("I will send you to defeat your own creation. I hope this is the last time. Good luck.");
            timeLimit = sec;
            counter = 0;
            isWaitToNext = true;
            isCountDown = true;
            isCountDownScene = true;
        }
        else{
            if(counter < timeLimit){
                counter += 0.01f;
            }
            else{
                AI.gameCon.GetComponent<TutorPartOneGameController>().sceneCon.ChangeScene(SceneController.GAME);
            }
        }
    }
}
