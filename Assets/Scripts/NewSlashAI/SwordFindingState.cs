using UnityEngine;
using System.Collections;

public class SwordFindingAIState : AIState {
	private readonly AITest AI;
	public string name{ get;}
	private float speed;
	private GameObject[] AISword;
	private GameObject swordTarget;

	public SwordFindingAIState(AITest statePatternAI){
		AI = statePatternAI;
	}

	public void StartState(){
		AI.currentState = AI.swordFindingAIState;
		speed = 10;
		AISword = GameObject.FindGameObjectsWithTag ("AISword");
		swordTarget = null;
		foreach(GameObject sword in AISword){
			if(swordTarget == null || Vector3.Distance(AI.transform.position, sword.transform.position) < Vector3.Distance(AI.transform.position, swordTarget.transform.position)){
				swordTarget = sword;
			}
		}
	}

	public void UpdateState(){
		AI.transform.LookAt (AI.player.transform.position);
		AI.transform.position = Vector3.MoveTowards (AI.transform.position, swordTarget.transform.position, speed/100f);
		StateChangeCondition ();
	}

	public void EndState(){
//		AI.GetComponent<Rigidbody>().isKinematic = false;
		AI.swordPullingAIState.StartState ();
	}

	public void StateChangeCondition(){
		if (Vector3.Distance (AI.transform.position, swordTarget.transform.position) < 5f) {
			EndState ();
		}
	}
}
