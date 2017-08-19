using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TutorDashState : MonoBehaviour, AIState {

	public string name{ get; }
    private readonly TutorialAIStatePattern AI;
    public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}

    private string[] talkScript;
    private bool isWaitToNext, isCountDown, isCountDownScene;
    private float counter, timeLimit;
    private int talkCounter, dashCount;

    private SceneController sceneCon = new SceneController();

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
								"Shoot me to skip this part or approch light ball behind you to fight boss.", //1
								"Seem like you had lost all of your memory.", //2
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
        // CountdownChangeScene(3f);
    }

    public void UpdateState()
    {
        Talk();
    }

	public void EndState()
    {
        CountdownChangeScene(3f);
	}

    void Talk() {
        if(AI.isEndTutor){
            CountdownChangeScene(4f);
        }
        if(!isWaitToNext) {
            AI.UpdateText(talkScript[talkCounter]);
            talkCounter++;
            isWaitToNext = true;
        }
        else if(talkCounter == 2){ 
            // if(AI.isEndTutor){
            //     CountdownChangeScene(4f);
            // }
            // else{
                Countdown(6f);
            // }
        }
        else if(talkCounter == 3){ 
            // AI.isTutor = true;
            Countdown(2f);
        }
        else if(talkCounter == 8){
            AI.SetTutorPicControl(AI.viveButtons[2], AI.viveButtons[2]);
            Countdown(5f);
        }
        else if(talkCounter == 8){
            Player.instance.GetComponent<PlayerControl>().isDashable=true;
            Countdown(5f);
        }
        else if(talkCounter == 9){   
            if(AI.dashTarget[2].GetComponent<DashTarget>().isPass){
                EndState();
            }
            else if(AI.dashTarget[1].GetComponent<DashTarget>().isPass){
                AI.dashTarget[2].GetComponent<DashTarget>().TargetActive();
            }
            else if(AI.dashTarget[0].GetComponent<DashTarget>().isPass){
                AI.dashTarget[1].GetComponent<DashTarget>().TargetActive();
            }
            else if(!AI.dashTarget[0].GetComponent<DashTarget>().isPass){
                AI.dashTarget[0].GetComponent<DashTarget>().TargetActive();
            }
        }
        else {
            Countdown(5f);
        }
    }

    void Countdown(float sec){
        if(!isCountDown){
            // Debug.Log("wait");
            timeLimit = sec;
            counter = 0;
            isWaitToNext = true;
            isCountDown = true;
        }
        else{
            // Debug.Log("count down");
            if(counter < timeLimit){
                // counter += 0.01f;
                counter += Time.deltaTime;
            }
            else{
                isWaitToNext = false;
                isCountDown = false;
            }
        }
    }

    IEnumerator CountDown(float sec){
		// isUpdateText = true;

		yield return new WaitForSeconds(sec);
        isWaitToNext = !isWaitToNext;

		// if(counter+1 == talkScript.Length){
		// 	Debug.Log("EndTutor");
		// 	isEndTutor = true;
		// }

		// if(!isEndTutor){
		// 	NextTalkScript();
		// }
	}

    void CountdownChangeScene(float sec){
        if(!isCountDownScene){
            AI.UpdateText("I will send you to next part of the tutorial.");
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
                sceneCon.ChangeScene(SceneController.ARROW_TUTOR);
            }
        }
    }
}
